namespace Utils.FileSystemWrapper
{
    public interface IDirectoryInfoWrapperManager
    {
        IDirectoryInfoWrapper Create(string path);
    }

    public class DirectoryInfoWrapperManager : IDirectoryInfoWrapperManager
    {
        public IDirectoryInfoWrapper Create(string path)
        {
            return new DirectoryInfoWrapper(path);
        }
    }
}