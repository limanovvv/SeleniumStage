using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.Steps
{
    [Binding]
    public class AcquisitionTabStep : BaseStep
	{
        public AcquisitionTabStep(SeleniumBuilder builder) : base(builder) { }

        [Then("Переключиться на вкладку 'Приобретение'")]
        public void SwitchToAcquisitionTab()
        {
            IWebElement acquisitionTab = driver.Wait(5).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Приобретение')]/ancestor::button")));
            acquisitionTab.Click();
            IWebElement acquisitionTabActive = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Приобретение')]/ancestor::button[@data-appearance='dark']")));
        }
    }
}

