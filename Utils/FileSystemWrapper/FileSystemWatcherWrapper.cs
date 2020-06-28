using System.IO;

namespace Utils.FileSystemWrapper
{
    public interface IFileSystemWatcher
    {
        bool EnableRaisingEvents { get; set; }
        string Filter { get; set; }
        bool IncludeSubdirectories { get; set; }
        int InternalBufferSize { get; set; }
        NotifyFilters NotifyFilter { get; set; }
        string Path { get; set; }
        WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType);
        WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout);
        event FileSystemEventHandler Changed;
        event FileSystemEventHandler Created;
        event FileSystemEventHandler Deleted;
        event ErrorEventHandler Error;
        event RenamedEventHandler Renamed;
        void Dispose();
    }
    [System.ComponentModel.DesignerCategory(@"Code")]
    public class FileSystemWatcher : System.IO.FileSystemWatcher, IFileSystemWatcher
    {

    }
}