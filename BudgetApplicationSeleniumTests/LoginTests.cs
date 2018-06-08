using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetApplicationSeleniumTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace BudgetApplicationSeleniumTests
{
    [TestClass]
    public class LoginTests
    {
        private static IWebDriver driver;
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private string baseURL = BasePath.Url + "Account/Login";
        private LoginPage loginPage = null;


        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver(pathToDebug);
            loginPage = new LoginPage(driver);
            driver.Navigate().GoToUrl(baseURL);

        }

        [TestMethod]
        public void LoginEmailNotValid()
        {
            loginPage.SendTextToField(loginPage.Email, "testtest");
            loginPage.Password.Click();

            var error = loginPage.GetEmailErrorText();

            StringAssert.Contains(error, "The Email field is not a valid e-mail address.");
            

        }


        [TestMethod]
        public void LoginWithInvalidPassword()
        {
            loginPage.SendTextToField(loginPage.Email, "admin@admin.com");
            loginPage.SendTextToField(loginPage.Password, "testtest");
            loginPage.LoginButton.Click();

            var error = loginPage.GetPasswordErrorText();

            StringAssert.Contains(error, "Invalid login attempt.");
            
        }


        [TestMethod]
        public void LoginWithCorrectEmailAndPasswordAsAdmin()
        {

            loginPage.SendTextToField(loginPage.Email, "admin@admin.com");
            loginPage.SendTextToField(loginPage.Password, "p@sw1ooorD");
            loginPage.LoginButton.Click();

            var admin = loginPage.FindElementByLinkText("Hello admin@admin.com!");
            StringAssert.Contains(admin, "Hello admin@admin.com!");
            

        }

        [TestCleanup]
        public void TearDown() => driver.Quit();

    }
}
