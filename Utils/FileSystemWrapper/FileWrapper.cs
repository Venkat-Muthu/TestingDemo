using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Utils.FileSystemWrapper
{
    public interface IFileWrapper
    {
        void Copy(string sourceFileName, string destFileName);
        void Copy(string sourceFileName, string destFileName, bool overwrite);
        void Delete(string path);
        Task DeleteFilesAsync(string directoryPath, string commaSeperatedExtensions = "*.*");
        bool Exists(string path);
        string ReadAllText(string path);

        string ReadAllText(string path, Encoding encoding);
        string[] ReadAllLines(string path);
        byte[] ReadAllBytes(string path);
        void WriteAllText(string path, string contents);
        void WriteAllText(string path, string contents, Encoding encoding);
        void WriteAllBytes(string path, byte[] bytes);
        void WriteAllLines(string path, string[] contents);
        void WriteAllLines(string path, IEnumerable<string> contents);
        long GetFileLength(string path);
        FileStream Open(string path, FileMode mode);
        FileStream OpenRead(string path);
        FileStream Create(string path, int bufferSize, FileOptions options);
        void SaveXElement(XElement xElementToSave, string fileName);

        DateTime GetCreationTimeUtc(string filePath);
        DateTime GetLastWriteTimeUtc(string filePath);
        IFileSecurityWrapper GetAccessControl(string path);
        void SetAccessControl(string path, IFileSecurityWrapper fileSecurityWrapper);
        FileMode FileMode { get; set; }
    }

    public class FileWrapper : IFileWrapper
    {
        public void Copy(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public Task DeleteFilesAsync(string directoryPath, string commaSeperatedExtensions = "*.*")
        {
            return Task.Factory.StartNew(() =>
            {
                if (Directory.Exists(directoryPath))
                {
                    var extn = commaSeperatedExtensions;
                    var files = extn.Split(',').SelectMany(ext => Directory
                        .GetFiles(directoryPath, ext, SearchOption.AllDirectories)).ToList();

                    var exceptions = new List<Exception>();
                    foreach (var file in files)
                    {
                        try
                        {
                            var path = file;
                            File.Delete(path);
                        }
                        catch (Exception exception)
                        {
                            exceptions.Add(new Exception($"File delete failed: {file}", exception));
                        }
                    }
                    if (exceptions.Any())
                        throw new AggregateException(
                            $"Deletion of files failed for {extn} extension in {directoryPath} folder", exceptions);
                }
            });
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public void WriteAllLines(string path, string[] contents)
        {
            File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(string path, IEnumerable<string> contents)
        {
            File.WriteAllLines(path, contents);
        }

        public long GetFileLength(string path)
        {
            var length = 0L;
            if (File.Exists(path))
            {
                length = new FileInfo(path).Length;
            }
            return length;
        }

        public FileStream Open(string path, FileMode mode)
        {
            return File.Open(path, mode);
        }

        public FileStream OpenRead(string path)
        {
            return File.OpenRead(path);
        }

        public FileStream Create(string path, int bufferSize, FileOptions options)
        {
            return File.Create(path, bufferSize, options);
        }

        public void SaveXElement(XElement xElementToSave, string fileName)
        {
            xElementToSave.Save(fileName);
        }

        public DateTime GetCreationTimeUtc(string filePath)
        {
            return File.GetCreationTimeUtc(filePath);
        }

        public DateTime GetLastWriteTimeUtc(string filePath)
        {
            return File.GetLastWriteTimeUtc(filePath);
        }

        public IFileSecurityWrapper GetAccessControl(string path)
        {
            return new FileSecurityWrapper(File.GetAccessControl(path));
        }

        public void SetAccessControl(string path, IFileSecurityWrapper fileSecurityWrapper)
        {
            File.SetAccessControl(path, fileSecurityWrapper.FileSecurityInstance);
        }

        public FileMode FileMode { get; set; }
    }
}
