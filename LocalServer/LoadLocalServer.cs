using System;
using System.IO;
using System.Reflection;
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
                        .WithMode(HttpListenerMode.EmbedIO))
                    .WithLocalSessionManager()
                    .WithStaticFolder("/", location, true, m => m
                        .WithContentCaching(true)
                    );

                staticFilesWebServer.RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        public string GenerateFrontEndServerUrl()
        {

            var dynamicFrontEndPortNumber = 4200;
            return $"http://localhost:{dynamicFrontEndPortNumber}";
        }
    }
}
