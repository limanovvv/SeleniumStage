using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebElements_Tests.Pages;

namespace WebElements_Tests
{
	public class ConsumerLoanPage : DebitCardYourCashbackPageNew
    {
        private IWebElement employmentStatusInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-select[@name='RussianEmployment']")));
        private IWebElement bkiRequestCheckBox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//rui-checkbox[@name='BkiRequestAgreementConcent']")));

        public ConsumerLoanPage(IWebDriver webDriver) : base(webDriver)
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/consumer-loan");
        }

        public void FillPageFields(ApplicationData data)
        {
            FillCommonFields(data);
            SelectEmploymentStatus();

            if (!bkiRequestCheckBox.Selected)
            {
                bkiRequestCheckBox.Click();
            }
            continueButton.Click();
        }

        private void SelectEmploymentStatus()
        {
            employmentStatusInput.Click();
            IWebElement employmentOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[@data-test-id = 'select-option-0']")));
            employmentOption.Click();
        }
    }
}

