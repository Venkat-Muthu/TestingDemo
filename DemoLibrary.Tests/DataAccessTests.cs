using DemoLibrary.Models;
using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class DataAccessTests
    {
        [SetUp]
        public void Initialize()
        {

        }

        [Test, Order(1)]
        public void GetAllPeople_ShouldReturnZeroRows()
        {
            var personModels = DataAccess.GetAllPeople();

            Assert.Zero(personModels.Count);
        }

        [Test, Order(2)]
        public void AddNewPerson_NormalScenario_ShouldSucceed()
        {
            //Arrange
            var personModel = new PersonModel {FirstName = "FN", LastName = "LN"};

            // Act
            DataAccess.AddNewPerson(personModel);

            // Assert
            var personModels = DataAccess.GetAllPeople();
            Assert.AreEqual(1, personModels.Count);


        }
    }
}