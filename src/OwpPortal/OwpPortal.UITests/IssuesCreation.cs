using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace OwpPortal.UITests
{
    public class IssuesCreation : BaseTest
    {
        [Fact]
        public void CanOpenMainPage()
        {
            m_driver = new ChromeDriver();
             m_driver.Manage().Window.Maximize();
            m_driver.Url = "https://owp.azurewebsites.net/";
            Assert.Equal("Home Page - owp_web",m_driver.Title);
            m_driver.Close();

        }
    }
}
