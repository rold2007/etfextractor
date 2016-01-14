using System.Collections.Generic;

namespace EtfExtractor.TickersProvider
{
    internal interface ITickersProvider
    {
        IEnumerable<string> GetTickers();
    }
}