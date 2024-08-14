using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.Steps
{
    [Binding]
    public class FamilyMortgageProgramStep : BaseStep
	{
        public FamilyMortgageProgramStep(SeleniumBuilder builder) : base(builder) { }

        [Then("Переключиться на программу 'Семейная ипотека'")]
        public void SwitchToFamilyMortgageProgram()
        {
            IWebElement familyMortgageProgram = driver.Wait(5).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Семейная ипотека — 6%')]/parent::div")));
            familyMortgageProgram.Click();
            IWebElement familyMortgageProgramActive = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Семейная ипотека — 6%')]/parent::div[contains(@class, '_active')]")));
        }

        [Then("В блоке расчета ежемесячного платежа проверить значение ставки по программе 'Семейная ипотека'")]
        public void CheckFamilyMortgageRate()
        {
            IWebElement rateElement = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-testid = 'interest-rate']")));
            string rateText = rateElement.Text;
            StringAssert.IsMatch(@"\d*%", rateText, "Значение ставки отображается некорректно");
        }
    }
}

