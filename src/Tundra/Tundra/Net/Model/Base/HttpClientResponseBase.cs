using System;
using System.Runtime.Serialization;
using Tundra.Interfaces.Errors;

namespace Tundra.Net.Model.Base
{
    /// <summary>
    /// Http Client Response Base Class
    /// </summary>
    [DataContract]
    public abstract class HttpClientResponseBase : IErrorInformation
    {
        /// <summary>
        /// Gets a value indicating whether this instance was caused by a cancellation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancellation; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool Cancellation { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [DataMember]
        public Exception Error { get; set; }
    }
}