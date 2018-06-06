using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace BudgetApplicationSeleniumTests
{
    [TestClass]
    public class RegisterTests
    {

        IWebDriver driver;
        private string pathToDebug = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath ?? "");
        private string baseURL = "http://localhost:49771/Account/Register";

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver(pathToDebug);      

        }

        [TestMethod]
        public void RegisterEmailNotValid()
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
        public void RegisterPasswordsNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            passwordField.Click();
            passwordField.SendKeys("test");
            emailField.Click();

            var error = driver.FindElement(By.Id("Password-error")).Text;

            StringAssert.Contains(error, "The Password must be at least 6 and at max 100 characters long.");
            driver.Quit();
        }

        [TestMethod]
        public void RegisterPasswordsDoNotMatch()
        {
            driver.Navigate().GoToUrl(baseURL);
            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            var confPasswordField = driver.FindElement(By.Id("ConfirmPassword"));

            emailField.Click();
            emailField.SendKeys("test@example.com");

            passwordField.Click();
            passwordField.SendKeys("testtest1");

            confPasswordField.Click();
            confPasswordField.SendKeys("testtest");

            passwordField.Click();

            var error = driver.FindElement(By.Id("ConfirmPassword-error")).Text;

            StringAssert.Contains(error, "The password and confirmation password do not match.");

            driver.Quit();
        }

        [TestMethod]
        public void RegisterPasswordNotValid()
        {
            driver.Navigate().GoToUrl(baseURL);
            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            var confPasswordField = driver.FindElement(By.Id("ConfirmPassword"));

            emailField.Click();
            emailField.SendKeys("test@example.com");

            passwordField.Click();
            passwordField.SendKeys("testtest");

            confPasswordField.Click();
            confPasswordField.SendKeys("testtest");

            var registerButton = driver.FindElement(By.XPath("/html/body/div/div/div/form/button"));
            registerButton.Click();

            var error1 = driver.FindElement(By.XPath("/html/body/div/div/div/form/div[1]/ul/li[1]")).Text;
            var error2 = driver.FindElement(By.XPath("/html/body/div/div/div/form/div[1]/ul/li[2]")).Text;
            var error3 = driver.FindElement(By.XPath("/html/body/div/div/div/form/div[1]/ul/li[3]")).Text;

            StringAssert.Contains(error1, "Passwords must have at least one non alphanumeric character.");
            StringAssert.Contains(error2, "Passwords must have at least one digit ('0'-'9').");
            StringAssert.Contains(error3, "Passwords must have at least one uppercase ('A'-'Z').");

            driver.Quit();
        }


        [TestMethod]
        public void RegisterEmailIsAlreadyTaken()
        {
            driver.Navigate().GoToUrl(baseURL);

            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            var confPasswordField = driver.FindElement(By.Id("ConfirmPassword"));

            emailField.Click();
            emailField.SendKeys("admin@admin.com");

            passwordField.Click();
            passwordField.SendKeys("Test123$");

            confPasswordField.Click();
            confPasswordField.SendKeys("Test123$");

            var registerButton = driver.FindElement(By.XPath("/html/body/div/div/div/form/button"));
            registerButton.Click();

            var error = driver.FindElement(By.XPath("/html/body/div/div/div/form/div[1]/ul/li")).Text;

            StringAssert.Contains(error, "User name 'admin@admin.com' is already taken.");
            driver.Quit();
        }



        [TestMethod]
        public void RegisterWithCorrectEmailAndPassword()
        {

            driver.Navigate().GoToUrl(baseURL);

            var emailField = driver.FindElement(By.Id("Email"));
            var passwordField = driver.FindElement(By.Id("Password"));
            var confPasswordField = driver.FindElement(By.Id("ConfirmPassword"));

            emailField.Click();
            emailField.SendKeys("test1@example.com");

            passwordField.Click();
            passwordField.SendKeys("Test123$");

            confPasswordField.Click();
            confPasswordField.SendKeys("Test123$");

            var registerButton = driver.FindElement(By.XPath("/html/body/div/div/div/form/button"));
            registerButton.Click();

            var user = driver.FindElement(By.LinkText("Hello test1@example.com!")).Text;

            StringAssert.Contains(user, "Hello test1@example.com!");
            driver.Quit();

        }


    }
}
