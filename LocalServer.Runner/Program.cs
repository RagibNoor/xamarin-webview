using System;

namespace LocalServer.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            //var outPutBasePath = @"/Users/streamstech/Desktop/ragib/db";
            var outPutBasePath = @"D:\IQVIA\data\poc2\";
            var local = new LoadLocalServer(outPutBasePath, "");
            // var _s3 = new S3Utility();
            //// _s3.DownloadDatabaseSchema().GetAwaiter().GetResult();
            // var watch = new System.Diagnostics.Stopwatch();
            // watch.Start();
            // Console.WriteLine("Hello World!");
            //  _s3.GetListOfFileNameAsync().GetAwaiter().GetResult();
            //  watch.Stop();
            //  TimeSpan elapsed = watch.Elapsed; // however you get the amount of time elapsed
            //  string tsOut = elapsed.ToString(@"m\:ss");
            Console.ReadKey(true);
        }
    }
}
