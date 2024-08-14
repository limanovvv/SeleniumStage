using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForTaskNumberFour
{
	public class OpenInvestmentsPageInNewTabStep
	{
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public OpenInvestmentsPageInNewTabStep(SeleniumBuilder builder)
        {
            this.driver = builder.WebDriver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [When("открыть страницу инвестиций в новой вкладке")]
        public void OpenInvestmentsPageInNewTab()
        {
            driver.SwitchTo().NewWindow(WindowType.Tab);
            wait.Until(driver => driver.WindowHandles.Count > 0);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/investmentsbrokerage");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Инвестиции в ценные бумаги')]/ancestor::h1")));
        }
    }
}

