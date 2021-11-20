using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private readonly By _submitInput = By.XPath("//input[@value='РќР°Р№С‚Рё']");
        private readonly By _textInput = By.XPath("//input[@name='text']");
        private readonly By _searchLink2 = By.XPath("//yass-span[text()='Р СѓРєРѕРІРѕРґСЃС‚РІРѕ Р»РёС†РµСЏ - Р›РёС†РµР№ в„–1 Рі. ']");
        private readonly By _searchLink = By.XPath("//yass-span[text()='Р СѓРєРѕРІРѕРґСЃС‚РІРѕ Р»РёС†РµСЏ - Р›РёС†РµР№ в„–1 Рі. РњРёРЅСЃРєР°']");
        private readonly By _memberCard = By.ClassName("excerpt_content");
        private readonly By _memberName = By.ClassName("name");
        private readonly By _showInfo = By.ClassName("show-hide");

        public LyceumWebsiteTest()
        {
            driver = new ChromeDriver(PathToDriver);
            driver.Navigate().GoToUrl(WebsiteUrl);
            driver.Manage().Window.Maximize();
        }

        [Theory]
        [InlineData("Гребень")]//Not a member of Administration
        [InlineData("Драчан")]//Not a member of Administration
        [InlineData("Лазарь")]
        [InlineData("Снежков")]
        [InlineData("Ананич")]
        [InlineData("Коржик")]
        [InlineData("Шамшур")]
        [InlineData("Карловский")]
        public void Search_MemberOfAdministration_OpenMemberDescription(string lastName)
        {
            IWebElement submit = driver.FindElement(_submitInput);
            IWebElement input = driver.FindElement(_textInput);
            input.SendKeys(lastName);
            submit.Click();
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
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
