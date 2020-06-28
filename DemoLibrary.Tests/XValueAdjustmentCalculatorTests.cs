using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class XValueAdjustmentCalculatorTests
    {
        private IXValueAdjustmentCalculator _xValueAdjustmentCalculator;

        [TestCase(10, 3, ExpectedResult = 7)]
        public double CalculateWithBuildInDate_PreBasel3_ShouldApplyMinus(double x, double y)
        {
            _xValueAdjustmentCalculator = new XValueAdjustmentCalculator();
            return _xValueAdjustmentCalculator.Calculate(x, y);
        }

        [Ignore("Cannot make it happen as it refers static object")]
        [TestCase(10, 3, ExpectedResult = 10)]
        public double CalculateWithBuildInDate_Basel3_ShouldApplyPlus(double x, double y)
        {
            _xValueAdjustmentCalculator = new XValueAdjustmentCalculator();
            return _xValueAdjustmentCalculator.Calculate(x, y);
        }
    }
}