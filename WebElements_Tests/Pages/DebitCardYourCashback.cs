using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;

namespace WebElements_Tests
{
    public class DebitCardYourCashbackPage
    {

        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private IWebElement surnameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='CardHolderLastName']")));
        private IWebElement nameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='CardHolderFirstName']")));
        private IWebElement middleNameInput => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='CardHolderMiddleName']")));
        private IWebElement maleRadioButton => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rui-radio[@data-test-id = 'radio-option-0']")));
        private IWebElement femaleRadioButton => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rui-radio[@data-test-id='radio-option-1']")));
        private IWebElement birthDateInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@data-mat-calendar='mat-datepicker-1']")));
        private IWebElement phoneNumberInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@name = 'Phone']")));
        private IWebElement citizenshipInput => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-select[@name='RussianFederationResident']")));
        private IWebElement personalDataCheckBox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//rui-checkbox[@name='PersonalDataProcessingAgreementConcent']")));
        private IWebElement promotionCheckBox => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//rui-checkbox[@name='PromotionAgreementConcent']")));
        private IWebElement continueButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Продолжить')]/ancestor::button")));

        private List<IWebElement> radioButtonsGender => new List<IWebElement> { maleRadioButton, femaleRadioButton };
      
        public DebitCardYourCashbackPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");
            try
            {

                IWebElement cookie = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Хорошо')]/ancestor::psb-button")));
                cookie.Click();
            }
            catch (Exception ex)
            {

            }
        }

        // Свойства для хранения введенных данных
        public string enteredSurname { get; private set; }
        public string enteredName { get; private set; }
        public string enteredMiddleName { get; private set; }
        public string enteredBirthDate { get; private set; }
        public string enteredPhoneNumber { get; private set; }
        

        public void FillPageFields()
        {

            enteredSurname = FieldsGenerator.GenerateLastName();
            surnameInput.SendKeys(enteredSurname + Keys.Escape);


            enteredName = FieldsGenerator.GenerateFirstName();
            nameInput.SendKeys(enteredName + Keys.Escape);

            enteredMiddleName = FieldsGenerator.GenerateMiddleName();
            middleNameInput.SendKeys(enteredMiddleName + Keys.Escape);

            selectRandomGender();

            enteredBirthDate = FieldsGenerator.GenerateDateOfBirth();

            birthDateInput.SendKeys(Keys.Home + enteredBirthDate);

            enteredPhoneNumber = FieldsGenerator.GenerateRandomPhoneNumber();
            phoneNumberInput.SendKeys(Keys.Home + enteredPhoneNumber);

            selectRussianCitizenship();

            if (!personalDataCheckBox.Selected)
            {
                personalDataCheckBox.Click();
            }
            if (!promotionCheckBox.Selected)
            {
                promotionCheckBox.Click();
            }

            continueButton.Click();
        }

        private void selectRandomGender()
        {
            Random rand = new Random();
            IWebElement sex = radioButtonsGender[rand.Next(radioButtonsGender.Count)];

            if (!sex.Selected)
            {
                sex.Click();
            }
        }

        private void selectRussianCitizenship()
        {
            citizenshipInput.Click();

            IWebElement citizenshipRussian = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[@data-test-id = 'select-option-0']")));
            citizenshipRussian.Click();
        }

        private void selectNotRussianCitizenship()
        {
            citizenshipInput.Click();

            IWebElement citizenshipNotRussian = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//mat-option[@data-test-id = 'select-option-1']")));
            citizenshipNotRussian.Click();
        }


    }
}

