using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;
using WebElements_Tests.Steps;

namespace WebElements_Tests.StepsForVerification
{
    [Binding]
	public class VerificationStep : BaseStep
	{
        public VerificationStep(SeleniumBuilder builder) : base(builder) { }

        [Then(@"проверить, что элемент с XPath ""(.*)"" отображается")]
        public void VerifyElementIsDisplayed(string xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement uniqueElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));

            Assert.IsTrue(uniqueElement.Displayed, $"Element with XPath '{xpath}' not found.");
        }
    }
}

