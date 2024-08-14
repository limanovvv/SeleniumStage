using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.Steps
{
    [Binding]
    public class RefFamilyMortgageProgramStep : BaseStep
	{
        public RefFamilyMortgageProgramStep(SeleniumBuilder builder) : base(builder) { }

        [Then("Переключиться на программу 'Рефинансирование. Семейная ипотека'")]
        public void SwitchToRefFamilyMortgageProgram()
        {
            IWebElement refFamilyMortgageProgram = driver.Wait(5).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Рефинансирование. Семейная ипотека — 6%')]/parent::div")));
            refFamilyMortgageProgram.Click();
            IWebElement refFamilyMortgageProgramActive = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Рефинансирование. Семейная ипотека — 6%')]/parent::div[contains(@class, '_active')]")));
        }

        [Then("В блоке расчета ежемесячного платежа проверить значение ставки по программе 'Рефинансирование. Семейная ипотека'")]
        public void CheckRefFamilyMortgageRate()
        {
            IWebElement rateElement = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-testid = 'interest-rate']")));
            string rateText = rateElement.Text;
            StringAssert.IsMatch(@"\d*%", rateText, "Значение ставки отображается некорректно");
        }
    }
}

