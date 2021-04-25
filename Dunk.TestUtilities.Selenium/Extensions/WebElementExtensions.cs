using OpenQA.Selenium;

namespace Dunk.TestUtilities.Selenium.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for a <see cref="IWebElement"/> instance.
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        /// Executes a Java-Script click event on this web-element using a specified <see cref="IWebDriver"/>.
        /// </summary>
        /// <param name="webElement">The web-element.</param>
        /// <param name="driver">The Web-Driver.</param>
        public static void JavaScriptClick(this IWebElement webElement, IWebDriver driver)
        {
            JavaScriptClick(webElement, (IJavaScriptExecutor)driver);
        }

        /// <summary>
        /// Executes a Java-Script click event on this web-element using a specified <see cref="IJavaScriptExecutor"/>.
        /// </summary>
        /// <param name="webElement">The web-element.</param>
        /// <param name="executor">The Java-Script executor.</param>
        public static void JavaScriptClick(this IWebElement webElement, IJavaScriptExecutor executor)
        {
            executor.ExecuteScript("arguments[0].click();", webElement);
        }
    }
}
