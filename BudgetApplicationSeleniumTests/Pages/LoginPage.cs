using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace BudgetApplicationSeleniumTests.Pages
{
    class LoginPage
    {
        private static IWebDriver _driver;
        
        public LoginPage(IWebDriver driver) => _driver = driver;

        [CacheLookup]
        public IWebElement Email => _driver.FindElement(By.Id("Email"));

        [CacheLookup]
        public IWebElement Password => _driver.FindElement(By.Id("Password"));

        [CacheLookup]
        public IWebElement PasswordError => _driver.FindElement(By.XPath("*//div[1]/ul/li"));
        [CacheLookup]
        public IWebElement EmailError => _driver.FindElement(By.Id("Email-error"));

        [CacheLookup]
        public IWebElement LoginButton => _driver.FindElement(By.XPath("*//div[1]/section/form/div[5]/button"));

        public void SendTextToField(IWebElement element, string textToSend)
        {
            element.Click();
            element.SendKeys(textToSend);
        }

        public string GetPasswordErrorText() => PasswordError.Text;

        public string GetEmailErrorText() => EmailError.Text;

        public string FindElementByLinkText(string givenText)
        {
            var element = _driver.FindElement(By.LinkText(givenText)).Text;
            return element;
        }
    }
}
