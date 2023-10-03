using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System.Threading;


namespace OfflineApp.EmbedIO.LocalServer
{
    public class MyFavouriteProgrammingLanguage : WebApiController
    {
        [Route(HttpVerbs.Get, "/api/favouritelanguage")]
        public async Task<string[]> Post(WebServer server, HttpListenerContext context)
        {
            try
            {

                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                Thread.Sleep(10000);
                watch.Stop();
                TimeSpan elapsed = watch.Elapsed;
                var tsOut = countClass.count_++;
                string[] words = { tsOut.ToString(), "done"};
                return words.ToArray();
            }
            catch (Exception ex)
            {
                string[] words = { ex.Message };
                return words.ToArray();
            }
        }
    }

    public static class countClass
    {
        public static int count_ = 0;
    }
}
