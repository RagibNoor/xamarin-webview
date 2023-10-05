using System;
using System.IO;
using System.Text;
using EmbedIO;
using EmbedIO.Files;
using EmbedIO.Security;
using EmbedIO.Utilities;
using EmbedIO.WebApi;

namespace LocalServer
{
    public class LoadLocalServer
    {
        private WebServer staticFilesWebServer;
        private WebServer applicationWebServer;

        public LoadLocalServer(string outPutBasePath, string location)
        {
            OutPutBasePathConstant.outPutBasePath = outPutBasePath;
            try
            {
                location = Path.Combine(location, "dist");
                staticFilesWebServer = new WebServer(o => o
                        .WithUrlPrefix(GenerateFrontEndServerUrl())
                        .WithMode(HttpListenerMode.Microsoft))
                    .WithLocalSessionManager()
                    .WithStaticFolder("/", location, true, m => m
                        .WithContentCaching(true)
                    );

                staticFilesWebServer.HandleHttpException(async (context, exception) =>
                {
                    context.Response.StatusCode = exception.StatusCode;

                    switch (exception.StatusCode)
                    {
                        case 404:
                            await context.SendStringAsync("Your content", "text/html", Encoding.UTF8);
                            break;
                        default:
                            await HttpExceptionHandler.Default(context, exception);
                            break;
                    }
                });
                staticFilesWebServer.RunAsync();
                     this.applicationWebServer = new WebServer(o => o
                        .WithUrlPrefix("http://localhost:8080")
                        .WithMode(HttpListenerMode.EmbedIO))
                    .WithIPBanning(o => o
                        .WithMaxRequestsPerSecond()
                        .WithRegexRules("HTTP exception 404"));
                this.applicationWebServer
                    .WithLocalSessionManager()
                    .WithCors()
                    .WithWebApi(UrlPath.Root,
                (module => { module.WithController<MyFavouriteProgrammingLanguage>(); }));
                // .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));
                applicationWebServer.RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        public string GenerateFrontEndServerUrl()
        {

            var dynamicFrontEndPortNumber = 44200;
            return $"http://localhost:{dynamicFrontEndPortNumber}";
        }
    }
}
