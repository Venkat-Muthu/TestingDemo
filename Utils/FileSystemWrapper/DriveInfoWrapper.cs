using System.IO;

namespace Utils.FileSystemWrapper
{
    public interface IDriveInfoWrapper
    {
        DriveInfo[] GetDrives();
    }

    public class DriveInfoWrapper : IDriveInfoWrapper
    {
        public DriveInfo[] GetDrives()
        {
            return DriveInfo.GetDrives();
        }
    }


}