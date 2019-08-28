namespace EtfExtractorLibrary.Settings
{
   using System;
   using System.Collections.Generic;
   using System.Text;

   public interface ISymbolsReaderSettings
    {
      string DataUri
      {
         get;
         set;
      }

      int NumberOfQuotesToRead
      {
         get;
         set;
      }

      int SleepBetweenRequests
      {
         get;
         set;
      }

      string ColumnSeparator
      {
         get;
         set;
      }

      string EmptyQuotePlaceholder
      {
         get;
         set;
      }

      string OutputFolder
      {
         get;
         set;
      }
   }
}
