using System.Security.AccessControl;

namespace Utils.FileSystemWrapper
{
    public class FileSecurityWrapper : IFileSecurityWrapper
    {
        private FileSecurity _fileSecurity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileSecurity">A FileSecurity object.</param>
        public FileSecurityWrapper(FileSecurity fileSecurity)
        {
            Initialize(fileSecurity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileSecurity">A FileSecurity object.</param>
        public void Initialize(FileSecurity fileSecurity)
        {
            _fileSecurity = fileSecurity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        public FileSecurityWrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        public void Initialize()
        {
            _fileSecurity = new FileSecurity();
        }

        /// <inheritdoc />
        public FileSecurity FileSecurityInstance => _fileSecurity;

        public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
        {
            _fileSecurity.SetAccessRuleProtection(isProtected, preserveInheritance);
        }

        public void AddAccessRule(FileSystemAccessRule rule)
        {
            _fileSecurity.AddAccessRule(rule);
        }
    }
}