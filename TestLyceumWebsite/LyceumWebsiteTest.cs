using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestLyceumWebsite
{
    public class LyceumWebsiteTest : IDisposable
    {
        private IWebDriver driver;
        private const string PathToDriver = "../../../../";
        private const string WebsiteUrl = @"http://lyceum1.minsk.edu.by/";
        private readonly By _submitInput = By.XPath("//input[@value='Найти']");
        private readonly By _textInput = By.XPath("//input[@name='text']");

        public LyceumWebsiteTest()
        {
            driver = new ChromeDriver(PathToDriver);
            driver.Navigate().GoToUrl(WebsiteUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Theory]
        [InlineData("Драчан")]
        [InlineData("Гребень")]
        [InlineData("Ананич")]
        [InlineData("Коржик")]
        [InlineData("Карловский")]
        public void Search_MemberOfLyceum_ReturnNotNull(string lastName)
        {
            IWebElement submit = driver.FindElement(_submitInput);
            IWebElement input = driver.FindElement(_textInput);
            input.SendKeys(lastName);
            submit.Click();
            var linkXPath = By.XPath($"//yass-div[@class='b-serp-item__content']//*[contains(text(),'{lastName}')]");
            var links = driver.FindElements(linkXPath);
            Assert.NotEmpty(links);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }

}
