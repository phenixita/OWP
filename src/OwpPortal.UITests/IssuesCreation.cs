using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OwpPortal.UITests.Constants;
using OwpPortal.UITests.Helpers;
using Xunit;

namespace OwpPortal.UITests
{
    public class IssuesCreation : BaseTest
    {
        [Fact]
        public void CanOpenMainPage()
        {

            driver.Manage().Window.Maximize();
            driver.Url = url;
            Assert.Equal(CreateItemFeatureTextValues.Create_Title, driver.Title);
            driver.Close();

        }

        [Theory]
        [InlineData(1, "Reading", "road is broken")]
        [InlineData(2, "Reading", "paving broken")]
        [InlineData(3, "London", "bridge broken")]
        public void CanCreateIssue(int WorkItemType, string workItemAdress, string WorkItemDescription)
        {
            // Arrange
            driver.Manage().Window.Maximize();
            driver.Url = url;
            IWebElement WorkItemTypeRadioBtn = driver.FindElement(By.XPath($".//*[@id='{CreateItemFeatureSelectors.WorkItem_Type_Id}'and @value='{WorkItemType}']"));
            IWebElement WorkItemAdressField = driver.FindElement(By.XPath($".//*[@id='{CreateItemFeatureSelectors.WorkItem_Adress_Id}']"));
            IWebElement WorkItemDescriptionField = driver.FindElement(By.XPath($".//*[@id='{CreateItemFeatureSelectors.WorkItem_Description_Id}']"));
            IWebElement SubmitButton = driver.FindElement(By.XPath($".//*[@value='{CreateItemFeatureSelectors.SubmitButton_Value}' and @type='submit']"));

            // Act
            WorkItemTypeRadioBtn.Click();
            WorkItemAdressField.SendKeys(workItemAdress);
            WorkItemDescriptionField.SendKeys(WorkItemDescription);
            SubmitButton.Click();


            // Assert
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            IWebElement CreationMessageAlert = wait.Until(driver => driver.FindElement(By.XPath($".//*[@class='{CreateItemFeatureSelectors.Creation_Message_Class}']")));
            Assert.Contains(CreateItemFeatureTextValues.CreationSuccess_Message, CreationMessageAlert.Text);


            driver.Close();
            driver.Dispose();
        }



        [Fact]
        //[InlineData(1, "Reading", "road is broken")]
        //[InlineData(2, "Reading", "paving broken")]
        //[InlineData(3, "London", "bridge broken")]
        public void CanAccessWorkItemsList()
        {
            // Arrange
            driver.Manage().Window.Maximize();
            driver.Url = url + "workitems/index";
            CommonHelper.DoAuthentication(driver);         
           
        }

        [Theory]
        [InlineData(4)]
        public void CanSearchForCreatedItem(int workItemId)
        {
            // Arrange
            driver.Manage().Window.Maximize();
            driver.Url = url + "workitems/index";
            CommonHelper.DoAuthentication(driver);
      
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            
            var searchField = wait.Until(driver => driver.FindElement(By.XPath($".//*[@class='{SearchItemFeatureSelectors.Search_Class}'and @type='search']")));

            searchField.SendKeys(workItemId.ToString());

        }
    }
}
