using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwpPortal.UITests
{
    public class BaseTest
    {
        protected readonly IWebDriver driver;
        protected readonly string url;

        public BaseTest()
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json")
                .Build();
             

            var options = new FirefoxOptions() { AcceptInsecureCertificates = true };
            driver = new FirefoxDriver(options);
            

            url = configuration["WebSiteUrl"];
        }
    }
}
