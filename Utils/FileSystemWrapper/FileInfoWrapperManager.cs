namespace Utils.FileSystemWrapper
{
    public interface IFileInfoWrapperManager
    {
        IFileInfoWrapper Create(string fileName);
    }

    public class FileInfoWrapperManager : IFileInfoWrapperManager
    {
        public IFileInfoWrapper Create(string fileName)
        {
            return new FileInfoWrapper(fileName);
        }
    }

}