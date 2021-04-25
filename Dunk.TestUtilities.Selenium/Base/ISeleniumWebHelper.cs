using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Dunk.TestUtilities.Selenium.Base
{
    /// <summary>
    /// An interface that defines the behaviour of a disposable helper
    /// class over the Selenium <see cref="IWebDriver"/>
    /// </summary>
    public interface ISeleniumWebHelper : IDisposable
    {
        /// <summary>
        /// Gets the base url associated with the web-site being tested.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Gets the underling Selenium <see cref="IWebDriver"/> asssociated
        /// with this class.
        /// </summary>
        IWebDriver WebDriver { get; }

        /// <summary>
        /// Navigates to the base url.
        /// </summary>
        void NavigateToBaseUrl();

        /// <summary>
        /// Checks the current web-page for any Java-Script errors.
        /// </summary>
        /// <returns>
        /// A collection containing any Java-Script errors, if any.
        /// </returns>
        IEnumerable<LogEntry> CheckForJavaScriptErrors();

        /// <summary>
        /// Checks the current web-page for any Java-Script warnings.
        /// </summary>
        /// <returns>
        /// A collection containing any Java-Script warnings, if any.
        /// </returns>
        IEnumerable<LogEntry> CheckForJavaScriptWarnings();

        /// <summary>
        /// Checks the current web-page for any Java-Script logs of a 
        /// specified level.
        /// </summary>
        /// <param name="level">The level of the logs we are interested in.</param>
        /// <returns>
        /// A collection containing any Java-Script of the specified level, if any.
        /// </returns>
        IEnumerable<LogEntry> CheckJavaScriptLogs(LogLevel level);
    }
}
