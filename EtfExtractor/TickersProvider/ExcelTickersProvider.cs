using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using EtfExtractor.Settings;
using Excel;

namespace EtfExtractor.TickersProvider
{
    internal class ExcelTickersProvider : ITickersProvider
    {
        internal ExcelTickersProvider(TickersProviderSettings settings)
        {
            this.TickersProviderSettings = settings;
        }

        internal TickersProviderSettings TickersProviderSettings { get; set; }

        private string DownloadTickersFile()
        {
            if (IsValidHttpUri(TickersProviderSettings.FileUri))
            {
                string fileName = string.Concat(DateTime.Now.ToString("yyyy_MM_dd"), ".xls");
                if (File.Exists(fileName))
                {
                    Log.Info(string.Concat("Excel file name: ", fileName));
                    return fileName;
                }

                Log.Info(string.Concat("Downloading file: ", TickersProviderSettings.FileUri));
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(TickersProviderSettings.FileUri, fileName);
                }
                Log.Info(string.Concat("Excel file name: ", fileName));
                return fileName;
            }

            Log.Info(string.Concat("Excel file name: ", TickersProviderSettings.FileUri));
            return TickersProviderSettings.FileUri;
        }

        private bool IsValidHttpUri(string uri)
        {
            Uri uriResult;
            return Uri.TryCreate(uri, UriKind.Absolute, out uriResult)
                   && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        public IEnumerable<string> GetTickers()
        {
            string fileName = DownloadTickersFile();
             
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            List<string> tickers;

            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream))
            {
                DataSet result = excelReader.AsDataSet();

                IEnumerable<DataRow> totalTable = result.Tables[TickersProviderSettings.SheetName].AsEnumerable();
                tickers = totalTable.
                    Where((r,i) => i >= TickersProviderSettings.TickerListRowIndex).
                    Select(r => r[TickersProviderSettings.TickerListColumnIndex].ToString().Trim()).
                    Where(t => t.Length > 0).
                    Distinct().
                    ToList();
            }

            return tickers;
        }
    }
}