using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utils.FileSystemWrapper
{
    public interface IDirectoryWrapper
    {
        string CreateDirectory(string path);
        bool Exists(string path);
        string[] GetFiles(string path);
        string[] GetFiles(string path, string searchPattern);
        string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
        string[] GetDirectories(string path);
        string[] GetDirectories(string path, string searchPattern);
        string[] GetDirectories(string path, string searchPattern, SearchOption searchOption);
        string[] GetFileSystemEntries(string path);
        IEnumerable<string> EnumerateDirectories(string path);
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern);
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption);
        IEnumerable<string> EnumerateFiles(string path);
        IEnumerable<string> EnumerateFiles(string path, string searchPattern);
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
        IEnumerable<string> EnumerateFileSystemEntries(string path);
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern);
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption);
        string[] GetLogicalDrives();
        string GetDirectoryRoot(string path);
        string GetCurrentDirectory();
        void SetCurrentDirectory(string path);
        void Move(string sourceDirName, string destDirName);
        void Delete(string path);
        void Delete(string path, bool recursive);
        IEnumerable<IFileInfoWrapper> EnumerateFilesWithInfo(string path, string searchPattern, SearchOption searchOption);
        string Combine(string path1, string path2);
        string Combine(string path1, string path2, string path3);
        string Combine(string path1, string path2, string path3, string path4);
        string Combine(params string[] paths);
        List<string> GetDirectoriesName(string path);
        string GetDirectoryOnly(string fileOrDirectoryPath);
        bool IsDirectory(string path);
    }

    public class DirectoryWrapper : IDirectoryWrapper
    {
        public string CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path).FullName;
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);

        }

        public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public string[] GetDirectories(string path, string searchPattern)
        {
            return Directory.GetDirectories(path, searchPattern);
        }

        public string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        public string[] GetFileSystemEntries(string path)
        {
            return Directory.GetFileSystemEntries(path);
        }

        public IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            return Directory.EnumerateDirectories(path, searchPattern);
        }

        public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }

        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public IEnumerable<IFileInfoWrapper> EnumerateFilesWithInfo(string path, string searchPattern, SearchOption searchOption)
        {
            var enumerateFiles = new DirectoryInfo(path).EnumerateFiles(searchPattern, searchOption);
            return enumerateFiles.Select(enumerateFile => new FileInfoWrapper(enumerateFile));
        }

        public string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2) + Path.DirectorySeparatorChar;
        }

        public string Combine(string path1, string path2, string path3)
        {
            return Path.Combine(path1, path2, path3) + Path.DirectorySeparatorChar;
        }

        public string Combine(string path1, string path2, string path3, string path4)
        {
            return Path.Combine(path1, path2, path3, path4) + Path.DirectorySeparatorChar;
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths) + Path.DirectorySeparatorChar;
        }

        [Obsolete("Can't EnumerateFiles be used?")]
        public List<string> GetDirectoriesName(string path)
        {
            var directoryInfos = new DirectoryInfo(path).GetDirectories();
            var subfolders = directoryInfos.Select(d => d.FullName).ToList();
            return subfolders;
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return Directory.EnumerateFiles(path, searchPattern);
        }

        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path)
        {
            return Directory.EnumerateFileSystemEntries(path);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
        {
            return Directory.EnumerateFileSystemEntries(path, searchPattern);
        }

        public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption);
        }

        public string[] GetLogicalDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public string GetDirectoryRoot(string path)
        {
            return Directory.GetDirectoryRoot(path);
        }

        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }

        public void Move(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

        public void Delete(string path)
        {
            Directory.Delete(path);
        }

        public void Delete(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public string GetDirectoryOnly(string fileOrDirectoryPath)
        {
            var result = fileOrDirectoryPath;
            if (!IsDirectory(fileOrDirectoryPath))
                result = Path.GetDirectoryName(fileOrDirectoryPath);
            return result;
        }

        public bool IsDirectory(string path)
        {
            var fileAttribute = File.GetAttributes(path);
            return fileAttribute.HasFlag(FileAttributes.Directory);
        }
    }
}
