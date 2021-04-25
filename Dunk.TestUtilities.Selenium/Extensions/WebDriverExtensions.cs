using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Dunk.TestUtilities.Selenium.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

using DotNetExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Dunk.TestUtilities.Selenium.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for a <see cref="IWebDriver"/> instance that
    /// supports waiting for the state of an element on a web-page to change.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Invokes a PageDown click event on the web-pgate.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException"><paramref name="driver"/> was null.</exception>
        public static void PageDown(this IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver),
                    $"Unable to PgDn, parameter {nameof(driver)} cannot be null");
            }
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.PageDown).Build().Perform();
        }

        /// <summary>
        /// Invokes a PageUp click event on the web-pgate.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException"><paramref name="driver"/> was null.</exception>
        public static void PageUp(this IWebDriver driver)
        {
            if(driver == null)
            {
                throw new ArgumentNullException(nameof(driver),
                    $"Unable to PgUp, parameter {nameof(driver)} cannot be null");
            }
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.PageUp).Build().Perform();
        }

        /// <summary>
        /// Waits 10 seconds for a given web element to exist.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <returns>
        /// A reference to the web element if it exists before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to exist.</exception>
        public static IWebElement WaitUntilElementExists(this IWebDriver driver, By elementLocator)
        {
            return WaitUntilElementExists(driver, elementLocator, 10);
        }

        /// <summary>
        /// Waits a specified number of seconds for a given web element to exist.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <param name="timeout">The number of seconds to wait for the element to exist.</param>
        /// <returns>
        /// A reference to the web element if it exists before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to exist.</exception>
        public static IWebElement WaitUntilElementExists(this IWebDriver driver, By elementLocator, int timeout)
        {
            return WaitUntilElementExists(driver, elementLocator, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for a given web element to exist.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for the element to exist.</param>
        /// <returns>
        /// A reference to the web element if it exists before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to exist.</exception>
        public static IWebElement WaitUntilElementExists(this IWebDriver driver, By elementLocator, TimeSpan timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                return wait.Until(DotNetExpectedConditions.ElementExists(elementLocator));
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for element to exist. Element with locator:{elementLocator} was not found";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
            catch (NoSuchElementException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for element to exist. Element with locator:{elementLocator} was not found";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
        }

        /// <summary>
        /// Waits 10 seconds for a given web element to become visible.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <returns>
        /// A reference to the web element if it becomes visible before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to be visible.</exception>
        public static IWebElement WaitUntilElementIsVisible(this IWebDriver driver, By elementLocator)
        {
            return WaitUntilElementIsVisible(driver, elementLocator, 10);
        }

        /// <summary>
        /// Waits a specified number of seconds for a given web element to become visible.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <param name="timeout">The number of seconds to wait for the element to be visible.</param>
        /// <returns>
        /// A reference to the web element if it becomes visible before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeeed timeout waiting for element to be visible.</exception>
        public static IWebElement WaitUntilElementIsVisible(this IWebDriver driver, By elementLocator, int timeout)
        {
            return WaitUntilElementIsVisible(driver, elementLocator, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for a given web element to become visible.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for the element to be visible.</param>
        /// <returns>
        /// A reference to the web element if it becomes visible before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to be visible.</exception>
        public static IWebElement WaitUntilElementIsVisible(this IWebDriver driver, By elementLocator, TimeSpan timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                return wait.Until(DotNetExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for element to be visible. Element with locator:{elementLocator} was not found";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
            catch (NoSuchElementException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for element to be visible. Element with locator:{elementLocator} was not found";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
        }

        /// <summary>
        /// Waits 10 seconds for a given web element to become clickable.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <returns>
        /// A reference to the web element if it becomes clickable before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to be clickable.</exception>
        public static IWebElement WaitUntilElementIsClickable(this IWebDriver driver, By elementLocator)
        {
            return WaitUntilElementIsClickable(driver, elementLocator, 10);
        }

        /// <summary>
        /// Waits a specified number of seconds for a given web element to become clickable.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <param name="timeout">The number of seconds to wait for the element to be clickable.</param>
        /// <returns>
        /// A reference to the web element if it becomes clickable before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to be clickable.</exception>
        public static IWebElement WaitUntilElementIsClickable(this IWebDriver driver, By elementLocator, int timeout)
        {
            return WaitUntilElementIsClickable(driver, elementLocator, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for a given web element to become clickable.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are waiting for.</param>
        /// <param name="timeout">The number of seconds to wait for the element to be clickable.</param>
        /// <returns>
        /// A reference to the web element if it becomes clickable before the allotted time is exceeded; otherwise this method will throw.
        /// </returns>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for element to be clickable.</exception>
        public static IWebElement WaitUntilElementIsClickable(this IWebDriver driver, By elementLocator, TimeSpan timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                return wait.Until(DotNetExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for element to be clickable. Element with locator:{elementLocator} was not found";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
            catch (NoSuchElementException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for element to be clickable. Element with locator:{elementLocator} was not found";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
        }

        /// <summary>
        /// Waits 10 seconds for a given web element to contain specified text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are inspecting.</param>
        /// <param name="text">The text we are checking for.</param>
        /// <returns>
        /// <c>true</c> if the web element contains the specified text before the allotted timeout; otherwise returns <c>false</c>.
        /// </returns>
        public static bool WaitUntilElementContainsText(this IWebDriver driver, By elementLocator, string text)
        {
            return WaitUntilElementContainsText(driver, elementLocator, text, 10);
        }

        /// <summary>
        /// Waits a specified number of seconds for a given web element to contain specified text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are inspecting.</param>
        /// <param name="text">The text we are checking for.</param>
        /// <param name="timeout">The in seconds to wait for the element to contain the specified text.</param>
        /// <returns>
        /// <c>true</c> if the web element contains the specified text before the allotted timeout; otherwise returns <c>false</c>.
        /// </returns>
        public static bool WaitUntilElementContainsText(this IWebDriver driver, By elementLocator, string text, int timeout)
        {
            return WaitUntilElementContainsText(driver, elementLocator, text, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for a given web element to contain specified text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element which we are inspecting.</param>
        /// <param name="text">The text we are checking for.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for the element to contain the specified text.</param>
        /// <returns>
        /// <c>true</c> if the web element contains the specified text before the allotted timeout; otherwise returns <c>false</c>.
        /// </returns>
        public static bool WaitUntilElementContainsText(this IWebDriver driver, By elementLocator, string text, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait.Until(DotNetExpectedConditions.TextToBePresentInElementLocated(elementLocator, text));
        }

        /// <summary>
        /// Waits 10 seconds for a given web-page to load.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>
        /// <c>true</c> if the web-page has loaded before the allotted timeout; otherwise returns <c>false.</c>
        /// </returns>
        public static bool WaitForPageLoad(this IWebDriver driver)
        {
            return WaitForPageLoad(driver, 10);
        }

        /// <summary>
        /// Waits a specified number of seconds for a given web-page to load.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The number of seconds to wait for the web-page to load.</param>
        /// <returns>
        /// <c>true</c> if the web-page has loaded before the allotted timeout; otherwise returns <c>false.</c>
        /// </returns>
        public static bool WaitForPageLoad(this IWebDriver driver, int timeout)
        {
            return WaitForPageLoad(driver, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for a given web-page to load.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for the web-page to load.</param>
        /// <returns>
        /// <c>true</c> if the web-page has loaded before the allotted timeout; otherwise returns <c>false.</c>
        /// </returns>
        public static bool WaitForPageLoad(this IWebDriver driver, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(d => (IJavaScriptExecutor)d).ExecuteScript("return document.readyState")
                .Equals("complete");
        }

        /// <summary>
        /// Gets browser logs for a specified driver.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>
        /// An enumerable containg the browser logs.
        /// </returns>
        /// <remarks>
        /// Temporary work around until Log functionality is fixed in Selenium 4.
        /// 
        /// See https://stackoverflow.com/questions/57520296/selenium-webdriver-3-141-0-driver-manage-logs-availablelogtypes-throwing-syste
        /// </remarks>
        public static IEnumerable<LogEntry> GetBrowserLogs(this IWebDriver driver)
        {
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
            var method = typeof(LogEntry).GetMethod("FromDictionary", BindingFlags.Static | BindingFlags.NonPublic);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
            return GetBrowserLogsAsDictionaries(driver)
                .Select(dict => method.Invoke(null, new[] { dict }) as LogEntry);
        }

        /// <summary>
        /// Gets browser logs for a specified driver.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>
        /// An enumerable containg the browser logs.
        /// </returns>
        /// <remarks>
        /// Temporary work around until Log functionality is fixed in Selenium 4.
        /// 
        /// See https://stackoverflow.com/questions/57520296/selenium-webdriver-3-141-0-driver-manage-logs-availablelogtypes-throwing-syste
        /// </remarks>
        public static IEnumerable<Dictionary<string, object>> GetBrowserLogsAsDictionaries(this IWebDriver driver)
        {
            // setup
            var endpoint = GetEndpoint(driver);
            var session = GetSession(driver);
            var resource = $"{endpoint}session/{session}/se/log";
            const string jsonBody = @"{""type"":""browser""}";

            // execute
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(resource, content).GetAwaiter().GetResult();
                var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return AsLogEntries(responseBody);
            }
        }

        private static string GetEndpoint(IWebDriver driver)
        {
            // setup
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
            const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields

            // get RemoteWebDriver type
            var remoteWebDriver = GetRemoteWebDriver(driver.GetType());

            // get this instance executor > get this instance internalExecutor
            var executor = remoteWebDriver.GetField("executor", Flags).GetValue(driver) as DriverServiceCommandExecutor;
            var internalExecutor = executor.GetType().GetField("internalExecutor", Flags).GetValue(executor) as HttpCommandExecutor;

            // get URL
            var uri = internalExecutor.GetType().GetField("remoteServerUri", Flags).GetValue(internalExecutor) as Uri;

            // result
            return uri.AbsoluteUri;
        }

        private static Type GetRemoteWebDriver(Type type)
        {
            if (!typeof(RemoteWebDriver).IsAssignableFrom(type))
            {
                return type;
            }

            while (type != typeof(RemoteWebDriver))
            {
                type = type.BaseType;
            }

            return type;
        }

        private static SessionId GetSession(IWebDriver driver)
        {
            if (driver is IHasSessionId id)
            {
                return id.SessionId;
            }
            return new SessionId($"gravity-{Guid.NewGuid()}");
        }

        private static IEnumerable<Dictionary<string, object>> AsLogEntries(string responseBody)
        {
            // setup
            var value = $"{JToken.Parse(responseBody)["value"]}";
            return JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, object>>>(value);
        }
    }
}
