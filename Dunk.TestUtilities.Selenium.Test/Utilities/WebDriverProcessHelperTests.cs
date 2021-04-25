using System;
using System.Diagnostics;
using Dunk.TestUtilities.Selenium.Utilities;
using NUnit.Framework;

namespace Dunk.TestUtilities.Selenium.Test.Utilities
{
    [TestFixture]
    public class WebDriverProcessHelperTests
    {
        [Test]
        public void GetChromeDriverProcessThrowsIfNoChromeDriverIsRunning()
        {
            Assert.Throws<InvalidOperationException>(() => WebDriverProcessHelper.GetChromeDriverProcess());
        }

        [Test]
        public void GetInternetExplorerDriverProcessThrowsIfNoInternetExplorerDriverIsRunning()
        {
            Assert.Throws<InvalidOperationException>(() => WebDriverProcessHelper.GetInternetExplorerDriverProcess());
        }

        [Test]
        public void GetChromeDriverProcessOrDefaultReturnsNullIfNoChromeDriverIsRunning()
        {
            Process driverProcess = WebDriverProcessHelper.GetChromeDriverProcessOrDefault();
            Assert.IsNull(driverProcess);
        }

        [Test]
        public void GetInternetExplorerDriverProcessOrDefaultReturnsNullIfNoInternetExplorerDriverIsRunning()
        {
            Process driverProcess = WebDriverProcessHelper.GetInternetExplorerDriverProcessOrDefault();
            Assert.IsNull(driverProcess);
        }

        [Test]
        public void GetChromeWindowHandleOrDefaultReturnsNullIfNoChromeDriverIsRunning()
        {
            Process browserProcess = WebDriverProcessHelper.GetChromeWindowHandleOrDefault();
            Assert.IsNull(browserProcess);
        }

        [Test]
        public void GetInternetExplorerWindowHandleOrDefaultReturnsNullIfNoInternetExplorerDriverIsRunning()
        {
            Process browserProcess = WebDriverProcessHelper.GetInternetExplorerWindowHandleOrDefault();
            Assert.IsNull(browserProcess);
        }
    }
}
