using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace BudgetApplicationSeleniumTests
{
    [TestClass]
    public class LoginTests
    {
        IWebDriver driver;
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private string baseURL = "http://localhost:49771/Account/Login";

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver(pathToDebug);
           
        }

        [TestMethod]
        public void LoginEmailNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            emailField.Click();
            emailField.SendKeys("testtest");
            passwordField.Click();

            var error = driver.FindElement(By.Id("Email-error")).Text;

            StringAssert.Contains(error, "The Email field is not a valid e-mail address.");
            driver.Quit();

        }
      

        [TestMethod]
        public void LoginWithInvalidPassword()
        {
            driver.Navigate().GoToUrl(baseURL);
            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            emailField.Click();
            emailField.SendKeys("admin@admin.com");
            passwordField.Click();
            passwordField.SendKeys("testtest");
            driver.FindElement(By.XPath("/html/body/div/div/div[1]/section/form/div[5]/button")).Click(); 

            var error = driver.FindElement(By.XPath("/html/body/div/div/div[1]/section/form/div[1]/ul/li")).Text;

            StringAssert.Contains(error, "Invalid login attempt.");
            driver.Quit();
        }


        [TestMethod]
        public void LoginWithCorrectEmailAndPasswordAsAdmin()
        {

            driver.Navigate().GoToUrl(baseURL);
            var email = driver.FindElement(By.Id("Email"));
            email.Click();
            email.SendKeys("admin@admin.com");

            var pass = driver.FindElement(By.Id("Password"));
            pass.Click();
            pass.SendKeys("p@sw1ooorD\n");

            var admin = driver.FindElement(By.LinkText("Hello admin@admin.com!")).Text;

            StringAssert.Contains(admin, "Hello admin@admin.com!");
            driver.Quit();

        }

    }
}
