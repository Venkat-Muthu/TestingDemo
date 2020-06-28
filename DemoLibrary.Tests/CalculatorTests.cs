using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private ICalculator _calculator;
        [SetUp]
        public void Initialize()
        {
            _calculator = new Calculator();
        }

        [TestCase(20, 40, 60)]
        public void Add_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange
            
            // Act
            var actual = _calculator.Add(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, -20)]
        public void Subtract_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = _calculator.Subtract(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, 800)]
        public void Multiply_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = _calculator.Multiply(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, 0.5)]
        public void Divide_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = _calculator.Divide(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
