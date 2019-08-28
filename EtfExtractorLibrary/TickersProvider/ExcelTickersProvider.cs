namespace EtfExtractorLibrary.TickersProvider
{
   using System;
   using System.Collections.Generic;
   using System.Data;
   using System.IO;
   using System.Linq;
   using System.Net;
   using EtfExtractorLibrary.Settings;
   using ExcelDataReader;

   public class ExcelTickersProvider : ITickersProvider
   {
      public ExcelTickersProvider(ITickersProviderSettings settings)
      {
         this.TickersProviderSettings = settings;
      }

      public ITickersProviderSettings TickersProviderSettings
      {
         get;
         private set;
      }

      private string DownloadTickersFile()
      {
         if (IsValidHttpUri(TickersProviderSettings.FileUri))
         {
            string fileName = string.Concat(DateTime.Now.ToString("yyyy_MM_dd"), ".xlsx");
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

            IEnumerable<DataRow> totalTable = result.Tables[TickersProviderSettings.SheetName].Rows.Cast< DataRow>();
            tickers = totalTable.
                Where((r, i) => i >= TickersProviderSettings.TickerListRowIndex).
                Select(r => r[TickersProviderSettings.TickerListColumnIndex].ToString().Trim()).
                Where(t => t.Length > 0).
                Distinct().
                ToList();
         }

         return tickers;
      }
   }
}