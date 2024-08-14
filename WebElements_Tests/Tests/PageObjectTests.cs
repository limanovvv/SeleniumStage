using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using WebElements_Tests.Pages;

namespace WebElements_Tests.Tests
{
	public class PageObjectTests
	{
        private SeleniumBuilder _builder = new SeleniumBuilder();

        public IWebDriver driver;

        public WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = _builder.WithTimeout(TimeSpan.FromSeconds(5)).Build();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {

                IWebElement cookie = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Хорошо')]/ancestor::psb-button")));
                cookie.Click();
            }
            catch (Exception ex)
            {

            }
        }

        [TearDown]
        public void Teardown()
        {

            driver.Quit();

        }


        //5.2
        [Test]
        public void FillFormAndVerifyEnteredData()
        {
            // Переход на страницу дебетовой карты
            DebitCardYourCashbackPageNew debitCardPage = new DebitCardYourCashbackPageNew(driver);

            // Генерация данных
            ApplicationData generatedData = new ApplicationData
            {
                Surname = FieldsGenerator.GenerateLastName(),
                Name = FieldsGenerator.GenerateFirstName(),
                MiddleName = FieldsGenerator.GenerateMiddleName(),
                BirthDate = FieldsGenerator.GenerateDateOfBirth(),
                PhoneNumber = FieldsGenerator.GenerateRandomPhoneNumber(),
                
            };

            // Заполнение полей формы
            debitCardPage.FillPageFields(generatedData);
            
            // Переход на страницу проверки введенных данных
            ReviewPageNew verificationPage = new ReviewPageNew(driver);

            // Проверка введенных данных
            verificationPage.VerifyEnteredData(generatedData);
        }



























        //5.3
        [Test]
        public void FillConsumerLoanFormAndVerifyEnteredData()
        {
            // Создаем экземпляр страницы заявки на потребительский кредит
            ConsumerLoanPage consumerLoanPage = new ConsumerLoanPage(driver);

            // Генерация данных
            ApplicationData generatedData = new ApplicationData
            {
                Surname = FieldsGenerator.GenerateLastName(),
                Name = FieldsGenerator.GenerateFirstName(),
                MiddleName = FieldsGenerator.GenerateMiddleName(),
                BirthDate = FieldsGenerator.GenerateDateOfBirth(),
                PhoneNumber = FieldsGenerator.GenerateRandomPhoneNumber(),

            };

            // Заполняем форму
            consumerLoanPage.FillPageFields(generatedData);

            // Создаем экземпляр страницы проверки данных
            ReviewPageNew reviewPage = new ReviewPageNew(driver);

            // Проверяем, что данные корректны
            reviewPage.VerifyEnteredData(generatedData);
        }


    }
}

