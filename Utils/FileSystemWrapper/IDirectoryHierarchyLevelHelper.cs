namespace Utils.FileSystemWrapper
{
    public interface IDirectoryHierarchyLevelHelper
    {
        int GetFileHierarchyDeepLevel(string referenceFolder, string filePath);
    }
}