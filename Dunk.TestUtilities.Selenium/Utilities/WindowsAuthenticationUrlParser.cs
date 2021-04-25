using System;
using System.Web;

namespace Dunk.TestUtilities.Selenium.Utilities
{
    /// <summary>
    /// A helper class responsible for combining a valid base url and combining
    /// it with a userName and password to support Windows Authentication
    /// </summary>
    internal static class WindowsAuthenticationUrlParser
    {
        /// <summary>
        /// Parses a valid url and combines it with a specified userName and password to
        /// support Windows Authentication.
        /// </summary>
        /// <param name="originalUrl">The original url.</param>
        /// <param name="userName">The userName for Windows Authentication.</param>
        /// <param name="password">The password for Windows Authentication.</param>
        /// <returns>
        /// A new url combining the original url, userName and password for Windows Authentication.
        /// Should be in the form of : http://username:password@url
        /// </returns>
        /// <exception cref="ArgumentException">Unable to determine schema of <paramref name="originalUrl"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="originalUrl"/>, <paramref name="userName"/> or <paramref name="password"/> was null or empty.</exception>
        internal static Uri ParseWindowsAuthenticationUrl(string originalUrl, string userName, string password)
        {
            UriBuilder uriBuilder = new UriBuilder(originalUrl);
            uriBuilder.UserName = HttpUtility.UrlEncode(userName);
            uriBuilder.Password = HttpUtility.UrlEncode(password);

            if (uriBuilder.Uri.IsDefaultPort)
            {
                var uriComponents = uriBuilder.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
                return new Uri(uriComponents);
            }
            return uriBuilder.Uri;
        }
    }
}
