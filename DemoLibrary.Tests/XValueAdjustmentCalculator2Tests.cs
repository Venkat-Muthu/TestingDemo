using NodaTime;
using NodaTime.Testing;
using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class XValueAdjustmentCalculator2Tests
    {
        private IClock _clock;
        private IXValueAdjustmentCalculator _xValueAdjustmentCalculator;

        [TestCase(10, 3, ExpectedResult = 7)]
        public double Calculate_PreBasel3_ShouldApplyMinus(double x, double y)
        {
            _clock = new FakeClock(Instant.FromUtc(2020, 1, 1, 0, 0));
            _xValueAdjustmentCalculator = new XValueAdjustmentCalculator2(_clock);
            return _xValueAdjustmentCalculator.Calculate(x, y);
        }

        [TestCase(10, 3, ExpectedResult = 13)]
        public double Calculate_Basel3_ShouldApplyPlus(double x, double y)
        {
            _clock = new FakeClock(Instant.FromUtc(2022, 1, 1, 0, 0));
            _xValueAdjustmentCalculator = new XValueAdjustmentCalculator2(_clock);
            return _xValueAdjustmentCalculator.Calculate(x, y);
        }

    }
}