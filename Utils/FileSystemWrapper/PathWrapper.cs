using System;
using System.IO;

namespace Utils.FileSystemWrapper
{
    public interface IPathWrapper
    {
        char DirectorySeparatorChar { get; set; }
        char AltDirectorySeparatorChar { get; set; }
        char VolumeSeparatorChar { get; set; }
        char PathSeparator { get; }
        string ChangeExtension(string path, string extension);
        string GetDirectoryName(string path);
        char[] GetInvalidPathChars();
        char[] GetInvalidFileNameChars();
        string GetExtension(string path);
        string GetFullPath(string path);
        string GetFileName(string path);
        string GetFileNameWithoutExtension(string path);
        string GetCommonApplicationDataPath();
        string GetPathRoot(string path);
        string GetTempPath();
        string GetRandomFileName();
        string GetTempFileName();
        bool HasExtension(string path);
        bool IsPathRooted(string path);
        string Combine(string path1, string path2);
        string Combine(string path1, string path2, string path3);
        string Combine(string path1, string path2, string path3, string path4);
        string Combine(params string[] paths);
    }

    public class PathWrapper : IPathWrapper
    {
        public PathWrapper()
        {
            PathSeparator = Path.PathSeparator;
        }

        public char DirectorySeparatorChar
        {
            get => Path.DirectorySeparatorChar;
            set => throw new NotImplementedException();
        }

        public char AltDirectorySeparatorChar
        {
            get => Path.AltDirectorySeparatorChar;
            set => throw new NotImplementedException();
        }

        public char VolumeSeparatorChar
        {
            get => Path.VolumeSeparatorChar;
            set => throw new NotImplementedException();
        }

        public char PathSeparator { get; private set; }

        public string ChangeExtension(string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        public char[] GetInvalidFileNameChars()
        {
            return Path.GetInvalidFileNameChars();
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string GetCommonApplicationDataPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
        }

        public string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }

        public string GetTempPath()
        {
            return Path.GetTempPath();
        }

        public string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        public string GetTempFileName()
        {
            return Path.GetTempFileName();
        }

        public bool HasExtension(string path)
        {
            return Path.HasExtension(path);
        }

        public bool IsPathRooted(string path)
        {
            return Path.IsPathRooted(path);
        }

        public string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public string Combine(string path1, string path2, string path3)
        {
            return Path.Combine(path1, path2, path3);
        }

        public string Combine(string path1, string path2, string path3, string path4)
        {
            return Path.Combine(path1, path2, path3, path4);
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }
    }
}
