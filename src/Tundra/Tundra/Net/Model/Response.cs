﻿using System.Runtime.Serialization;
using Tundra.Net.Model.Base;

namespace Tundra.Net.Model
{
    /// <summary>
    /// Response Base Class
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    [DataContract]
    public class Response<TData> : HttpClientResponseBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the it was a success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if successful; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember]
        public TData Data { get; set; }
    }
}