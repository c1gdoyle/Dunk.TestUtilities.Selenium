using System;
using Dunk.TestUtilities.Selenium.Core;
using Dunk.TestUtilities.Selenium.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Dunk.TestUtilities.Selenium.Test.Extensions
{
    [TestFixture]
    public class WebDriverExtensionsTests
    {
        [Test]
        public void WaitForPageLoadReturnsTrueIfPageLoadsBeforeExceededTimeout()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                Assert.IsTrue(webHelper.WebDriver.WaitForPageLoad());
            }
        }

        [Test]
        public void WaitUntilElementExistsReturnsElementIfElementExistsBeforeTimeoutExceeded()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var elementLocator = By.Name("btnK");
                var googleSearchButton = webHelper.WebDriver.WaitUntilElementExists(elementLocator);

                Assert.IsNotNull(googleSearchButton);
            }
        }

        [Test]
        public void WaitUntiElementContainsTextReturnsTrueIfTextIsPresent()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var elementLocator = By.ClassName("gb_g");
                bool containsText = webHelper.WebDriver.WaitUntilElementContainsText(elementLocator, "Gmail");

                Assert.IsTrue(containsText);
            }
        }

        [Test]
        public void PageDownEventThrowsIfWebDriverIsNull()
        {
            IWebDriver driver = null;
            Assert.Throws<ArgumentNullException>(() => driver.PageDown());
        }

        [Test]
        public void PageDownEventDoesNotThrowIfWebDriverIsInitialised()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                Assert.DoesNotThrow(() => webHelper.WebDriver.PageDown());
            }
        }

        [Test]
        public void PageUpEventThrowsIfWebDriverIsNull()
        {
            IWebDriver driver = null;
            Assert.Throws<ArgumentNullException>(() => driver.PageUp());
        }

        [Test]
        public void PageUpEventDoesNotThrowIfWebDriverIsInitialised()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                Assert.DoesNotThrow(() => webHelper.WebDriver.PageUp());
            }
        }
    }
}
