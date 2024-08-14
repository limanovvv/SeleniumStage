using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.Steps
{
    [Binding]
    public class RefinancingTabStep : BaseStep
	{
        public RefinancingTabStep(SeleniumBuilder builder) : base(builder) { }

        [Then("Переключиться на вкладку 'Рефинансирование'")]
        public void SwitchToRefinancingTab()
        {
            IWebElement refinancingTab = driver.Wait(5).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Рефинансирование')]/ancestor::button")));
            refinancingTab.Click();
            IWebElement refinancingTabActive = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Рефинансирование')]/ancestor::button[@data-appearance='dark']")));
        }
    }
}

