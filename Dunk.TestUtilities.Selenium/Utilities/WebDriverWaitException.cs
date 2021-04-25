using System;
using System.Runtime.Serialization;

namespace Dunk.TestUtilities.Selenium.Utilities
{
    /// <summary>
    /// An exception that is thrown when a timeout for waiting for an element on a
    /// web-page to change state has been exceeded.
    /// </summary>
    [Serializable]
    public class WebDriverWaitException : Exception
    {
        /// <summary>
        /// Initialises a new default instance of <see cref="WebDriverWaitException"/>.
        /// </summary>
        public WebDriverWaitException()
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="WebDriverWaitException"/> with a specified
        /// error messsage.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WebDriverWaitException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="WebDriverWaitException"/> with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public WebDriverWaitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected WebDriverWaitException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
        }
    }
}
