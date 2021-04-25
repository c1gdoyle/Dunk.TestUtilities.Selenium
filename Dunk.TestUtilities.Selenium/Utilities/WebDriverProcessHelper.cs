using System;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace Dunk.TestUtilities.Selenium.Utilities
{
    /// <summary>
    /// A helper class that provides methods for getting the <see cref="Process"/>
    /// handle for a Selenium Web-Driver and the associated Web-Browser windows.
    /// </summary>
    public static class WebDriverProcessHelper
    {
        /// <summary>
        /// Gets the currently running Selenium ChromeDriver process.
        /// </summary>
        /// <returns>
        /// The Selenium ChromeDriver process.
        /// </returns>
        /// <exception cref="InvalidOperationException">More than one Selenium ChromeDriver process is running or no Selenium ChromeDriver processes are running.</exception>
        public static Process GetChromeDriverProcess()
        {
            var processes = Process.GetProcessesByName("chromedriver");
            if (processes.Length != 1)
            {
                throw new InvalidOperationException($"Unable to determine ChromeDriver process. Currently {processes.Length} Selenium ChromeDriver processes active");
            }
            return processes[0];
        }

        /// <summary>
        /// Gets the currently running Selenium ChromeDriver process.
        /// </summary>
        /// <returns>
        /// The Selenium ChromeDriver process or null if zero or more Selenium ChromeDriver processes are running.
        /// </returns>
        public static Process GetChromeDriverProcessOrDefault()
        {
            var processes = Process.GetProcessesByName("chromedriver");
            return processes.Length == 1 ? processes[0] : null;
        }

        /// <summary>
        /// Gets the Chrome browser window associated with the currently
        /// running Selenium ChromeDriver process.
        /// </summary>
        /// <returns>
        /// The Chrome browser window process.
        /// </returns>
        /// <exception cref="InvalidOperationException">More than one Selenium ChromeDriver process is running or no Selenium ChromeDriver processes are running.</exception>
        public static Process GetChromeWindowHandle()
        {
            Process driverProcess = GetChromeDriverProcess();
            return GetChromeWindowHandle(driverProcess.Id);
        }

        /// <summary>
        /// Gets the Chrome browser window for a specified Selenium IEDriver pid.
        /// </summary>
        /// <param name="driverId">The Selenium ChromeDriver pid.</param>
        /// <returns>
        /// The Chrome browser window process or null if no browser process is running under the Selenium ChromeDriver pid.
        /// </returns>
        public static Process GetChromeWindowHandle(int driverId)
        {
            return GetWindowHandle(driverId, "chrome");
        }

        /// <summary>
        /// Gets the Chrome browser window associated with the currently
        /// running Selenium ChromeDriver process.
        /// </summary>
        /// <returns>
        /// The Chrome browser window process or null if more than one Selenium ChromeDriver process is running or no Selenium ChromeDriver processes are running.
        /// </returns>
        public static Process GetChromeWindowHandleOrDefault()
        {
            Process driverProcess = GetChromeDriverProcessOrDefault();
            return driverProcess != null ? GetChromeWindowHandle(driverProcess.Id) : null;
        }

        /// <summary>
        /// Gets the currently running Selenium IEDriver process.
        /// </summary>
        /// <returns>
        /// The Selenium IEDriver process.
        /// </returns>
        /// <exception cref="InvalidOperationException">More than one Selenium IEDriver process is running or no Selenium IEDriver processes are running.</exception>
        public static Process GetInternetExplorerDriverProcess()
        {
            var processes = Process.GetProcessesByName("IEDriverServer");
            if (processes.Length != 1)
            {
                throw new InvalidOperationException($"Unable to determine IEDriver process. Currently {processes.Length} Selenium IEDriver processes active");
            }
            return processes[0];
        }

        /// <summary>
        /// Gets the currently running Selenium IEDriver process.
        /// </summary>
        /// <returns>
        /// The Selenium IEDriver process or null if zero or more Selenium IEDriver processes are running.
        /// </returns>
        public static Process GetInternetExplorerDriverProcessOrDefault()
        {
            var processes = Process.GetProcessesByName("IEDriverServer");
            return processes.Length == 1 ? processes[0] : null;
        }

        /// <summary>
        /// Gets the Internet-Explorer browser window associated with the currently
        /// running Selenium IEDriver process.
        /// </summary>
        /// <returns>
        /// The Internet-Explorer browser window process.
        /// </returns>
        /// <exception cref="InvalidOperationException">More than one Selenium IEDriver process is running or no Selenium IEDriver processes are running.</exception>
        public static Process GetInternetExplorerWindowHandle()
        {
            Process driverProcess = GetInternetExplorerDriverProcess();
            return GetInternetExplorerWindowHandle(driverProcess.Id);
        }

        /// <summary>
        /// Gets the Internet-Explorer browser window for a specified Selenium IEDriver pid.
        /// </summary>
        /// <param name="driverId">The Selenium IEDriver pid.</param>
        /// <returns>
        /// The Internet-Explorer browser window process or null if no browser process is running under the Selenium IEDriver pid.
        /// </returns>
        public static Process GetInternetExplorerWindowHandle(int driverId)
        {
            return GetWindowHandle(driverId, "iexplore");
        }

        /// <summary>
        /// Gets the Internet-Explorer browser window associated with the currently
        /// running Selenium IEDriver process.
        /// </summary>
        /// <returns>
        /// The Internet-Explorer browser window process or null if more than one Selenium IEDriver process is running or no Selenium IEDriver processes are running.
        /// </returns>
        public static Process GetInternetExplorerWindowHandleOrDefault()
        {
            Process driverProcess = GetInternetExplorerDriverProcessOrDefault();
            return driverProcess != null ? GetInternetExplorerWindowHandle(driverProcess.Id) : null;
        }

        /// <summary>
        /// Gets the browser window for a specified Selenium WbeDriver pid and browser.
        /// </summary>
        /// <param name="driverId">The Selenium WebDriver pid.</param>
        /// <param name="browserProcessName">The name of the browser process we are looking for.</param>
        /// <returns>
        /// The browser window process or null if no browser process is running under the Selenium WebDriver pid.
        /// </returns>
        public static Process GetWindowHandle(int driverId, string browserProcessName)
        {
            var processes = Process.GetProcessesByName(browserProcessName)
                .Where(x => !x.MainWindowHandle.Equals(IntPtr.Zero));

            foreach (var process in processes)
            {
                var parentId = GetParentProcess(process.Id);
                if (parentId == driverId)
                {
                    return process;
                }
            }
            return null;
        }

        private static int GetParentProcess(int id)
        {
            int parentPid;
            using (ManagementObject mo = new ManagementObject($"win32_process.handle={id}"))
            {
                mo.Get();
                parentPid = Convert.ToInt32(mo["ParentProcessId"]);
            }
            return parentPid;
        }
    }
}
