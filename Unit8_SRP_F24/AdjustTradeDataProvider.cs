using System;
using Unit8_SRP_F24.Contracts;

namespace Unit8_SRP_F24;

public class AdjustTradeDataProvider : ITradeDataProvider
{
    ITradeDataProvider _tradeDataProvider;

    public AdjustTradeDataProvider(ITradeDataProvider tradeDataProvider)
    {
        _tradeDataProvider = tradeDataProvider;
    }

    public async Task<IEnumerable<string>> ReadTradeDataAsync()
    {
        IEnumerable<string> tradeData = await _tradeDataProvider.ReadTradeDataAsync();
        
        // Convert every instance of "GBP" to "EUR" in the trade data
        List<string> adjustedTradeData = new List<string>();
        foreach (string line in tradeData)
        {
            adjustedTradeData.Add(line.Replace("GBP", "EUR"));
        }

        return adjustedTradeData;
    }
}
