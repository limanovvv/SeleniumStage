using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;

namespace WebElements_Tests
{

    public class NavigationToTests
    {
        private SeleniumBuilder _builder;

        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            _builder = new SeleniumBuilder();
            driver = _builder.WithTimeout(TimeSpan.Zero).Build();

        }
        [TearDown]
        public void Teardown()
        {

            driver.Quit();

        }
        [Test]
        public void CheckUniqueElementOnMainPage()
        {

            driver.Navigate().GoToUrl("м");
            IWebElement uniqueElement = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Финансовые продукты')]")));

            Assert.IsTrue(uniqueElement.Displayed, "Unique element on main page not found.");
        }

        [Test]
        public void CheckUniqueElementOnConsumerLoanPage()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/consumer-loan");
            IWebElement uniqueElement = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Вернем все проценты по кредиту')]/ancestor::h1")));

            Assert.IsTrue(uniqueElement.Displayed, "Unique element on main page not found.");

        }

        [Test]
        public void CheckUniqueElementOnInvestmentsBrokeragePage()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/investmentsbrokerage");
            IWebElement uniqueElement = driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Инвестиции в ценные бумаги')]/ancestor::h1")));

            Assert.IsTrue(uniqueElement.Displayed, "Unique element on main page not found.");

        }


        [Test]
        public void FinancialProductsText_Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

            driver.Navigate().GoToUrl("https://ib.psbank.ru/");

            IWebElement financialProductsHeader = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Финансовые продукты')]/ancestor::div[@class='header']")));
            Assert.IsTrue(financialProductsHeader.Displayed, "Financial products text is not displayed");


        }


        [Test]
        public void BrokerageAgreement_Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("https://ib.psbank.ru/");

            IWebElement invesments = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Инвестиции')]/ancestor::a")));

            invesments.Click();

            IWebElement brokerageAgreement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Брокерский договор')]")));

            brokerageAgreement.Click();

            IWebElement uniqueElementIntoInvestmentsBrokeragePage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Инвестиции в ценные бумаги')]/ancestor::h1")));

            Assert.IsTrue(uniqueElementIntoInvestmentsBrokeragePage.Displayed, "Не произошел переход по указанной ссылке, на страницу Инвестиции");

        }


        [Test]
        public void WorkingWithDifferentBrowserTabs_Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/consumer-loan");
            IWebElement loanPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Вернем все проценты по кредиту')]/ancestor::h1")));

            //Actions actions = new Actions(driver);
            //actions.KeyDown(Keys.Control).SendKeys("t").KeyUp(Keys.Control).Perform();
            driver.SwitchTo().NewWindow(WindowType.Tab);

            wait.Until(driver => driver.WindowHandles.Count > 0);

            driver.SwitchTo().Window(driver.WindowHandles[1]);

            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/investmentsbrokerage");
            IWebElement invesmentsPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Инвестиции в ценные бумаги')]/ancestor::h1")));

            IWebElement copyrights = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rtl-copyrights")));
            string copyrightsText = copyrights.Text;

            StringAssert.IsMatch(@"Генеральная лицензия на осуществление банковских операций № \d\d\d\d от \d\d .* \d\d\d\d", copyrightsText, "Данные отображаются некорректно и не соответствуют маске");

            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            loanPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Вернем все проценты по кредиту')]/ancestor::h1")));

            IWebElement copyrightsLoanPage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//rtl-copyrights")));
            string copyrightsLoanPageText = copyrightsLoanPage.Text;

            Assert.AreEqual(copyrightsText, copyrightsLoanPageText, "Копирайтинги не соответствуют на страницах кредита и брокерского договора ");
        }




        [Test]
        public void MortgagePageTabs_Test()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));
            wait.PollingInterval = TimeSpan.FromMilliseconds(1000);

            // Шаг 1: Открыть страницу получения ипотеки
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/classic-mortgage-program");
            IWebElement mortgagePageHeader = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Расчёт ипотеки' )]")));

            // Шаг 2: Переключиться на вкладку 'Приобретение'
            IWebElement acquisitionTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Приобретение')]/ancestor::button")));
            acquisitionTab.Click();
            IWebElement acquisitionTabActive = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Приобретение')]/ancestor::button[ @data-appearance = 'dark']")));

            // Шаг 3: Переключиться на программу 'Семейная ипотека'
            IWebElement familyMortgageProgram = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Семейная ипотека — 6%')]/parent::div")));
            familyMortgageProgram.Click();
            
            IWebElement familyMortgageDetails = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Семейная ипотека — 6%')]/parent::div[contains(@class, '_active')]")));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//rtl-mortgage-calculator-output//psb-loader/div")));

            // Шаг 4: Проверить значение ставки по программе 'Семейная ипотека'
            IWebElement familyMortgageRate = wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@data-testid = 'interest-rate']")));
            string familyMortgageRateText = familyMortgageRate.Text;
            StringAssert.IsMatch(@"\d*%", familyMortgageRateText, "Значение ставки по программе 'Семейная ипотека' отображается некорректно");

            // Шаг 5: Переключиться на вкладку 'Рефинансирование'
            IWebElement refinancingTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Рефинансирование')]/ancestor::button")));
            refinancingTab.Click();
            IWebElement refinancingTabActive = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Рефинансирование')]/ancestor::button[@data-appearance = 'dark']")));

            // Шаг 6: Переключиться на программу 'Рефинансирование. Семейная ипотека'
            IWebElement familyRefinancingProgram = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Рефинансирование. Семейная ипотека — 6%')]/parent::div")));
            familyRefinancingProgram.Click();
            IWebElement familyRefinancingDetails = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Рефинансирование. Семейная ипотека — 6%')]/parent::div[contains(@class, '_active')]")));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//rtl-mortgage-calculator-output//psb-loader/div")));

            // Шаг 7: Проверить значение ставки по программе 'Рефинансирование. Семейная ипотека'
            IWebElement familyRefinancingRate = wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@data-testid = 'interest-rate']")));
            string familyRefinancingRateText = familyRefinancingRate.Text;
            Assert.AreEqual(familyMortgageRateText, familyRefinancingRateText, "Значение ставки по программе 'Рефинансирование. Семейная ипотека' не идентично результату шага 4");


        }
    }



}



