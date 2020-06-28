namespace DemoLibrary
{
    public class Ticker
    {
        public string Symbol { get; }
        public string Exchange { get; }
        public double AskPrice { get; }
        public double BidPrice { get; }

        public Ticker(string symbol, string exchange, double bidPrice, double askPrice)
        {
            Symbol = symbol;
            Exchange = exchange;
            BidPrice = bidPrice;
            AskPrice = askPrice;
        }
    }
}