using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [SetUp]
        public void Initialize()
        {

        }

        [TestCase(20, 40, 60)]
        public void Add_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange
            
            // Act
            var actual = Calculator.Add(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, -20)]
        public void Subtract_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Subtract(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, 800)]
        public void Multiply_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Multiply(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase(20, 40, 0.5)]
        public void Divide_WithInRange_ShouldToBeSuccess(double x, double y, double expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Divide(x, y);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
