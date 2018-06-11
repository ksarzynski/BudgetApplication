using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BudgetApplicationSeleniumTests.Pages;
using System;
using System.Collections.ObjectModel;

namespace BudgetApplicationSeleniumTests
{
    [TestClass]
    public class CategoryTest
    {
        public static IWebDriver driver;
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private string baseURL = BasePath.Url + "Categories";
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
        public void CreateCategoryWithValidName()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var categoryName = driver.FindElement(By.Id("CategoryName"));
            categoryName.SendKeys("Test");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[2]/input"));
            button.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]")).Text;
            StringAssert.Contains(result, "Test");
            driver.Close();

        }

        [TestMethod]
        [Priority(2)]
        public void EditCategoryWithValidName()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/a[1]")).Click();
            var categoryName = driver.FindElement(By.Id("CategoryName"));
            categoryName.Clear();
            categoryName.SendKeys("TestEdit");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[2]/input"));
            button.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]")).Text;
            StringAssert.Contains(result, "TestEdit");
            driver.Close();
        }

        [TestMethod]
        [Priority(3)]
        public void DeleteCategory()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/a[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();

            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.XPath("//*[contains(., 'TestEdit')]"));


            Assert.IsTrue(links.Count < 1);
            driver.Close();
        }

        [TestMethod]
        [Priority(4)]
        public void CreateCategoryWithSmallFirstLetterInNameShouldNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var categoryName = driver.FindElement(By.Id("CategoryName"));
            categoryName.SendKeys("test");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[2]/input"));
            button.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[1]/span")).Text;
            StringAssert.Contains(result, "Invalid text format. Word starts with upper-case and contains only letters.");
            driver.Close();

        }

        [TestMethod]
        [Priority(5)]
        public void CreateCategoryWithTooLongNameShouldNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var categoryName = driver.FindElement(By.Id("CategoryName"));
            categoryName.SendKeys("Testtesttesttesttesttesttesttest");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[2]/input"));
            button.Click();

            var result = driver.FindElement(By.Id("CategoryName-error")).Text;
            StringAssert.Contains(result, "Please use name of length in range 3..30");
            driver.Close();

        }

    }
}
