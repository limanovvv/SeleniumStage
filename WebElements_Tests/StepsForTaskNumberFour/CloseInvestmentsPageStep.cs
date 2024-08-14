using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForTaskNumberFour
{
	public class CloseInvestmentsPageStep
	{
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public CloseInvestmentsPageStep(SeleniumBuilder builder)
        {
            this.driver = builder.WebDriver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [When("закрыть страницу инвестиций")]
        public void CloseInvestmentsPage()
        {
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Вернем все проценты по кредиту')]/ancestor::h1")));
        }
    }
}

