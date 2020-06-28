using System;
using System.Reactive.Concurrency;
using System.Threading;
using NUnit.Framework;
using Reactive.EventAggregator;
using Rhino.Mocks;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class TickerMonitorTests
    {
        private IScheduler _scheduler;
        private IEventAggregator _eventAggregator;
        private ITickerMonitor _tickerMonitor;
        [SetUp]
        public void Initialize()
        {
            _scheduler = Scheduler.Default;
            _eventAggregator = MockRepository.GenerateMock<IEventAggregator>();
            _tickerMonitor = new TickerMonitor(_eventAggregator, _scheduler);
        }

        [Test]
        public void Ticker_OnStart_ShouldPublishEvents()
        {
            var finishedEvent = new ManualResetEventSlim();
            _eventAggregator.Expect(e => e.Publish(Arg<Ticker>.Is.Anything))
                .Repeat.AtLeastOnce()
                .WhenCalled(_ =>
                {
                    _tickerMonitor.Stop();
                    finishedEvent.Set();
                });
            _tickerMonitor.Start();

            finishedEvent.Wait();
            _eventAggregator.VerifyAllExpectations();
        }

        [Test]
        public void Ticker_AfterStop_ShouldNotPublishEvents()
        {
            var finishedEvent = new ManualResetEventSlim();
            var @event = finishedEvent;
            _eventAggregator.Expect(e => e.Publish(Arg<Ticker>.Is.Anything))
                .Repeat.AtLeastOnce()
                .WhenCalled(_ =>
                {
                    _tickerMonitor.Stop();
                    @event.Set();
                });
            _tickerMonitor.Start();

            var wait = finishedEvent.Wait(TimeSpan.FromMilliseconds(10000));
            _eventAggregator.VerifyAllExpectations();
            Assert.IsTrue(wait);

            // reset
            _eventAggregator.BackToRecord();
            _eventAggregator.Replay();

            finishedEvent = new ManualResetEventSlim();
            _eventAggregator.Expect(e => e.Publish(Arg<Ticker>.Is.Anything))
                .Repeat.Never()
                .WhenCalled(_ =>
                {
                    _tickerMonitor.Stop();
                    finishedEvent.Set();
                });

            wait = finishedEvent.Wait(TimeSpan.FromMilliseconds(500));
            _eventAggregator.VerifyAllExpectations();
            Assert.IsFalse(wait);
        }

        [Test]
        public void Ticker_NoStart_ShouldNotPublishEvents()
        {
            var finishedEvent = new ManualResetEventSlim();
            _eventAggregator.Expect(e => e.Publish(Arg<Ticker>.Is.Anything))
                .Repeat.Never()
                .WhenCalled(_ =>
                {
                    _tickerMonitor.Stop();
                    finishedEvent.Set();
                });

            var wait = finishedEvent.Wait(TimeSpan.FromMilliseconds(500));
            _eventAggregator.VerifyAllExpectations();
            Assert.IsFalse(wait);
        }

    }
}