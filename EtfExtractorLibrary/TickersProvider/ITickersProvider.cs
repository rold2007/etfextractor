using System.Collections.Generic;

namespace EtfExtractorLibrary.TickersProvider
{
    internal interface ITickersProvider
    {
        IEnumerable<string> GetTickers();
    }
}