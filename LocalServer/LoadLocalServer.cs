using System;
using System.IO;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Threading;
using EmbedIO;
using EmbedIO.Files;
using EmbedIO.Security;
using EmbedIO.Utilities;
using EmbedIO.WebApi;
using LocalServer;


namespace OfflineApp.EmbedIO.LocalServer
{
    public class LoadLocalServer
    {
        private WebServer staticFilesWebServer;

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
