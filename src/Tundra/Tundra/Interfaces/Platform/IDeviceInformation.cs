namespace Tundra.Interfaces.Platform
{
    /// <summary>
    /// Device Information Interface
    /// </summary>
    public interface IDeviceInformation
    {
        /// <summary>
        /// Gets the device identifier.
        /// </summary>
        /// <returns>the unique device identifier</returns>
        string GetDeviceId();
    }
}