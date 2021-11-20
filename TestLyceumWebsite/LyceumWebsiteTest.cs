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
<<<<<<< HEAD
        private readonly By _searchLink2 = By.XPath("//yass-span[text()='Руководство лицея - Лицей №1 г. ']");
        private readonly By _searchLink = By.XPath("//yass-span[text()='Руководство лицея - Лицей №1 г. Минска']");
        private readonly By _memberCard = By.ClassName("excerpt_content");
        private readonly By _memberName = By.ClassName("name");
        private readonly By _showInfo = By.ClassName("show-hide");
=======
>>>>>>> af16464 (change test logic)

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
<<<<<<< HEAD
            Thread.Sleep(1000);
            IWebElement searchResult;
            try
            {
                searchResult = driver.FindElement(_searchLink);
            }
            catch (NoSuchElementException)
            {
                searchResult = driver.FindElement(_searchLink2);
            }
            searchResult.Click();
            var windowHandle = driver.WindowHandles;
            driver.SwitchTo().Window(windowHandle.Last());
            IList<IWebElement> adminstration = driver.FindElements(_memberCard);
            for (var index = 1; index < adminstration.Count; index++)
            {
                var memberName = adminstration[index].FindElement(_memberName);
                if (memberName.Text.Contains(lastName))
                {
                    var info = adminstration[index].FindElement(_showInfo);
                    info.Click();
                    break;
                }
            }
            Thread.Sleep(3000);
=======
            var linkXPath = By.XPath($"//yass-div[@class='b-serp-item__content']//*[contains(text(),'{lastName}')]");
            var links = driver.FindElements(linkXPath);
            Assert.NotEmpty(links);
>>>>>>> af16464 (change test logic)
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}