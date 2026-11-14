using System;
using Unit8_SRP_F24;
using Unit8_SRP_F24.Contracts;

namespace Unit8_SRP_F24Tests.UnitTests;

public class dummyPositiveTradeValidator : ITradeValidator
    {
        public bool Validate(string[] tradeData)
        {
            return true;
        }
    }

    public class dummyNegativeTradeValidator : ITradeValidator
    {
        public bool Validate(string[] tradeData)
        {
            return false;
        }
    }

    public class dummyTradeMapper : ITradeMapper
    {
        public TradeRecord Map(string[] fields)
        {
            TradeRecord sampleRec = new TradeRecord();
            sampleRec.DestinationCurrency = "XXX";
            sampleRec.SourceCurrency = "YYY";
            sampleRec.Price = 1.11M;
            sampleRec.Lots = 2.22F;
            return sampleRec;
        }
    }

[TestClass]
public class SimpleTradeParserTests
{
    [TestMethod]
    public void TestPositiveValidation()
    {
        // Arrange
        ITradeValidator validator = new dummyPositiveTradeValidator();
        ITradeMapper mapper = new dummyTradeMapper();
        SimpleTradeParser parser = new SimpleTradeParser(validator, mapper);

        // Act
        IEnumerable<TradeRecord> trades = parser.ParseTrades(new List<string> { "AAABBB,1111,1.11", "CCCDDD,2222,2.22", "EEEFFF,3333,3.33" });

        // Assert
        Assert.AreEqual(3, trades.Count());
    }
    [TestMethod]
    public void TestNegativeValidation()
    {
        // Arrange
        ITradeValidator validator = new dummyNegativeTradeValidator();
        ITradeMapper mapper = new dummyTradeMapper();
        SimpleTradeParser parser = new SimpleTradeParser(validator, mapper);

        // Act
        IEnumerable<TradeRecord> trades = parser.ParseTrades(new List<string> { "AAABBB,1111,1.11", "CCCDDD,2222,2.22", "EEEFFF,3333,3.33" });

        // Assert
        Assert.AreEqual(0, trades.Count());
    }
}
