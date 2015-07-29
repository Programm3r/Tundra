using Tundra.Interfaces.Platform;

namespace Tundra.Mock
{
    /// <summary>
    /// Design Device Information Class Declaration
    /// </summary>
    /// <remarks>
    /// Has no use what so ever besides the fact that it aids with design time development
    /// </remarks>
    public class MockDeviceInformation : IDeviceInformation
    {
        /// <summary>
        /// Gets the device identifier.
        /// </summary>
        /// <returns>
        /// the unique device identifier
        /// </returns>
        public string GetDeviceId()
        {
            return string.Empty;
        }
    }
}