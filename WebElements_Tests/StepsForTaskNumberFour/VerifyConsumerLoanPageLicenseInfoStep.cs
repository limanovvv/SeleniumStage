using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForTaskNumberFour
{
	public class VerifyConsumerLoanPageLicenseInfoStep
	{
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public VerifyConsumerLoanPageLicenseInfoStep(SeleniumBuilder builder)
        {
            this.driver = builder.WebDriver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Then("проверить отображение информации о номере лицензии и дате на странице кредита")]
        public void VerifyConsumerLoanPageLicenseInfo()
        {
            var copyrightsLoanPageElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rtl-copyrights")));
            string copyrightsLoanPageText = copyrightsLoanPageElement.Text;

            StringAssert.IsMatch(@"Генеральная лицензия на осуществление банковских операций № \d\d\d\d от \d\d .* \d\d\d\d", copyrightsLoanPageText, "Данные на странице кредита отображаются некорректно и не соответствуют маске");
        }
    }
}

