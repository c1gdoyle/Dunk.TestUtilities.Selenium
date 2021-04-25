using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Dunk.TestUtilities.Selenium.Base;
using Dunk.TestUtilities.Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Dunk.TestUtilities.Selenium.Core
{
    /// <summary>
    /// An implementation of <see cref="ISeleniumWebHelper"/> that supporting testing
    /// a web-site using Internet Explorer browser.
    /// </summary>
    /// <remarks>
    /// By default the driver associated with this class will start with the web-page maximised.
    /// 
    /// These options can be overridden by supplying isMaximised <c>false</c> to the constructor.
    /// </remarks>
    public class SeleniumInternetExplorerWebHelper : ISeleniumWebHelper
    {
        private readonly IWebDriver _driver;
        private readonly Uri _baseUri;

        private bool _isDisposed;

        #region Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, a flag for whether the web-page should be maximised and a page-load timeout
        /// of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, bool isMaximised = true)
            : this(baseUrl, GetDefaultOptions(), isMaximised)
        {
        }

        ///<summary>
        /// Intialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, options for the web-driver, a flag for whether the web-page should be 
        /// maximised and page-load timeout of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> or <paramref name="options"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, InternetExplorerOptions options, bool isMaximised = true)
            : this(baseUrl, options, TimeSpan.FromSeconds(60), isMaximised)
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, a page-load timeout, a flag for whether the web-page should be 
        /// maximised and default options for the web-driver.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, TimeSpan pageLoadTimeout, bool isMaximised = true)
            : this(baseUrl, GetDefaultOptions(), pageLoadTimeout, isMaximised)
        {
        }

        /// <summary>
        /// Intialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, options for the web-driver, a flag for whether the web-page should be 
        /// maximised and page-load timeout.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> or <paramref name="options"/>was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, InternetExplorerOptions options, TimeSpan pageLoadTimeout, bool isMaximised = true)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumInternetExplorerWebHelper)}, {nameof(options)} cannot be null");
            }
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumInternetExplorerWebHelper)}, {nameof(baseUrl)} cannot be null");
            }

            _baseUri = new Uri(baseUrl);
            _driver = new InternetExplorerDriver(options);
            _driver.Manage().Timeouts().PageLoad = pageLoadTimeout;

            if (isMaximised)
            {
                _driver.Manage().Window.Maximize();
            }
        }

        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication,
        /// a flag for whether the web-page should be maximised and a page-load timeout of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> <paramref name="userName"/> or <paramref name="password"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, string userName, string password, bool isMaximised = true)
            : this(baseUrl, userName, password, GetDefaultOptions(), isMaximised)
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication, options
        /// for the web-driver, a flag for whether the web-page should be maximised and a page-load 
        /// timeout of 60 seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> <paramref name="userName"/>, <paramref name="password"/> or <paramref name="options"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, string userName, string password, InternetExplorerOptions options, bool isMaximised = true)
            : this(baseUrl, userName, password, options, TimeSpan.FromSeconds(60), isMaximised)
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication, a page-load
        /// time in seconds, a flag for whether the web-page should be maximised and a default options for the
        /// web-driver.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> <paramref name="userName"/> or <paramref name="password"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, string userName, string password, TimeSpan pageLoadTimeout, bool isMaximised = true)
            : this(baseUrl, userName, password, GetDefaultOptions(), TimeSpan.FromSeconds(60), isMaximised)
        {
        }

        /// <summary>
        /// Initialises a new instance of <see cref="SeleniumInternetExplorerWebHelper"/> with a specified
        /// base URL for the web-site, userName and password for the site's Windows Authentication, options
        /// for the web-driver, a flag for whether the web-page should be maximised and a page-load 
        /// timeout in seconds.
        /// </summary>
        /// <param name="baseUrl">The base URL for the web-site.</param>
        /// <param name="userName">The userName for the site's Windows Authentication pop-up.</param>
        /// <param name="password">The password for the site's Windows Authentication pop-up.</param>
        /// <param name="options">The options for the underlying web-driver.</param>
        /// <param name="pageLoadTimeout">The page-load timeout in seconds.</param>
        /// <param name="isMaximised">The flag for whether or not the web-page should be maximised, default is <c>true</c></param>
        /// <exception cref="ArgumentNullException"><paramref name="baseUrl"/> <paramref name="userName"/>, <paramref name="password"/> or <paramref name="options"/> was null.</exception>
        /// <exception cref="UriFormatException"><paramref name="baseUrl"/> was not valid url.</exception>
        public SeleniumInternetExplorerWebHelper(string baseUrl, string userName, string password, InternetExplorerOptions options, TimeSpan pageLoadTimeout, bool isMaximised = true)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumInternetExplorerWebHelper)}, {nameof(options)} cannot be null");
            }
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumInternetExplorerWebHelper)}, {nameof(baseUrl)} cannot be null");
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumInternetExplorerWebHelper)}, {nameof(userName)} cannot be null");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(options),
                    $"Unable to initialise {nameof(SeleniumInternetExplorerWebHelper)}, {nameof(password)} cannot be null");
            }

            _baseUri = WindowsAuthenticationUrlParser.ParseWindowsAuthenticationUrl(baseUrl, userName, password);
            _driver = new InternetExplorerDriver(options);
            _driver.Manage().Timeouts().PageLoad = pageLoadTimeout;

            if (isMaximised)
            {
                _driver.Manage().Window.Maximize();
            }
        }
        #endregion Constructors

        #region ISeleniumWebHelper Members
        /// <inheritdoc />
        public string BaseUrl { get { return _baseUri.ToString(); } }
        
        /// <inheritdoc />
        /// <remarks>
        /// In this case a <see cref="InternetExplorerDriver"/> instance.
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
            try
            {
                var availableLogs = WebDriver.Manage().Logs;

                //if driver returns logs             
                return availableLogs?.GetLog(LogType.Browser)
                    .Where(x => x.Level == level);
            }
            catch (NullReferenceException)
            {
                //some versions of IE do not provide access to the logs.
                return new LogEntry[0];
            }
        }
        #endregion ISeleniumWebHelper Members

        private static InternetExplorerOptions GetDefaultOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IgnoreZoomLevel = true;
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
                    /*
                     * 
                     *  By default the Selenium Internet-Explorer Driver does not
                     *  close the associated browser windows on quit & dispose.
                     *  
                     *  So the below code is intended to combine the Driver dipose
                     *  with closing the browser windows.
                     *  
                     *  1) Get the process handle for the browser window associated with the IEDriver
                     *  2) Quit and Dispose of the IEDriver
                     *  3) If we have a process handle for the browser and it has not exited then kill it.
                     * */
                    Process browser = WebDriverProcessHelper
                        .GetInternetExplorerWindowHandleOrDefault();

                    _driver.Quit();
                    _driver.Dispose();

                    if (browser != null &&
                        !browser.HasExited)
                    {
                        browser.Kill();
                    }
                }
                _isDisposed = true;
            }
        }
        #endregion IDisposable Members

        ~SeleniumInternetExplorerWebHelper()
        {
            Dispose(false);
        }
    }
}
