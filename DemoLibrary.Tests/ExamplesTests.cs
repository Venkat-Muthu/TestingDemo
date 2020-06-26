using System.IO;
using NUnit.Framework;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class ExamplesTests
    {
        [Test]
        public void ExampleLoadTextFile_ValidFile_ShouldValidateSucceefully()
        {
            // Assign
            var file = "Abcdefghilj123456789";

            // Act
            var actual = Examples.ExampleLoadTextFile(file);

            // Assert
            Assert.AreEqual(Examples.SuccessMessage, actual);
        }

        [Test]
        public void ExampleLoadTextFile_InValidFile_ShouldValidateWithException()
        {
            // Assign
            var file = "";

            // Act

            // Assert
            Assert.Throws<FileNotFoundException>(() => Examples.ExampleLoadTextFile(file));
        }
    }
}