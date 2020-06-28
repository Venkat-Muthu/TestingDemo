using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Utils.FileSystemWrapper;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class DataAccessWithBuilderTests
    {
        [TestCase(new string[] { }, "myFullPath", "PersonText.txt")]
        [TestCase(new[] { "F1,L1" }, "myFullPath", "PersonText.txt")]
        [TestCase(new[] { "F1,L1", "F2,L2" }, "myFullPath", "PersonText.txt")]
        public void GetAllPeople_DefaultProperties_ShouldReturnRows(string[] content, string fileFullPath, string filename)
        {
            var builder = new DataAccessBuilder(content, fileFullPath, filename);
            var dataAccess = builder.Build();

            var personModels = dataAccess.GetAllPeople();

            Assert.AreEqual(personModels.Count, content.Length);
        }

        [TestCase(new string[] { }, "myFullPath", "PersonText.txt")]
        [TestCase(new[] { "F1,L1" }, "myFullPath", "PersonText.txt")]
        [TestCase(new[] { "F1,L1", "F2,L2" }, "myFullPath", "PersonText.txt")]

        public void GetAllPeople_CustomProperties_ShouldReturnRows(string[] content, string fileFullPath, string filename)
        {
            // Customized to add one extra line in the txt file
            var contentList = new List<string>(content) {"F-Custom-1,L-Custom-1"};
            // If I want to override the default mocking values. For example, if default value is true, I can set false here...
            var fileWrapperMock = MockRepository.GenerateMock<IFileWrapper>();
            fileWrapperMock.Stub(stub => stub.Exists(Arg<string>.Is.Equal($"{fileFullPath}\\{filename}"))).Return(true).Repeat.Once();
            fileWrapperMock.Stub(stub => stub.ReadAllLines(Arg<string>.Matches(path => path.EndsWith("PersonText.txt")))).Return(contentList.ToArray()).Repeat.Once();

            var builder = new DataAccessBuilder(content, fileFullPath, filename);
            builder.WithFileWrapper(fileWrapperMock);
            var dataAccess = builder.Build();

            var personModels = dataAccess.GetAllPeople();

            Assert.AreEqual(personModels.Count, content.Length + 1);
        }
    }
}