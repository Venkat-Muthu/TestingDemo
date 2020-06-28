using System.Threading.Tasks;
using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class CalculatorAsyncTests
    {
        private ICalculator _calculator;
        [SetUp]
        public void Initialize()
        {
            _calculator = new Calculator();
        }

        [TestCase(20, 40, 60)]
        public async Task AddAsync_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = await _calculator.AddAsync(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, -20)]
        public async Task SubtractAsync_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = await _calculator.SubtractAsync(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, 800)]
        public async Task MultiplyAsync_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = await _calculator.MultiplyAsync(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, 0.5)]
        public async Task Divide_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = await _calculator.DivideAsync(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}