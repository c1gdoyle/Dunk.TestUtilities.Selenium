using System;
using Dunk.TestUtilities.Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using DotNetExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Dunk.TestUtilities.Selenium.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for a <see cref="IWebDriver"/> instance
    /// for accessing Alert frames.
    /// </summary>
    public static class WebDriverAlertExtensions
    {
        /// <summary>
        /// Waits a specified number of seconds for an alert to appeart on the web-page
        /// and then accepts it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The number of seconds to wait for the alter to appear.</param>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for alert.</exception>
        public static void WaitAndAcceptAlert(this IWebDriver driver, int timeout)
        {
            WaitAndAcceptAlert(driver, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for an alert to appeart on the web-page
        /// and then accepts it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for the alter to appear.</param>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for alert.</exception>
        public static void WaitAndAcceptAlert(this IWebDriver driver, TimeSpan timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(DotNetExpectedConditions.AlertIsPresent());

                IAlert alert = driver.SwitchTo().Alert();
                alert?.Accept();
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for Alert";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error occurred waiting and accepting Alert";
                throw new WebDriverWaitException(errorMsg, ex);
            }
        }

        /// <summary>
        /// Waits a specified number of seconds for an alert to appeart on the web-page
        /// and then dismisses it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The number of seconds to wait for the alter to appear.</param>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for alert.</exception>
        public static void WaitAndDismisstAlert(this IWebDriver driver, int timeout)
        {
            WaitAndDismisstAlert(driver, TimeSpan.FromSeconds(timeout));
        }

        /// <summary>
        /// Waits a specified <see cref="TimeSpan"/> for an alert to appeart on the web-page
        /// and then dismisses it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The <see cref="TimeSpan"/> to wait for the alter to appear.</param>
        /// <exception cref="WebDriverWaitException">Exceeded timeout waiting for alert.</exception>
        public static void WaitAndDismisstAlert(this IWebDriver driver, TimeSpan timeout)
        {
            try
            {
                var wait = new WebDriverWait(driver, timeout);
                wait.Until(DotNetExpectedConditions.AlertIsPresent());

                IAlert alert = driver.SwitchTo().Alert();
                alert?.Dismiss();
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                string errorMsg = $"Exceeded timeout of {timeout} waiting for Alert";
                throw new WebDriverWaitException(errorMsg, wdtEx);
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error occurred waiting and dismissing Alert";
                throw new WebDriverWaitException(errorMsg, ex);
            }
        }
    }
}
