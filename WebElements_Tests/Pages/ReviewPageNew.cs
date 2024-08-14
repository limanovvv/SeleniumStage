using System;
using iTextSharp.text.pdf;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebElements_Tests.Pages;

namespace WebElements_Tests
{
	public class ReviewPageNew
	{
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private IWebElement surnameText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__name')][1]/descendant::b")));
        private IWebElement nameText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__name')][2]/descendant::b")));
        private IWebElement middleNameText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__name')][3]/descendant::b")));
        private IWebElement birthDateText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__birthdate')]/descendant::b")));
        private IWebElement phoneNumberText => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'confirm-section__mobile-phone')]/descendant::b")));

        public ReviewPageNew(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void VerifyEnteredData(ApplicationData expectedData)
        {
            Assert.AreEqual(expectedData.Surname, surnameText.Text, "Фамилия не соответствует введенному значению");
            Assert.AreEqual(expectedData.Name, nameText.Text, "Имя не соответствует введенному значению");
            Assert.AreEqual(expectedData.MiddleName, middleNameText.Text, "Отчество не соответствует введенному значению");
            Assert.AreEqual(expectedData.BirthDate, birthDateText.Text, "Дата рождения не соответствует введенному значению");
            Assert.AreEqual(expectedData.PhoneNumber, phoneNumberText.Text, "Номер телефона не соответствует введенному значению");
            
        }
    }
}

