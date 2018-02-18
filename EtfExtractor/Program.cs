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
            /*
// From http://www.tmxmoney.com/en/research/closed-end_funds.html

            List<string> tickers = new List<string>();

            List<string> tickersRaw = new List<string>(new string[]
            {
"ABK",
"ACZ",
"ADC",
"AEU",
"AHY",
"ALB",
"AOG",
"AUI",
"AV",
"BGI",
"BHY",
"BIG",
"BK",
"BLB",
"BPS",
"BSC",
"BSD",
"BSE",
"BSO",
"BUA",
"BXS",
"CBT",
"CCI",
"CDD",
"CEF",
"CIQ",
"CPF",
"CRP",
"CTF",
"DCD",
"DF",
"DFN",
"DGS",
"DS",
"DSL",
"EBC",
"ECF",
"EGI",
"EIT",
"ENI",
"EVT",
"FCS",
"FFI",
"FFN",
"FGX",
"FNM",
"FRL",
"FTN",
"FTU",
"GAF",
"GBF",
"GCS",
"GDG",
"GRL",
"GRP",
"GSB",
"HBL",
"HEN",
"HGI",
"HIG",
"HRR",
"HTA",
"HTO",
"HWF",
"HYB",
"IDR",
"IDX",
"IFB",
"IFL",
"IHL",
"INC",
"ISL",
"JFS",
"LBS",
"LCS",
"LFE",
"LOW",
"LVU",
"MBB",
"MBK",
"MBN",
"MDS",
"MFR",
"MID",
"MIF",
"MKZ",
"MLD",
"MLF",
"MMP",
"MNS",
"MNT",
"MQI",
"NAF",
"NCD",
"NEW",
"NGI",
"NPF",
"NRF",
"NRGY",
"NXC",
"OCS",
"OSF",
"OSL",
"OSP",
"PBU",
"PBY",
"PCD",
"PCF",
"PDV",
"PFD",
"PFT",
"PFU",
"PGI",
"PHYS",
"PIC",
"PMB",
"PME",
"PRF",
"PSLV",
"PUB",
"PVS",
"RAI",
"RAV",
"RBN",
"RBP",
"RBS",
"RCO",
"RIB",
"RIGP",
"RTU",
"SBC",
"SBN",
"SCW",
"SIN",
"SKG",
"SPPP",
"SSF",
"TCT",
"TGF",
"TLF",
"TOF",
"TRF",
"TTE",
"TTY",
"TUT",
"TXT",
"UCD",
"UDA",
"US",
"USF",
"USH",
"UTC",
"UTE",
"VIP",
"WFS",
"XMF",
"XTD",
"XYM",
"YCM"
               });

            foreach (string ticker in tickersRaw)
            {
               tickers.Add(ticker);
               tickers.Add(ticker + "-a");
               tickers.Add(ticker + ".a");
               tickers.Add(ticker + ".u");
               tickers.Add(ticker + ".un");
            }
            //*/


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
