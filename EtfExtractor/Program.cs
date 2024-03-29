﻿using System;
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
            "CEF",
            "PHY",
            "PPT",
            "PHS",
            "MNT",
            "MNS",
            "AHY",
            "BHY",
            "PBY",
            "SCW",
            "HYB",
            "DSL",
            "ECF",
            "PCF",
            "ADC",
            "NCD",
            "FFI",
            "CBT",
            "MFR",
            "MLD",
            "MLF",
            "OCS",
            "OSL",
            "PCD",
            "PMB",
            "PGI",
            "RIB",
            "FRL",
            "SSF",
            "TRH",
            "IFL",
            "ISL",
            "IHL",
            "AEU",
            "ACZ",
            "AOG",
            "AUI",
            "HRR",
            "BLB",
            "BUA",
            "HBL",
            "OSP",
            "BGI",
            "BSO",
            "UDA",
            "CPF",
            "BK" ,
            "CCI",
            "PFT",
            "UTE",
            "EIT",
            "CRP",
            "CDD",
            "DS" ,
            "EVT",
            "ENI",
            "HEN",
            "EBC",
            "EBF",
            "EGI",
            "FGX",
            "DCD",
            "TRF",
            "UCD",
            "GAF",
            "GDG",
            "GHC",
            "HIG",
            "GRL",
            "HGI",
            "HTO",
            "GBF",
            "GSB",
            "MDS",
            "HHY",
            "INC",
            "IFB",
            "PFU",
            "PFD",
            "LOW",
            "LVU",
            "MQA",
            "MQI",
            "MBK",
            "MBN",
            "RCO",
            "HWF",
            "NGI",
            "NEW",
            "NXC",
            "NRF",
            "OSF",
            "PRF",
            "MMP",
            "PIC",
            "PDV",
            "RAV",
            "RAI",
            "IDR",
            "PME",
            "SIF",
            "TOF",
            "HTA",
            "TLF",
            "TCT",
            "TTY",
            "TTE",
            "TUT",
            "PUB",
            "US" ,
            "USF",
            "USH",
            "UTC",
            "MBB",
            "AV" ,
            "VIP",
            "RBN",
            "BSE",
            "CIQ",
            "RTU",
            "CTF",
            "CMZ",
            "NAF",
            "IDX",
            "JFS",
            "MIF",
            "MID",
            "NPF",
            "SIN",
            "SKG",
            "XYM",
            "YP" ,
            "ERM",
            "FNM",
            "GII",
            "TGF",
            "TZZ",
            "TZS",
            "PBU",
            "MKZ",
            "FBS",
            "ABK",
            "ALB",
            "BIG",
            "BBO",
            "BSC",
            "LCS",
            "SBC",
            "BPS",
            "BSD",
            "LFE",
            "YCM",
            "DFN",
            "DF" ,
            "DGS",
            "FCS",
            "FTN",
            "GCS",
            "GRP",
            "LBS",
            "XMF",
            "FFN",
            "PVS",
            "RBS",
            "SBN",
            "XTD",
            "TXT",
            "FTU",
            "UST",
            "WFS",
            "BXS"
               });

            foreach(string ticker in tickersRaw)
            {
               tickers.Add(ticker);
               tickers.Add(ticker + "-a");
               tickers.Add(ticker + ".a");
               tickers.Add(ticker + ".u");
               tickers.Add(ticker + ".un");
            }*/


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
