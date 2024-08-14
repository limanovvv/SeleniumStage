using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForWindow
{
	public class ButtonStep
	{
        private readonly IWebDriver driver;
        private readonly ScenarioContext scenarioContext;

        public ButtonStep(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            this.scenarioContext = scenarioContext;
        }

        [When("Нажать на кнопку с текстом '(.*)'")]
        public void ClickButtonWithText(string buttonText)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement button = wait.Until(driver => driver.FindElement(By.XPath($"//*[contains(text(), '{buttonText}')]/ancestor::button")));
            button.Click();
            scenarioContext["buttonText"] = buttonText;
        }
    }
}

