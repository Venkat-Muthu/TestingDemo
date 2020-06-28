using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Reactive.EventAggregator;

namespace DemoLibrary
{
    public class TickerMonitor : ITickerMonitor
    {
        private static readonly Random RdmPrice = new Random();
        private static readonly Random RdmSymbol = new Random();
        private IDisposable _subscribe;
        private readonly IEventAggregator _eventAggregator;
        private readonly IScheduler _scheduler;

        public TickerMonitor(IEventAggregator eventAggregator, IScheduler scheduler = null)
        {
            _eventAggregator = eventAggregator;
            _scheduler = scheduler ?? Scheduler.Default;
        }

        public void RepeatAction(Timestamped<long> _)
        {
            var bidPrice = RdmPrice.Next(1, 20000);
            var askPrice = bidPrice - (bidPrice * 0.0001);
            var ticker = new Ticker(RandomString(3), "FTSE", bidPrice, askPrice);
            _eventAggregator.Publish(ticker);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RdmSymbol.Next(s.Length)]).ToArray());
        }
        
        public void Start()
        {
            _subscribe = Observable.Interval(TimeSpan.FromMilliseconds(1000), _scheduler)
                .Timestamp()
                .Subscribe(RepeatAction);
        }

        public void Stop()
        {
            _subscribe?.Dispose();
            _subscribe = null;
        }
    }
}