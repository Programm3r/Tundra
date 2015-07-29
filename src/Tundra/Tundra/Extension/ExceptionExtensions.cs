using System;

namespace Tundra.Extension
{
    /// <summary>
    /// Exception Extension Methods
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static string GetExceptionMessage(this Exception exception)
        {
            var realerror = exception;
            while (realerror.InnerException != null)
            {
                realerror = realerror.InnerException;
            }
            return realerror.Message;
        }
    }
}