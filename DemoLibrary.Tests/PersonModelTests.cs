using DemoLibrary.Models;
using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class PersonModelTests
    {
        [Test]
        public void Name_FirstAndLastName_ShouldReturnAsName()
        {
            // Arrange
            string firstName = "Fn", lastName = "Ln";
            string expected = $"{firstName},{lastName}";
            var personModel = new PersonModel {FirstName = firstName, LastName = lastName };

            // Act
            var actual = personModel.Name;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}