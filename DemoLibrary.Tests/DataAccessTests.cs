using System.Collections.Generic;
using DemoLibrary.Models;
using NUnit.Framework;
using Rhino.Mocks;
using Utils.FileSystemWrapper;

namespace DemoLibrary.Tests
{
    [TestFixture]
    public class DataAccessTests
    {
        private IDataAccess _dataAccess;
        [SetUp]
        public void Initialize()
        {
            var fileWrapper = new FileWrapper() as IFileWrapper;
            var pathWrapper = new PathWrapper() as IPathWrapper;
            _dataAccess = new DataAccess(fileWrapper, pathWrapper);
        }

        [Test]
        public void GetAllPeople_ShouldReturnZeroRows()
        {
            // Arrange
            string[] content = new[] {"F1,L1", "F2,L2"};
            string filename = "PersonText.txt";
            string fileFullPath = "myFullPath";

            var fileWrapperMock = MockRepository.GenerateMock<IFileWrapper>();
            fileWrapperMock.Stub(stub => stub.Exists(Arg<string>.Is.Equal($"{fileFullPath}\\{filename}"))).Return(true).Repeat.Once();
            fileWrapperMock.Stub(stub => stub.ReadAllLines(Arg<string>.Matches(path => path.EndsWith("PersonText.txt")))).Return(content).Repeat.Once();

            var pathWrapperMock = MockRepository.GenerateMock<IPathWrapper>();
            pathWrapperMock.Stub(s => s.GetFullPath(Arg<string>.Is.Anything))
                .Return(null)
                .WhenCalled(_ =>
                {
                    var path = (string) _.Arguments[0];
                    fileFullPath = $"{fileFullPath}\\{path}";
                    _.ReturnValue = fileFullPath;
                });

            _dataAccess = new DataAccess(fileWrapperMock, pathWrapperMock);

            // Act
            var personModels = _dataAccess.GetAllPeople();

            //Assert
            Assert.AreEqual(personModels.Count, content.Length);
        }

        [Test]
        public void AddNewPerson_NormalScenario_ShouldSucceed()
        {
            //Arrange
            var personModel = new PersonModel {FirstName = "FN", LastName = "LN"};
            // Arrange
            List<string> content = new List<string>{ "F1,L1", "F2,L2" };
            string filename = "PersonText.txt";
            string fileFullPath = "myFullPath";

            var fileWrapperMock = MockRepository.GenerateMock<IFileWrapper>();
            fileWrapperMock.Stub(stub => stub.Exists(Arg<string>.Is.Equal($"{fileFullPath}\\{filename}"))).Return(true)
                .Repeat.Twice();
            fileWrapperMock
                .Stub(stub => stub.ReadAllLines(Arg<string>.Matches(path => path.EndsWith("PersonText.txt"))))
                .Return(content.ToArray()).Repeat.Once();

            fileWrapperMock
                .Stub(stub => stub.ReadAllLines(Arg<string>.Matches(path => path.EndsWith("PersonText.txt"))))
                .Return(null).Repeat.Once()
                .WhenCalled(_ =>
                {
                    var newFileContent = content;
                    newFileContent.Add($"{personModel.Name}");
                    _.ReturnValue = newFileContent.ToArray();
                });


            var pathWrapperMock = MockRepository.GenerateMock<IPathWrapper>();
            pathWrapperMock.Stub(s => s.GetFullPath(Arg<string>.Is.Anything))
                .Return(null)
                .WhenCalled(_ =>
                {
                    var path = (string)_.Arguments[0];
                    fileFullPath = $"myFullPath\\{path}";
                    _.ReturnValue = fileFullPath;
                });

            _dataAccess = new DataAccess(fileWrapperMock, pathWrapperMock);

            // Act
            _dataAccess.AddNewPerson(personModel);

            // Assert
            var personModels = _dataAccess.GetAllPeople();
            Assert.IsNotNull(personModels.Find(_ => _.Name.Contains(personModel.Name)));


        }
    }
}