using System;

namespace EmbedIO
{
    public static class LogExtention
    {
        public static void Info(
            this string message,
            string? source = null)
        {
            Console.WriteLine(message);
            Console.WriteLine(source);
        }

        public static void Debug(
            this string message,
            string? source = null)
        {
            Console.WriteLine(message);
            Console.WriteLine(source);
        }

        public static void Error(
            this string message,
            string? source = null)
        {
            Console.WriteLine(message);
            Console.WriteLine(source);
        }

        public static void Trace(
            this string message,
            string? source = null)
        {
            Console.WriteLine(message);
            Console.WriteLine(source);
        }

    }
}
