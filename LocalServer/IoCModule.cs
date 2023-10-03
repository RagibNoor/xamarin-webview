using System;
using System.Threading.Tasks;
using Autofac;
using EmbedIO;
using EmbedIO.Utilities;

namespace LocalServer
{
    /// <inheritdoc />
    public class IoCModule : WebModuleBase
    {
        // Unique key for storing scopes in HTTP context items
        internal static readonly object ScopeKey = new object();

        private readonly IContainer _container;
        private readonly Action<ContainerBuilder> _configurationAction;

        /// <inheritdoc />
        public IoCModule(IContainer container, Action<ContainerBuilder> configurationAction = null)
            : base(UrlPath.Root)
        {
            // We will need the container to create child scopes.
            this._container = container ?? throw new ArgumentNullException(nameof(container));

            // We can optionally use a configuration action
            // to register additional services for each HTTP context.
            this._configurationAction = configurationAction;
        }

        // Tell EmbedIO that this module does not handle requests completely.
        /// <inheritdoc />
        public override bool IsFinalHandler => false;

        /// <inheritdoc />
        protected override Task OnRequestAsync(IHttpContext context)
        {
            // Create a scope for the HTTP context, tagging it with the context.
            // Use the configuration action if one has been specified.
            var scope = this._configurationAction == null
                ? this._container.BeginLifetimeScope(context, builder => builder.RegisterInstance(context))
                : this._container.BeginLifetimeScope(context, builder =>
                {
                    builder.RegisterInstance(context);
                    this._configurationAction?.Invoke(builder);
                });


            // Store the scope for later retrieval.
            context.Items.Add(ScopeKey, scope);

            // Ensure that the scope is disposed when EmbedIO is done processing the request.
            context.OnClose(this.OnContextClose);

            return Task.CompletedTask;
        }

        private void OnContextClose(IHttpContext context)
        {
            // Retrieve the scope and dispose it
            var scope = context.Items[ScopeKey] as ILifetimeScope;
            context.Items.Remove(ScopeKey);
            scope?.Dispose();
        }
    }
}
