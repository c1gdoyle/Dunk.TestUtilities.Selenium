using Dunk.TestUtilities.Selenium.Core;
using NUnit.Framework;

namespace Dunk.TestUtilities.Selenium.Test.Core
{
    [TestFixture]
    public class SeleniumEdgeWebHelperTests
    {
        [Test]
        public void EdgeWebHelperInitialisesWebDriver()
        {
            using (var webHelper = new SeleniumEdgeWebHelper("http://test_website_url"))
            {
                Assert.IsNotNull(webHelper.WebDriver);
            }
        }

        [Test]
        public void EdgeWebHelperInitialisesWithExceptedBaseUrl()
        {
            const string expectedUrl = "http://test_website_url/";

            using (var webHelper = new SeleniumEdgeWebHelper("http://test_website_url"))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }


        [Test]
        public void EdgeWebHelperInitialisesWebDriverWithUserNameAndPassword()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_bar";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.IsNotNull(webHelper.WebDriver);
            }
        }

        [Test]
        public void EdgeWebHelperInitialisesWithExceptedBaseUrlWithUserNameAndPassword()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_bar";

            string expectedUrl = $"http://{userName}:{password}@test_website_url/";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void EdgeWebHelperInitialisesWithUsernameAndPasswordInUrlAndCustomPort()
        {
            const string baseUrl = "http://test_website_url:8080";
            const string userName = "userName_foo";
            const string password = "password_bar";

            string expectedUrl = $"http://{userName}:{password}@test_website_url:8080/";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void EdgeWebHelperIntialisesWithUsernameContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_@foo";
            const string password = "password_bar";

            string expectedUrl = "http://userName_%40foo:password_bar@test_website_url/";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void EdgeWebHelperIntialisesWithPasswordContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_foo:password_%40bar@test_website_url/";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void EdgeWebHelperIntialisesWithUserNameAndPasswordContainingUriEscapeCharacterInUrl()
        {
            const string baseUrl = "http://test_website_url";
            const string userName = "userName_@foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_%40foo:password_%40bar@test_website_url/";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void EdgeWebHelperIntialisesWithUserNameAndPasswordContainingUriEscapeCharacterAndCustomPortInUrl()
        {
            const string baseUrl = "http://test_website_url:8080";
            const string userName = "userName_@foo";
            const string password = "password_@bar";

            string expectedUrl = "http://userName_%40foo:password_%40bar@test_website_url:8080/";

            using (var webHelper = new SeleniumEdgeWebHelper(baseUrl, userName, password))
            {
                Assert.AreEqual(expectedUrl, webHelper.BaseUrl);
            }
        }

        [Test]
        public void EdgeWebHelperNavigatesToBaseUrl()
        {
            using (var webHelper = new SeleniumEdgeWebHelper("https://www.google.com"))
            {
                Assert.DoesNotThrow(() => webHelper.NavigateToBaseUrl());
            }
        }

        [Test]
        public void EdgeWebHelperChecksForJavaScriptErrors()
        {
            using (var webHelper = new SeleniumEdgeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var jsErrors = webHelper.CheckForJavaScriptErrors();

                Assert.IsEmpty(jsErrors);
            }
        }

        [Test]
        public void EdgeWebHelperChecksForJavaScriptWarnings()
        {
            using (var webHelper = new SeleniumEdgeWebHelper("https://www.google.com"))
            {
                webHelper.NavigateToBaseUrl();

                var jsWarnings = webHelper.CheckForJavaScriptWarnings();

                Assert.IsEmpty(jsWarnings);
            }
        }
    }
}
