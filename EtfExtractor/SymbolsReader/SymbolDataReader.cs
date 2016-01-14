using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EtfExtractor.Settings;

namespace EtfExtractor.SymbolsReader
{
    class SymbolDataReader
    {
        SymbolsReaderSettings SymbolDataReaderSettings { get; }

        internal SymbolDataReader(SymbolsReaderSettings settings)
        {
            this.SymbolDataReaderSettings = settings;
        }

        internal IList<SymbolData> GetSymbolData(List<string> tickers)
        {
            if (SymbolDataReaderSettings.NumberOfQuotesToRead > 0)
            {
                tickers = tickers.Take(SymbolDataReaderSettings.NumberOfQuotesToRead).ToList();
            }

            IList<SymbolData> symbolData = new List<SymbolData>();

            using (WebClient webClient = new WebClient())
            {
                Log.Info($"Starting to retrieve data for {tickers.Count} tickers.");
                foreach (string ticker in tickers)
                {
                    try
                    {
                        Log.Info(string.Concat("Getting data for: ", ticker));
                        string htmlDocument = webClient.DownloadString(string.Format(SymbolDataReaderSettings.DataUri, ticker));
                        SymbolData symbol = new SymbolData(ticker, htmlDocument, SymbolDataReaderSettings.EmptyQuotePlaceholder, SymbolDataReaderSettings.ColumnSeparator);
                        symbolData.Add(symbol);

                        Task.Delay(SymbolDataReaderSettings.SleepBetweenRequests).Wait();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Exception occured while retriving data for: {ticker}. Exception message: {e.Message}");
                    }
                }
                Log.Info($"Ended to retrieve data for given tickers.");
            }

            return symbolData;
        }
    }
}