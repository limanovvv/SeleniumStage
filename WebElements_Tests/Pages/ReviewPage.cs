using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebElements_Tests
{
	public class ReviewPage
	{
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private IWebElement surnameText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__name')][1]/descendant::b")));
        private IWebElement nameText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__name')][2]/descendant::b")));
        private IWebElement middleNameText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__name')][3]/descendant::b")));
        private IWebElement birthDateText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__birthdate')]/descendant::b")));
        private IWebElement phoneNumberText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__mobile-phone')]/descendant::b")));

        public ReviewPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void VerifyEnteredData(DebitCardYourCashbackPage debitCardPage)
        {
            Assert.AreEqual(debitCardPage.enteredSurname, surnameText.Text, "Фамилия не совпадает");
            Assert.AreEqual(debitCardPage.enteredName, nameText.Text, "Имя не совпадает");
            Assert.AreEqual(debitCardPage.enteredMiddleName, middleNameText.Text, "Отчество не совпадает");
            Assert.AreEqual(debitCardPage.enteredBirthDate, birthDateText.Text, "Дата рождения не совпадает");
            Assert.AreEqual(debitCardPage.enteredPhoneNumber, phoneNumberText.Text, "Номер телефона не совпадает");
        }
    }
}

