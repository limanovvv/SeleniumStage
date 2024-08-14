using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebElements_Tests.Pages;

namespace WebElements_Tests
{
	public class BaseApplicationPage
	{
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;
        

        protected IWebElement surnameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='CardHolderLastName']")));
        protected IWebElement nameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='CardHolderFirstName']")));
        protected IWebElement middleNameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='CardHolderMiddleName']")));
        protected IWebElement maleRadioButton => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rui-radio[@data-test-id = 'radio-option-0']")));
        protected IWebElement femaleRadioButton => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rui-radio[@data-test-id='radio-option-1']")));
        protected IWebElement birthDateInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@data-mat-calendar='mat-datepicker-1']")));
        protected IWebElement phoneNumberInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name = 'Phone']")));
        protected IWebElement citizenshipInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-select[@name='RussianFederationResident']")));
        protected IWebElement personalDataCheckBox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//rui-checkbox[@name='PersonalDataProcessingAgreementConcent']")));
        protected IWebElement promotionCheckBox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//rui-checkbox[@name='PromotionAgreementConcent']")));
        protected IWebElement continueButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Продолжить')]/ancestor::button")));

        private List<IWebElement> radioButtonsGender => new List<IWebElement> { maleRadioButton, femaleRadioButton };

        public BaseApplicationPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void FillCommonFields(ApplicationData data)
        {

            surnameInput.SendKeys(data.Surname);
            nameInput.SendKeys(data.Name);
            middleNameInput.SendKeys(data.MiddleName);
            birthDateInput.SendKeys(data.BirthDate);
            phoneNumberInput.SendKeys(data.PhoneNumber);

            SelectRandomGender();
            SelectRussianCitizenship();

            if (!personalDataCheckBox.Selected)
            {
                personalDataCheckBox.Click();
            }
            if (!promotionCheckBox.Selected)
            {
                promotionCheckBox.Click();
            }
        }


        private void SelectRandomGender()
        {
            Random rand = new Random();
            IWebElement sex = radioButtonsGender[rand.Next(radioButtonsGender.Count)];

            if (!sex.Selected)
            {
                sex.Click();
            }
        }

        private void SelectRussianCitizenship()
        {
            citizenshipInput.Click();

            IWebElement citizenshipRussian = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[@data-test-id = 'select-option-0']")));
            citizenshipRussian.Click();
        }

        private void SelectNotRussianCitizenship()
        {
            citizenshipInput.Click();

            IWebElement citizenshipNotRussian = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[@data-test-id = 'select-option-1']")));
            citizenshipNotRussian.Click();
        }


    }
}

