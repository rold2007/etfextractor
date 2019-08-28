using System;
using System.Collections.Generic;
using System.Text;
using EtfExtractorLibrary.Settings;

namespace EtfExtractorCore.Settings
{
    public class ManualTickersProviderSettings : ITickersProviderSettings
    {
      public string FileUri
      {
         get;
         set;
      }

      public string SheetName
      {
         get;
         set;
      }

      public int TickerListRowIndex
      {
         get;
         set;
      }

      public int TickerListColumnIndex
      {
         get;
         set;
      }
   }
}
