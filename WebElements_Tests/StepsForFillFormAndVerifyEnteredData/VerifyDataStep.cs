using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebElements_Tests.Pages;

namespace WebElements_Tests.FillFormAndVerifyEnteredData
{
	public class VerifyDataStep
	{
        private readonly IWebDriver driver;
        private readonly FillFormStep fillFormSteps;

        public VerifyDataStep(IWebDriver driver, FillFormStep fillFormStep)
        {
            this.driver = driver;
            this.fillFormSteps = fillFormSteps;
        }

        [Then(@"проверить введенные данные")]
        public void ThenVerifyEnteredData()
        {
            ApplicationData data = fillFormSteps.GetApplicationData();
            ReviewPageNew verificationPage = new ReviewPageNew(driver);
            verificationPage.VerifyEnteredData(data);
        }
    }
}

