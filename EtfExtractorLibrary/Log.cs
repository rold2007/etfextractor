namespace EtfExtractorLibrary
{
   using System;
   using System.Collections.Generic;
   using System.IO;

   public class Log
    {
        static Log()
        {
            LogWriters = new List<TextWriter> { Console.Out };
        }
        internal static IList<TextWriter> LogWriters { get; set; }
        public static void Info(string message)
        {
            WriteMessage(string.Concat("INFO: ", message));
        }
        public static void Error(string message)
        {
            WriteMessage(string.Concat("Error: ", message));
        }
        private static void WriteMessage(string message)
        {
            try
            {
                foreach (var writer in LogWriters)
                {
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                //intentionally left blank
            }
        }
    }
}