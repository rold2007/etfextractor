namespace EtfExtractorLibrary.Settings
{
   public interface ITickersProviderSettings
    {
      string FileUri
      {
         get;
         set;
      }

      string SheetName
      {
         get;
         set;
      }

      int TickerListRowIndex
      {
         get;
         set;
      }

      int TickerListColumnIndex
      {
         get;
         set;
      }
   }
}
