using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebElements_Tests.Pages
{
	public class BaseApplicationPageChain
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

        public ApplicationData EnteredData { get; private set; } = new ApplicationData();

        public BaseApplicationPageChain(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public BaseApplicationPageChain FillSurname(string surname)
        {
            EnteredData.Surname = surname;
            surnameInput.SendKeys(surname);
            return this;
        }

        public BaseApplicationPageChain FillName(string name)
        {
            EnteredData.Name = name;
            nameInput.SendKeys(name);
            return this;
        }

        public BaseApplicationPageChain FillMiddleName(string middleName)
        {
            EnteredData.MiddleName = middleName;
            middleNameInput.SendKeys(middleName);
            return this;
        }

        

        public BaseApplicationPageChain FillBirthDate(string birthDate)
        {
            EnteredData.BirthDate = birthDate;
            birthDateInput.SendKeys(birthDate);
            return this;
        }

        public BaseApplicationPageChain FillPhoneNumber(string phoneNumber)
        {
            EnteredData.PhoneNumber = phoneNumber;
            phoneNumberInput.SendKeys(phoneNumber);
            return this;
        }



        public BaseApplicationPageChain SelectRussianCitizenship()
        {
            citizenshipInput.Click();
            IWebElement citizenshipRussian = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[@data-test-id = 'select-option-0']")));
            citizenshipRussian.Click();
            return this;
        }



        public BaseApplicationPageChain AgreeToPersonalDataProcessing()
        {
            if (!personalDataCheckBox.Selected)
            {
                personalDataCheckBox.Click();
            }
            return this;
        }



        public BaseApplicationPageChain AgreeToPromotion()
        {
            if (!promotionCheckBox.Selected)
            {
                promotionCheckBox.Click();
            }
            return this;
        }



        public void ClickContinueButton()
        {
            continueButton.Click();
        }
    }
}

