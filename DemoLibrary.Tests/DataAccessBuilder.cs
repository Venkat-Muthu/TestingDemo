using NUnit.Framework;
using Rhino.Mocks;
using Utils.FileSystemWrapper;

namespace DemoLibrary.Tests
{
    public class DataAccessBuilder
    {
        public IFileWrapper FileWrapper { get; private set; }
        public IPathWrapper PathWrapper { get; private set; }

        public DataAccessBuilder(string[] content, string fileFullPath, string filename)
        {
            FileWrapper = MockRepository.GenerateMock<IFileWrapper>();
            FileWrapper.Stub(stub => stub.Exists(Arg<string>.Is.Equal($"{fileFullPath}\\{filename}"))).Return(true).Repeat.Once();
            FileWrapper.Stub(stub => stub.ReadAllLines(Arg<string>.Matches(path => path.EndsWith("PersonText.txt")))).Return(content).Repeat.Once();

            PathWrapper = MockRepository.GenerateMock<IPathWrapper>();
            PathWrapper.Stub(s => s.GetFullPath(Arg<string>.Is.Anything))
                .Return(null)
                .WhenCalled(_ =>
                {
                    var path = (string)_.Arguments[0];
                    fileFullPath = $"{fileFullPath}\\{path}";
                    _.ReturnValue = fileFullPath;
                });
        }
        public static implicit operator DataAccess(DataAccessBuilder instance)
        {
            return instance.Build();
        }

        public DataAccess Build()
        {
            var dataAccess = new DataAccess(FileWrapper, PathWrapper);
            return dataAccess;
        }

        public DataAccessBuilder WithFileWrapper(IFileWrapper fileWrapper)
        {
            FileWrapper = fileWrapper;
            return this;
        }

        public DataAccessBuilder WithPathWrapper(IPathWrapper pathWrapper)
        {
            PathWrapper = pathWrapper;
            return this;
        }
    }
}