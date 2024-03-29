﻿using System.Configuration;

namespace EtfExtractor.Settings
{
    class TickersProviderSettings : ConfigurationSection
    {
        private static readonly TickersProviderSettings settings =
            ConfigurationManager.GetSection("TickersProviderSettings") as TickersProviderSettings;

        public static TickersProviderSettings Instance => settings;

        [ConfigurationProperty("FileUri", IsRequired = true)]
        public string FileUri
        {
            get { return (string)this["FileUri"]; }
            set { this["FileUri"] = value; }
        }

        [ConfigurationProperty("SheetName", IsRequired = true)]
        public string SheetName
        {
            get { return (string)this["SheetName"]; }
            set { this["SheetName"] = value; }
        }

        [ConfigurationProperty("TickerListRowIndex", DefaultValue = "2", IsRequired = true)]
        [IntegerValidator(MinValue = 0)]
        public int TickerListRowIndex
        {
            get { return (int)this["TickerListRowIndex"]; }
            set { this["TickerListRowIndex"] = value; }
        }

        [ConfigurationProperty("TickerListColumnIndex", DefaultValue ="1", IsRequired = true)]
        [IntegerValidator(MinValue = 0)]
        public int TickerListColumnIndex
        {
            get { return (int)this["TickerListColumnIndex"]; }
            set { this["TickerListColumnIndex"] = value; }
        }
    }
}
