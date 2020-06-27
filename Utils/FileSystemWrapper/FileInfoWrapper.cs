using System;
using System.IO;

namespace Utils.FileSystemWrapper
{
    public interface IFileInfoWrapper
    {
        string FullName { get; }
        string Extension { get; }
        string Name { get; }
        string DirectoryName { get; }
        bool IsReadOnly { get; }
        bool Exists { get; }
        long Length { get; }
        DateTime CreationTime { get; set; }
        DateTime CreationTimeUtc { get; set; }
        DateTime LastAccessTime { get; set; }
        DateTime LastAccessTimeUtc { get; set; }
        DateTime LastWriteTime { get; set; }
        DateTime LastWriteTimeUtc { get; set; }
    }

    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly FileInfo _fileInfo;
        public FileInfoWrapper(string fileName)
        {
            _fileInfo = new FileInfo(fileName);
        }
        public FileInfoWrapper(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public string FullName => _fileInfo.FullName;

        public string Extension => _fileInfo.Extension;

        public string Name => _fileInfo.Name;

        public string DirectoryName => _fileInfo.DirectoryName;

        public bool IsReadOnly => _fileInfo.IsReadOnly;

        public bool Exists => _fileInfo.Exists;

        public long Length => _fileInfo.Length;

        public DateTime CreationTime
        {
            get => _fileInfo.CreationTime;
            set => _fileInfo.CreationTime = value;
        }

        public DateTime CreationTimeUtc
        {
            get => _fileInfo.CreationTimeUtc;
            set => _fileInfo.CreationTimeUtc = value;
        }

        public DateTime LastAccessTime
        {
            get => _fileInfo.CreationTimeUtc;
            set => _fileInfo.CreationTimeUtc = value;
        }

        public DateTime LastAccessTimeUtc
        {
            get => _fileInfo.LastAccessTimeUtc;
            set => _fileInfo.LastAccessTimeUtc = value;
        }

        public DateTime LastWriteTime
        {
            get => _fileInfo.LastWriteTime;
            set => _fileInfo.LastWriteTime = value;
        }

        public DateTime LastWriteTimeUtc
        {
            get => _fileInfo.LastWriteTimeUtc;
            set => _fileInfo.LastWriteTimeUtc = value;
        }
    }
}