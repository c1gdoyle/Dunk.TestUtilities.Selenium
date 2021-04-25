using System;
using System.Collections.Generic;
using System.Linq;
using Dunk.TestUtilities.Selenium.Base;
using Dunk.TestUtilities.Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Dunk.TestUtilities.Selenium.Core
{
    /// <summary>
    /// An implementation of <see cref="ISeleniumWebHelper"/> that supporting testing
    /// a web-site using Google Chrome browser.
    /// </summary>
    /// <remarks>
    /// By default the driver associated with this class will start with the web-page maximised
    /// and with the default downloads folder set to the C:\TEMP\Download
    /// 
    /// These options can be overridden by supplying custom <see cref="ChromeOptions"/> to
    /// the constructor.
    /// </remarks>
    public class SeleniumChromeWebHelper : ISeleniumWebHelper
    {
        private readonly IWebDriver _driver;
        private readonly Uri _baseUri;

        private bool _isDisposed;

        #region Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, default options for the web-driver and a page-load timeout
        /// of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl)
            : this(baseUrl, GetDefaultOptions())
        {
        }

        ///<summary>
        /// Intialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, options for the web-driver and page-load timeout
        /// of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> or <paramref name="options"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, ChromeOptions options)
            : this(baseUrl, options, TimeSpan.FromSeconds(60))
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, a page-load timeout and default options for the web-driver.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, TimeSpan pageLoadTimeout)
            : this(baseUrl, GetDefaultOptions(), pageLoadTimeout)
        {
        }

        /// <summary>
        /// Intialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, options for the web-driver and page-load timeout.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> or <paramref name="options"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, ChromeOptions options, TimeSpan pageLoadTimeout)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumChromeWebHelper)}, {nameof(options)} cannot be null");
            }
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumChromeWebHelper)}, {nameof(baseUrl)} cannot be null");
            }

            _baseUri = new Uri(baseUrl);
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().PageLoad = pageLoadTimeout;
        }

        /// <summary>
        /// Intialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication,
        /// default options for the web-driver and page-load timeout of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/>, <paramref name="userName"/> or <paramref name="password"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, string userName, string password)
            : this(baseUrl, userName, password, GetDefaultOptions())
        {
        }

        /// <summary>
        /// Intialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication,
        /// options for the web-driver and page-load timeout of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> or <paramref name="options"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, string userName, string password, ChromeOptions options)
            : this(baseUrl, userName, password, options, TimeSpan.FromSeconds(60))
        {
        }

        /// <summary>
        /// Intialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication,
        /// page-load timeout and default options for the web-driver.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/>, <paramref name="userName"/> or <paramref name="password"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, string userName, string password, TimeSpan pageLoadTimeout)
            : this(baseUrl, userName, password, GetDefaultOptions(), pageLoadTimeout)
        {
        }

        /// <summary>
        /// Intialises a new instance of <see cref="SeleniumChromeWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication,
        /// options for the web-driver and page-load timeout.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> <paramref name="userName"/>, <paramref name="password"/> or <paramref name="options"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumChromeWebHelper(string baseUrl, string userName, string password, ChromeOptions options, TimeSpan pageLoadTimeout)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumChromeWebHelper)}, {nameof(options)} cannot be null");
            }
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumChromeWebHelper)}, {nameof(baseUrl)} cannot be null");
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumChromeWebHelper)}, {nameof(userName)} cannot be null");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumChromeWebHelper)}, {nameof(password)} cannot be null");
            }

            _baseUri = WindowsAuthenticationUrlParser.ParseWindowsAuthenticationUrl(baseUrl, userName, password);
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().PageLoad = pageLoadTimeout;
        }
        #endregion Constructors

        #region ISeleniumWebHelper Members
        /// <inheritdoc />
        public string BaseUrl { get { return _baseUri.ToString(); } }

        /// <inheritdoc />
        /// <remarks>
        /// In this case a <see cref="ChromeDriver"/> instance.
        /// </remarks>
        public IWebDriver WebDriver { get { return _driver; } }

        /// <inheritdoc />
        public void NavigateToBaseUrl()
        {
            WebDriver.Navigate().GoToUrl(BaseUrl);
        }

        /// <inheritdoc />
        public IEnumerable<LogEntry> CheckForJavaScriptErrors()
        {
            return CheckJavaScriptLogs(LogLevel.Severe);
        }

        /// <inheritdoc />
        public IEnumerable<LogEntry> CheckForJavaScriptWarnings()
        {
            return CheckJavaScriptLogs(LogLevel.Warning);
        }

        /// <inheritdoc />
        public IEnumerable<LogEntry> CheckJavaScriptLogs(LogLevel level)
        {
            return Extensions.WebDriverExtensions.GetBrowserLogs(WebDriver)
                .Where(x => x.Level == level);
        }
        #endregion ISeleniumWebHelper Members

        private static ChromeOptions GetDefaultOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            options.AddUserProfilePreference("domain.default_directory", @"C:\TEMP\Download");
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            return options;
        }

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
                _isDisposed = true;
            }
        }
        #endregion IDisposable Members

        ~SeleniumChromeWebHelper()
        {
            Dispose(false);
        }
    }
}
