using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Dunk.TestUtilities.Selenium.Extensions
{
    /// <summary>
    /// Provides a series of extension methods for a <see cref="IWebDriver"/> instance
    /// for accessing HMTL controls on a web-page.
    /// </summary>
    public static class WebDriverControlsExtensions
    {
        /// <summary>
        /// Finds a HTML control on the web-page by id and enters the specified text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementId">The id of the element we are looking for.</param>
        /// <param name="input">The text to enter.</param>
        public static void FindAndEnterTextById(this IWebDriver driver, string elementId, string input)
        {
            FindAndEnterText(driver, By.Id(elementId), input);
        }

        /// <summary>
        /// Finds a HTML control on the web-page by name and enters the specified text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementName">The name of the element we are looking for.</param>
        /// <param name="input">The text to enter.</param>
        public static void FindAndEnterTextByName(this IWebDriver driver, string elementName, string input)
        {
            FindAndEnterText(driver, By.Name(elementName), input);
        }

        /// <summary>
        /// Finds a HTML control on the web-page and enters the specified text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element we are looking for.</param>
        /// <param name="input">The text to enter.</param>
        public static void FindAndEnterText(this IWebDriver driver, By elementLocator, string input)
        {
            IWebElement element = driver.FindElement(elementLocator);
            element.SendKeys(input);
        }

        /// <summary>
        /// Finds a HTML control on the web-page by id and clicks it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementId">The id of the element we are looking for.</param>
        public static void FindAndClickById(this IWebDriver driver, string elementId)
        {
            FindAndClick(driver, By.Id(elementId));
        }

        /// <summary>
        /// Finds a HTML control on the web-page by name and clicks it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementName">The name of the element we are looking for.</param>
        public static void FindAndClickByName(this IWebDriver driver, string elementName)
        {
            FindAndClick(driver, By.Name(elementName));
        }

        /// <summary>
        /// Finds a HTML control on the web-page and clicks it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element we are looking for.</param>
        public static void FindAndClick(this IWebDriver driver, By elementLocator)
        {
            IWebElement element = driver.FindElement(elementLocator);
            element.Click();
        }

        /// <summary>
        /// Finds a HTML control on the web-page by id and executes a JavaScript clicks event on it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementId">The id of the element we are looking for.</param>
        public static void FindAndJavaClickById(this IWebDriver driver, string elementId)
        {
            FindAndJavaClick(driver, By.Id(elementId));
        }

        /// <summary>
        /// Finds a HTML control on the web-page by name and executes a JavaScript clicks event on it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementName">The name of the element we are looking for.</param>
        public static void FindAndJavaClickByName(this IWebDriver driver, string elementName)
        {
            FindAndJavaClick(driver, By.Name(elementName));
        }

        /// <summary>
        /// Finds a HTML control on the web-page and executes a JavaScript clicks event on it.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element we are looking for.</param>
        public static void FindAndJavaClick(this IWebDriver driver, By elementLocator)
        {
            IWebElement element = driver.FindElement(elementLocator);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Finds a HMTL drop down control on the web-page and selects an item
        /// from the drop down.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element we are looking for.</param>
        /// <param name="itemText">The name of the drop down item to select.</param>
        public static void FindAndSelectDropDownItem(this IWebDriver driver, By elementLocator, string itemText)
        {
            IWebElement element = driver.FindElement(elementLocator);
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByText(itemText);
        }

        /// <summary>
        /// Finds a HMTL drop down control on the web-page by id and selects an item
        /// from the drop down.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementId">The id of the element we are looking for.</param>
        /// <param name="itemText">The name of the drop down item to select.</param>
        public static void FindAndSelectDropDownItemById(this IWebDriver driver, string elementId, string itemText)
        {
            FindAndSelectDropDownItem(driver, By.Id(elementId), itemText);
        }

        /// <summary>
        /// Finds a HMTL drop down control on the web-page by id and selects an item
        /// from the drop down.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementId">The id of the element we are looking for.</param>
        /// <param name="itemIndex">The index of the drop down item to select.</param>
        public static void FindAndSelectDropDownItemById(this IWebDriver driver, string elementId, int itemIndex)
        {
            FindAndSelectDropDownIndex(driver, By.Id(elementId), itemIndex);
        }

        /// <summary>
        /// Finds a HMTL drop down control on the web-page by name and selects an item
        /// from the drop down.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementName">The name of the element we are looking for.</param>
        /// <param name="itemIndex">The index of the drop down item to select.</param>
        public static void FindAndSelectDropDownItemByName(this IWebDriver driver, string elementName, int itemIndex)
        {
            FindAndSelectDropDownIndex(driver, By.Name(elementName), itemIndex);
        }

        /// <summary>
        /// Finds a HMTL drop down control on the web-page by name and selects an item
        /// from the drop down.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementName">The name of the element we are looking for.</param>
        /// <param name="itemText">The name of the drop down item to select.</param>
        public static void FindAndSelectDropDownItemByName(this IWebDriver driver, string elementName, string itemText)
        {
            FindAndSelectDropDownItem(driver, By.Name(elementName), itemText);
        }

        /// <summary>
        /// Finds a HMTL drop down control on the web-page and selects an item
        /// from the drop down.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element we are looking for.</param>
        /// <param name="itemIndex">The index of the drop down item to select.</param>
        public static void FindAndSelectDropDownIndex(this IWebDriver driver, By elementLocator, int itemIndex)
        {
            IWebElement element = driver.FindElement(elementLocator);
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByIndex(itemIndex);
        }

        /// <summary>
        /// Finds a HTML control on the web-page by id and retrieves the text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementId">The id of the element we are looking for.</param>
        /// <returns>
        /// The text held by the HTML control, if any.
        /// </returns>
        public static void FindAndGetTextById(this IWebDriver driver, string elementId)
        {
            FindAndGetText(driver, By.Id(elementId));
        }

        /// <summary>
        /// Finds a HTML control on the web-page by name and retrieves the text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementName">The name of the element we are looking for.</param>
        /// <returns>
        /// The text held by the HTML control, if any.
        /// </returns>
        public static void FindAndGetTextByName(this IWebDriver driver, string elementName)
        {
            FindAndGetText(driver, By.Name(elementName));
        }

        /// <summary>
        /// Finds a HTML control on the web-page and retrieves the text.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="elementLocator">The element we are looking for.</param>
        /// <returns>
        /// The text held by the HTML control, if any.
        /// </returns>
        public static string FindAndGetText(this IWebDriver driver, By elementLocator)
        {
            IWebElement element = driver.FindElement(elementLocator);
            return element.Text;
        }
    }
}
