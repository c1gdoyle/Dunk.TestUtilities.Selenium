using Dunk.TestUtilities.Selenium.Core;
using NUnit.Framework;

namespace Dunk.TestUtilities.Selenium.Test.Core
{
    [TestFixture]
    public class SeleniumInternetExplorerWebHelperTests
    {
        [Test]
        public void InternetExplorerWebHelperInitialisesWebDriver()
        {
            using (var webHelper = new SeleniumInternetExplorerWebHelper("http://test_website_url"))
            {
                Assert.IsNotNull(webHelper.WebDriver);
            }
        }

        [Test]
        public void InternetExplorerWebHelperInitialisesWithExceptedBaseUrl()
        {
            const string expectedUrl = "http://test_website_url/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper("http://test_website_url"))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }


        [Test]
        public void InternetExplorerWebHelperInitialisesWebDriverWithUserNameAndPassword()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_bar";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.IsNotNull(webHelper.WebDriver);
            }
        }

        [Test]
        public void InternetExplorerWebHelperInitialisesWithExceptedBaseUrlWithUserNameAndPassword()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_bar";

            string expectedUrl = $"http://{userName}:{password}@test_website_url/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void InternetExplorerWebHelperInitialisesWithUsernameAndPasswordInUrlAndCustomPort()
        {
            const string baseUrl = "http://test_website_url:8080";
            const string userName = "userName_foo";
            const string password = "password_bar";

            string expectedUrl = $"http://{userName}:{password}@test_website_url:8080/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void InternetExplorerWebHelperIntialisesWithUsernameContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_@foo";
            const string password = "password_bar";

            string expectedUrl = "http://userName_%40foo:password_bar@test_website_url/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void InternetExplorerWebHelperIntialisesWithPasswordContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_foo:password_%40bar@test_website_url/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void InternetExplorerWebHelperIntialisesWithUserNameAndPasswordContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_@foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_%40foo:password_%40bar@test_website_url/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void InternetExplorerWebHelperIntialisesWithUserNameAndPasswordContainingUriEscapeCharacterAndCustomPortInUrl()
        {
            const string baseUrl = "http://test_website_url:8080";
            const string userName = "userName_@foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_%40foo:password_%40bar@test_website_url:8080/";

            using (var webHelper = new SeleniumInternetExplorerWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void InternetExplorerWebHelperNavigatesToBaseUrl()
        {
            using (var webHelper = new SeleniumInternetExplorerWebHelper("https://www.google.com"))
            {
                Assert.DoesNotThrow(() => webHelper.NavigateToBaseUrl());
            }
        }

        [Test]
        public void InternetExplorerWebHelperChecksForJavaScriptErrors()
        {
            using (var webHelper = new SeleniumInternetExplorerWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var jsErrors = webHelper.CheckForJavaScriptErrors();

                Assert.IsEmpty(jsErrors);
            }
        }

        [Test]
        public void InternetExplorerWebHelperChecksForJavaScriptWarnings()
        {
            using (var webHelper = new SeleniumInternetExplorerWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var jsWarnings = webHelper.CheckForJavaScriptWarnings();

                Assert.IsEmpty(jsWarnings);
            }
        }
    }
}
