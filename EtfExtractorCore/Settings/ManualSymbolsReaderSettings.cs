using EtfExtractorLibrary.Settings;

namespace EtfExtractorCore.Settings
{
   public class ManualSymbolsReaderSettings : ISymbolsReaderSettings
   {
      public string DataUri
      {
         get;
         set;
      }

      public int NumberOfQuotesToRead
      {
         get;
         set;
      }

      public int SleepBetweenRequests
      {
         get;
         set;
      }

      public string ColumnSeparator
      {
         get;
         set;
      }

      public string EmptyQuotePlaceholder
      {
         get;
         set;
      }

      public string OutputFolder
      {
         get;
         set;
      }
   }
}
