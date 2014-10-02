using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CSharpMultiPartPostHandler.RestClient
{
    public class CSharpMultiPartPostHandlerException : Exception
    {
        public CSharpMultiPartPostHandlerException(HttpStatusCode statusCode, string message = null, IEnumerable<string> errors = null, Exception innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Errors = (errors == null)
                ? new System.Collections.ObjectModel.ReadOnlyCollection<string>(new List<string>())
                : new System.Collections.ObjectModel.ReadOnlyCollection<string>(errors.ToList());
        }

        /// <summary>
        /// Gets the <see cref="HttpStatusCode"/> of the error.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the extra error messages returned from the server (if any).
        /// </summary>
        public IReadOnlyCollection<string> Errors { get; private set; }
    }



}