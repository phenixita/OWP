using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OwpPortal.UITests.Helpers
{
    public static class CommonHelper
    {
        public static void DoAuthentication(IWebDriver driver)
        {
            driver.FindElement(By.Id("i0116")).Clear();
            driver.FindElement(By.Id("i0116")).SendKeys("admin@owpimagination.onmicrosoft.com");
            driver.FindElement(By.Id("i0116")).SendKeys(Keys.Enter);
            Thread.Sleep(1500);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            var passwordField = wait.Until(driver => driver.FindElement(By.Id("i0118")));
            passwordField.SendKeys("T3amImagin3");
            Thread.Sleep(1500);
            var submitButton = wait.Until(driver => driver.FindElement(By.XPath(".//*[@class='btn btn-block btn-primary']")));
            submitButton.SendKeys(Keys.Enter);
            var submitYesButton = wait.Until(driver => driver.FindElement(By.Id("idSIButton9")));

            if (submitYesButton != null)
                submitYesButton.SendKeys(Keys.Enter);
        }


    }
}
