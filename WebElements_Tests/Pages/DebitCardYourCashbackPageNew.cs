using System;
using OpenQA.Selenium;
using WebElements_Tests.Pages;

namespace WebElements_Tests
{
	public class DebitCardYourCashbackPageNew : BaseApplicationPage
    {
        public DebitCardYourCashbackPageNew(IWebDriver webDriver) : base(webDriver)
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");
        }

        public void FillPageFields(ApplicationData data)
        {
            FillCommonFields(data);
            continueButton.Click();
        }
    }
}

