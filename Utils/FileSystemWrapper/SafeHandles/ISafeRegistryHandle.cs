using Microsoft.Win32.SafeHandles;

namespace Utils.FileSystemWrapper.SafeHandles
{
    /// <summary>
    /// Interface that matches the decoration of the object
    /// <see cref="SafeRegistryHandle"/>.
    /// </summary>
    public interface ISafeRegistryHandle
    {
        /// <summary>
        /// Gets <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandleInstance"/> object.
        /// </summary>
        SafeRegistryHandle SafeRegistryHandleInstance { get; }
    }
}