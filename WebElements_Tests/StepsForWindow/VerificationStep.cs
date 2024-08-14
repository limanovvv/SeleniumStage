using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForWindow
{
	public class VerificationStep
	{
        private readonly IWebDriver driver;
        private readonly ScenarioContext scenarioContext;

        public VerificationStep(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            this.scenarioContext = scenarioContext;
        }

        [Then("Проверить что появилась ошибка '(.*)'")]
        public void CheckErrorAppeared(string errorMessage)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement errorElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'mortgage-calculator-output__alert_show')]")));
            Assert.AreEqual(errorMessage, errorElement.Text, "Ошибка не соответствует ожидаемой");
        }

        [Then("Проверить что кнопка с сохраненным текстом отсутствует")]
        public void CheckSavedButtonIsAbsent()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string buttonText = (string)scenarioContext["buttonText"]; 
            IWebElement button = wait.Until(ExpectedConditions.ElementExists(By.XPath($"//*[contains(text(), '{buttonText}')]/ancestor::button")));
            string actualCssValue = button.GetCssValue("outline");
            string expectedCssValue = "0"; // мб нужна конвертация
            Assert.AreEqual(expectedCssValue, actualCssValue, $"Кнопка с текстом '{buttonText}' все еще отображается");
        }
    }
}

