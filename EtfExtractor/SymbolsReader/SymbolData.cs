using EtfExtractor.Settings;
using HtmlAgilityPack;

namespace EtfExtractor.SymbolsReader
{
    class SymbolData
    {
        private readonly string _missingQuoteValue;
        private readonly string _columnSeparator;

        internal SymbolData(string name, string htmlDocument, string missingQuoteValue, string columnSeparator)
        {
            this.Name = name;
            this._missingQuoteValue = missingQuoteValue;
            this._columnSeparator = columnSeparator;
            this.ParseHtmlDocument(htmlDocument);
        }

        internal string Name { get; set; }
        internal string DividendFrequency { get; set; }
        internal string PeRatio { get; set; }
        internal string Yield { get; set; }
        internal string MarketCapitalization { get; set; }

        private void ParseHtmlDocument(string htmlDocument)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlDocument);

            this.DividendFrequency = GetField(doc, "Div. Frequency:");
            this.PeRatio = GetField(doc, "P/E Ratio:");
            this.Yield = GetField(doc, "Yield:");
            this.MarketCapitalization = ReplaceIllegalChars(GetField(doc, "Market Cap:"));
        }

        private string ReplaceIllegalChars(string s)
        {
            return s.Replace(",", "").Replace(".", "");
        }

        private string GetField(HtmlDocument doc, string label)
        {
            HtmlNode labelNode = doc.DocumentNode.SelectSingleNode(
                $"//table[@class='detailed-quote-table']//td[text()='{label}']");
            return labelNode == null ? this._missingQuoteValue : HtmlEntity.DeEntitize(GetNextTdValue(labelNode));
        }

        private string GetNextTdValue(HtmlNode dtNode)
        {
            HtmlNode found = dtNode.SelectSingleNode("following-sibling::*[1][self::td]");
            return found?.InnerText.Trim() ?? this._missingQuoteValue;
        }

        public override string ToString()
        {
            return $"{Name}{_columnSeparator}{DividendFrequency}{_columnSeparator}{PeRatio}{_columnSeparator}{Yield}{_columnSeparator}{MarketCapitalization}";
        }

        public static string GetHeader()
        {
            string separator = SymbolsReaderSettings.Instance.ColumnSeparator;
            return $"Name{separator}DividendFrequency{separator}PeRatio{separator}Yield{separator}MarketCapitalization";
        }
    }
}