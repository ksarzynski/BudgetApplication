using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BudgetApplicationSeleniumTests.Pages;
using System;
using System.Collections.ObjectModel;

namespace BudgetApplicationSeleniumTests
{
    [TestClass]
    public class ItemsTest
    {
        public static IWebDriver driver;
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private string baseURL = BasePath.Url + "Items";
        private LoginPage loginPage = null;

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver(pathToDebug);
            loginPage = new LoginPage(driver);
            driver.Navigate().GoToUrl(BasePath.Url + "Account/Login");
            driver.Manage().Window.Maximize();
            loginPage.SendTextToField(loginPage.Email, "user@user.com");
            loginPage.SendTextToField(loginPage.Password, "p@sw1ooorD");
            loginPage.LoginButton.Click();
        }

        [TestMethod]
        [Priority(1)]
        public void CreateItemWithoutSubategory()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var ItemName = driver.FindElement(By.Id("ItemName"));
            ItemName.SendKeys("Test");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/input   "));
            button.Click();

            string result = driver.Title;
            StringAssert.Contains(result, "Create - Budget Application");
            driver.Close();

        }



        [TestMethod]
        [Priority(2)]
        public void CreateItemWithValidName()
        {

            driver.Navigate().GoToUrl(BasePath.Url + "Categories");
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var categoryName = driver.FindElement(By.Id("CategoryName"));
            categoryName.SendKeys("Test");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[2]/input"));
            button.Click();

            driver.Navigate().GoToUrl(BasePath.Url + "Subcategories");
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var subcategoryName = driver.FindElement(By.Id("SubcategoryName"));
            subcategoryName.SendKeys("Test");
            var button2 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/input"));
            button2.Click();

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var ItemName = driver.FindElement(By.Id("ItemName"));
            ItemName.SendKeys("Test");
            var button3 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/input"));
            button3.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]")).Text;
            StringAssert.Contains(result, "Test");
            driver.Close();

        }


        [TestMethod]
        [Priority(3)]
        public void CreateItemWithSmallFirstLetterInNameShouldNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var ItemName = driver.FindElement(By.Id("ItemName"));
            ItemName.SendKeys("test");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/input"));
            button.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[2]/span")).Text;
            StringAssert.Contains(result, "Invalid text format. Word starts with upper-case and contains only letters.");
            driver.Close();

        }


        [TestMethod]
        [Priority(4)]
        public void EditItemWithValidName()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[3]/a[1]")).Click();
            var ItemName = driver.FindElement(By.Id("ItemName"));
            ItemName.Clear();
            ItemName.SendKeys("TestEdit");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/input"));
            button.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]")).Text;
            StringAssert.Contains(result, "TestEdit");
            driver.Close();
        }

        [TestMethod]
        [Priority(5)]
        public void DeleteItem()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[3]/a[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();

            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.XPath("//*[contains(., 'TestEdit')]"));

            Assert.IsTrue(links.Count < 1);


            driver.Navigate().GoToUrl(BasePath.Url + "Subcategories");
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[3]/a[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();

            driver.Navigate().GoToUrl(BasePath.Url + "Categories");
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/a[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
            driver.Close();
        }

    }
}
