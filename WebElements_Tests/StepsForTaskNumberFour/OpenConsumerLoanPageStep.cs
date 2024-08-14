using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForTaskNumberFour
{
	public class OpenConsumerLoanPageStep
	{
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public OpenConsumerLoanPageStep(SeleniumBuilder builder)
        {
            this.driver = builder.WebDriver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [When("открыть страницу получения потребительского кредита")]
        public void OpenConsumerLoanPage()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/consumer-loan");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Вернем все проценты по кредиту')]/ancestor::h1")));
        }
    }
}

