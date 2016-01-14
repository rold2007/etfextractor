using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using EtfExtractor.Settings;
using EtfExtractor.SymbolsReader;
using EtfExtractor.TickersProvider;

namespace EtfExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ExcelTickersProvider tickersProvider = new ExcelTickersProvider(TickersProviderSettings.Instance);
                List<string> tickers = tickersProvider.GetTickers().ToList();

                SymbolDataReader reader = new SymbolDataReader(SymbolsReaderSettings.Instance);
                IList<SymbolData> data = reader.GetSymbolData(tickers);

                WriteSymbolData(data);                
            }
            catch (Exception e)
            {
                Log.Error($"Application error occured: {e}");
            }

            Console.ReadKey();
        }

        private static void WriteSymbolData(IEnumerable<SymbolData> data)
        {
            string outputFolder = ConfigurationManager.AppSettings["OutputFolder"] ?? string.Empty;
            string filePath = string.Concat(outputFolder, DateTime.Now.ToString("yyyy_MM_dd_OUTPUT"), ".csv");
            try
            {
                StringBuilder outputData = new StringBuilder(SymbolData.GetHeader()).AppendLine();

                foreach (var symbolData in data)
                {
                    outputData.AppendLine(symbolData.ToString());
                }

                Log.Info($"Writing output file to [{filePath}]");
                File.WriteAllText(filePath, outputData.ToString());
            }
            catch (Exception)
            {
                Log.Error($"Output file [{filePath}] cannot be created. No output file was produced.");
            }
        }
    }
}
