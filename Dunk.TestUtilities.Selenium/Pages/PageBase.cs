using System;
using OpenQA.Selenium;

namespace Dunk.TestUtilities.Selenium.Pages
{
    /// <summary>
    /// An abstract base class representing a Page Object Model (POM).
    /// </summary>
    public abstract class PageBase
    {
        private readonly IWebDriver _driver;

        /// <summary>
        /// Intialises a new instance of <see cref="PageBase"/> with a specified
        /// <see cref="IWebDriver"/> instance.
        /// </summary>
        /// <param name="driver">The Web-Driver associated with this page.</param>
        /// <exception cref="ArgumentNullException"><paramref name="driver"/> parameter was null.</exception>
        protected PageBase(IWebDriver driver)
        {
            if(driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }
            _driver = driver;
        }

        /// <summary>
        /// Gets the <see cref="IWebDriver"/> associated with this page. 
        /// </summary>
        public IWebDriver Driver { get { return _driver; } }
    }
}
