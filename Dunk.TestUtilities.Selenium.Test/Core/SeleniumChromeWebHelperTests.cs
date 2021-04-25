using Dunk.TestUtilities.Selenium.Core;
using NUnit.Framework;

namespace Dunk.TestUtilitiesSelenium.Test.Core
{
    [TestFixture]
    public class SeleniumChromeWebHelperTests
    {
        [Test]
        public void ChromeWebHelperInitialisesWebDriver()
        {
            using (var webHelper = new SeleniumChromeWebHelper("http://test_website_url"))
            {
                Assert.IsNotNull(webHelper.WebDriver);
            }
        }

        [Test]
        public void ChromeWebHelperInitialisesWithExceptedBaseUrl()
        {
            const string expectedUrl = "http://test_website_url/";

            using (var webHelper = new SeleniumChromeWebHelper("http://test_website_url"))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperInitialisesWebDriverWithUserNameAndPassword()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_bar";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.IsNotNull(webHelper.WebDriver);
            }
        }

        [Test]
        public void ChromeWebHelperInitialisesWithExceptedBaseUrlWithUserNameAndPassword()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_bar";

            string expectedUrl = $"http://{userName}:{password}@test_website_url/";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperInitialisesWithUsernameAndPasswordInUrlAndCustomPort()
        {
            const string baseUrl = "http://test_website_url:8080";
            const string userName = "userName_foo";
            const string password = "password_bar";

            string expectedUrl = $"http://{userName}:{password}@test_website_url:8080/";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperIntialisesWithUsernameContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_@foo";
            const string password = "password_bar";

            string expectedUrl = "http://userName_%40foo:password_bar@test_website_url/";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperIntialisesWithPasswordContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_foo:password_%40bar@test_website_url/";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperIntialisesWithUserNameAndPasswordContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_@foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_%40foo:password_%40bar@test_website_url/";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperIntialisesWithUserNameAndPasswordContainingUriEscapeCharacterAndCustomPortInUrl()
        {
            const string baseUrl = "http://test_website_url:8080";
            const string userName = "userName_@foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_%40foo:password_%40bar@test_website_url:8080/";

            using (var webHelper = new SeleniumChromeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void ChromeWebHelperNavigatesToBaseUrl()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                Assert.DoesNotThrow(() => webHelper.NavigateToBaseUrl());
            }
        }

        [Test]
        public void ChromeWebHelperChecksForJavaScriptErrors()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var jsErrors = webHelper.CheckForJavaScriptErrors();

                Assert.IsEmpty(jsErrors);
            }
        }

        [Test]
        public void ChromeWebHelperChecksForJavaScriptWarnings()
        {
            using (var webHelper = new SeleniumChromeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var jsWarnings = webHelper.CheckForJavaScriptWarnings();

                Assert.IsEmpty(jsWarnings);
            }
        }
    }
}
