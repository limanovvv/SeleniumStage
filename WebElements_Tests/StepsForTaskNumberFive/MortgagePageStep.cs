using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.Steps
{
	[Binding]
	public class MortgagePageStep : BaseStep
    {
        public MortgagePageStep(SeleniumBuilder builder) : base(builder) { }
        [Then("Открыть страницу получения ипотеки")]
        [When("Открыть страницу получения ипотеки")]
        public void OpenMortgagePage()
        {
            
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/classic-mortgage-program");
            IWebElement mortgagePageHeader = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Расчёт ипотеки')]")));
        }

        
    }
}

