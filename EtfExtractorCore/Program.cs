using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EtfExtractorLibrary;
using EtfExtractorLibrary.Settings;
using EtfExtractorLibrary.SymbolsReader;
using EtfExtractorLibrary.TickersProvider;
using EtfExtractorCore.Settings;

namespace EtfExtractorCore
{
   public class Program
   {
      static void Main(string[] args)
      {
         try
         {
            // See https://github.com/ExcelDataReader/ExcelDataReader#important-note-on-net-core
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            ManualTickersProviderSettings closedEndFundsManualTickersProviderSettings = new ManualTickersProviderSettings();

            closedEndFundsManualTickersProviderSettings.FileUri = "https://www.tsx.com/resource/en/563";
            closedEndFundsManualTickersProviderSettings.SheetName = "";
            closedEndFundsManualTickersProviderSettings.TickerListRowIndex = 8;
            closedEndFundsManualTickersProviderSettings.TickerListColumnIndex = 3;

            ExcelTickersProvider closedEndFundsTickersProvider = new ExcelTickersProvider(closedEndFundsManualTickersProviderSettings);
            List<string> closedEndFundsTickers = closedEndFundsTickersProvider.GetTickers().ToList();

            ManualTickersProviderSettings etfManualTickersProviderSettings = new ManualTickersProviderSettings();

            etfManualTickersProviderSettings.FileUri = "http://www.tmxmoney.com/en/pdf/ETF_List.xls";
            etfManualTickersProviderSettings.SheetName = "Total";
            etfManualTickersProviderSettings.TickerListRowIndex = 2;
            etfManualTickersProviderSettings.TickerListColumnIndex = 1;
            https://zimmergren.net/using-appsettings-json-instead-of-web-config-in-net-core-projects/
            ExcelTickersProvider etfTickersProvider = new ExcelTickersProvider(etfManualTickersProviderSettings);
            List<string> etfTickers = etfTickersProvider.GetTickers().ToList();

            ManualSymbolsReaderSettings manualSymbolsReaderSettings = new ManualSymbolsReaderSettings();

            manualSymbolsReaderSettings.ColumnSeparator = ";";

            SymbolDataReader reader = new SymbolDataReader(manualSymbolsReaderSettings);
            IList<SymbolData> data = reader.GetSymbolData(etfTickers);

            WriteSymbolData(data, ".", manualSymbolsReaderSettings);
         }
         catch (Exception e)
         {
            Log.Error($"Application error occured: {e}");
         }

         Console.ReadKey();
      }

      private static void WriteSymbolData(IEnumerable<SymbolData> data, string outputFolder, ISymbolsReaderSettings symbolsReaderSettings)
      {
         //string outputFolder = ConfigurationManager.AppSettings["OutputFolder"] ?? string.Empty;
         string filePath = string.Concat(outputFolder, DateTime.Now.ToString("yyyy_MM_dd_OUTPUT"), ".csv");
         try
         {
            StringBuilder outputData = new StringBuilder(SymbolData.GetHeader(symbolsReaderSettings.ColumnSeparator)).AppendLine();

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
