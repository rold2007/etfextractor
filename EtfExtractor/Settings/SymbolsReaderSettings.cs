namespace EtfExtractor.Settings
{
   using System;
   using System.Configuration;
   using EtfExtractorLibrary.Settings;

   class SymbolsReaderSettings : ConfigurationSection, ISymbolsReaderSettings
   {
      private static readonly SymbolsReaderSettings settings =
          ConfigurationManager.GetSection("SymbolsReaderSettings") as SymbolsReaderSettings;

      public static SymbolsReaderSettings Instance => settings;

      [ConfigurationProperty("DataUri", IsRequired = true)]
      public string DataUri
      {
         get { return (string)this["DataUri"]; }
         set { this["DataUri"] = value; }
      }

      [ConfigurationProperty("NumberOfQuotesToRead", DefaultValue = "0", IsRequired = true)]
      [IntegerValidator(MinValue = 0)]
      public int NumberOfQuotesToRead
      {
         get { return (int)this["NumberOfQuotesToRead"]; }
         set { this["NumberOfQuotesToRead"] = value; }
      }

      [ConfigurationProperty("SleepBetweenRequestsInMs", IsRequired = true)]
      public int SleepBetweenRequests
      {
         get { return (int)this["SleepBetweenRequestsInMs"]; }
         set { this["SleepBetweenRequestsInMs"] = value; }
      }

      [ConfigurationProperty("ColumnSeparator", IsRequired = false, DefaultValue = ",")]
      public string ColumnSeparator
      {
         get { return (string)this["ColumnSeparator"]; }
         set { this["ColumnSeparator"] = value; }
      }

      [ConfigurationProperty("EmptyQuotePlaceholder", IsRequired = false, DefaultValue = "---")]
      public string EmptyQuotePlaceholder
      {
         get { return (string)this["EmptyQuotePlaceholder"]; }
         set { this["EmptyQuotePlaceholder"] = value; }
      }

      [ConfigurationProperty("OutputFolder", IsRequired = false, DefaultValue = "")]
      public string OutputFolder
      {
         get { return (string)this["OutputFolder"]; }
         set { this["OutputFolder"] = value; }
      }
   }
}
