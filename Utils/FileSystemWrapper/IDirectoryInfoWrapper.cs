using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utils.FileSystemWrapper
{
    public interface IDirectoryInfoWrapper
    {
        string Name { get; }
        string FullName { get; }
        IDirectoryInfoWrapper Parent { get; }
        IDirectoryInfoWrapper CreateSubdirectory(string path);
        void Create();
        List<IFileInfoWrapper> GetFiles(string searchPattern);
        List<IFileInfoWrapper> GetFiles(string searchPattern, SearchOption searchOption);
        List<IFileInfoWrapper> GetFiles();
        List<IDirectoryInfoWrapper> GetDirectories();
        List<IDirectoryInfoWrapper> GetDirectories(string searchPattern);
        void Delete();
        void Delete(bool recursive);
    }

    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly DirectoryInfo _directoryInfo;

        public DirectoryInfoWrapper(string path)
        {
            _directoryInfo = new DirectoryInfo(path);
        }
        public DirectoryInfoWrapper(DirectoryInfo  directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }
        public string Name => _directoryInfo.Name;
        public string FullName => _directoryInfo.FullName;
        public IDirectoryInfoWrapper Parent => new DirectoryInfoWrapper(_directoryInfo?.Parent?.FullName);
        public IDirectoryInfoWrapper CreateSubdirectory(string path)
        {
            return new DirectoryInfoWrapper(_directoryInfo.CreateSubdirectory(path));
        }

        public void Create()
        {
            _directoryInfo.Create();
        }

        public List<IFileInfoWrapper> GetFiles()
        {
            return GetFiles("*", SearchOption.TopDirectoryOnly);
        }

        public List<IFileInfoWrapper> GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, SearchOption.TopDirectoryOnly);
        }

        public List<IFileInfoWrapper> GetFiles(string searchPattern, SearchOption searchOption)
        {
            var simpleFileInfoWrappers = _directoryInfo.GetFiles(searchPattern, searchOption)
                .Select(f => new FileInfoWrapper(f)).ToList<IFileInfoWrapper>();
            return simpleFileInfoWrappers;
        }

        public List<IDirectoryInfoWrapper> GetDirectories()
        {
            return GetDirectories("*", SearchOption.TopDirectoryOnly);
        }

        public List<IDirectoryInfoWrapper> GetDirectories(string searchPattern)
        {
            return GetDirectories(searchPattern, SearchOption.TopDirectoryOnly);
        }

        public List<IDirectoryInfoWrapper> GetDirectories(string searchPattern, SearchOption searchOption)
        {
            var directories = new List<IDirectoryInfoWrapper>();
            foreach (var directory in _directoryInfo.GetDirectories())
            {
                var wrapper = new DirectoryInfoWrapper(directory.FullName);
                directories.Add(wrapper);
            }
            return directories;
        }

        public void Delete()
        {
            Delete(false);
        }
        public void Delete(bool recursive)
        {
            _directoryInfo.Delete(recursive);
        }
    }
}
