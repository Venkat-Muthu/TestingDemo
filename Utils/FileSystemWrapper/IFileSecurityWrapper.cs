using System.Security.AccessControl;

namespace Utils.FileSystemWrapper
{
    public interface IFileSecurityWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileSecurity">A FileSecurity object.</param>
        void Initialize(FileSecurity fileSecurity);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets <see cref="T:System.Security.AccessControl.DirectorySecurity"/> object.
        /// </summary>
        FileSecurity FileSecurityInstance { get; }

        void SetAccessRuleProtection(bool isProtected, bool preserveInheritance);

        void AddAccessRule(FileSystemAccessRule rule);
    }
}