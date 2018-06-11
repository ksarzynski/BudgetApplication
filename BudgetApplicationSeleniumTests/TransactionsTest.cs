using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BudgetApplicationSeleniumTests.Pages;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace BudgetApplicationSeleniumTests
{
    [TestClass]
    public class TransactionsTest
    {
        public static IWebDriver driver;
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private string baseURL = BasePath.Url + "Transactions";
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
        public void CreateTransactionWithoutItem()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();

            var price = driver.FindElement(By.Id("Price"));
            price.SendKeys("20");
            var transactionDate = driver.FindElement(By.Id("TransactionDate"));
            transactionDate.SendKeys("08.06.2018");

            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input"));
            button.Click();

            string result = driver.Title;

            StringAssert.Contains(result, "Create - Budget Application");
            driver.Close();

        }

        [TestMethod]
        [Priority(2)]
        public void CreateTransactionWithValidData()
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

            driver.Navigate().GoToUrl(BasePath.Url + "Items");
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var ItemName = driver.FindElement(By.Id("ItemName"));
            ItemName.SendKeys("Test");
            var button3 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[3]/input"));
            button3.Click();

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();

            var price = driver.FindElement(By.Id("Price"));
            price.SendKeys("20");
            var transactionDate = driver.FindElement(By.Id("TransactionDate"));
            transactionDate.SendKeys("08.06.2018");

            var button4 = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input"));
            button4.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[4]")).Text;
            StringAssert.Contains(result, "Test");
            driver.Close();

        }

        [TestMethod]
        [Priority(3)]
        public void CreateTransactionWithNotValidPriceShouldNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var price = driver.FindElement(By.Id("Price"));
            price.SendKeys("20df");
            driver.FindElement(By.Name("TransactionPlace")).Click();

            var result = driver.FindElement(By.Id("Price-error")).Text;

            StringAssert.Contains(result, "Please enter valid price format");
            driver.Close();
        }

        [TestMethod]
        [Priority(4)]
        public void CreateTransactionWithEmptyPriceLabelShouldNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var price = driver.FindElement(By.Id("Price"));
            price.SendKeys("");
            var transactionDate = driver.FindElement(By.Id("TransactionDate"));
            transactionDate.SendKeys("08.06.2018");

            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input"));
            button.Click();

            var result = driver.FindElement(By.Id("Price-error")).Text;

            StringAssert.Contains(result, "The Price field is required.");
            driver.Close();
        }

        [TestMethod]
        [Priority(5)]
        public void CreateTransactionWithEmptyDateLabelShouldNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/p/a")).Click();
            var price = driver.FindElement(By.Id("Price"));
            price.SendKeys("20");

            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input"));
            button.Click();

            var result = driver.FindElement(By.Id("TransactionDate-error")).Text;

            StringAssert.Contains(result, "The Transaction date field is required.");
            driver.Close();
        }

        [TestMethod]
        [Priority(6)]
        public void EditTransactionWithValidData()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[5]/a[1]")).Click();
            var price = driver.FindElement(By.Id("Price"));
            price.Clear();
            price.SendKeys("25");
            var transactionDate = driver.FindElement(By.Id("TransactionDate"));
            transactionDate.SendKeys("12.05.2018");
            var button = driver.FindElement(By.XPath("/html/body/div/div[1]/div/form/div[5]/input"));
            button.Click();

            var result = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[4]")).Text;
            StringAssert.Contains(result, "Test");
            driver.Close();
        }

        [TestMethod]
        [Priority(7)]
        public void DeleteTransaction()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[5]/a[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();

            ReadOnlyCollection<IWebElement> links = driver.FindElements(By.XPath("//*[contains(., 'Test')]"));

            Assert.IsTrue(links.Count < 1);


            driver.Navigate().GoToUrl(BasePath.Url + "Categories");
            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/a[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/form/input[2]")).Click();
            driver.Close();
        }


    }
}
