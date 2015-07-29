using System.IO;
using System.Text;

namespace Tundra.Writers
{
    /// <summary>
    /// Utf8 String Writer Class
    /// </summary>
    public class Utf8StringWriter : StringWriter
    {
        /// <summary>
        /// Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.
        /// </summary>
        /// <returns>The Encoding in which the output is written.</returns>
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}