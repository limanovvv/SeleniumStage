using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Org.BouncyCastle.Bcpg.OpenPgp;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;

namespace WebElements_Tests
{
    public class Calculator
    {
        private SeleniumBuilder _builder = new SeleniumBuilder();

        public IWebDriver driver;

        public WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = _builder.WithURL("https://ib.psbank.ru/store/products/family-mortgage-program").WithTimeout(TimeSpan.FromSeconds(5)).Build();

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


        //4.1
        public void SetSliderValue(IWebElement slider, int targetValue)
        {
            
            int currentValue = Int32.Parse(slider.GetAttribute("value").Replace(" ", "")); // может не сработать

            int minValue = Int32.Parse(slider.GetAttribute("min"));
            int maxValue = Int32.Parse(slider.GetAttribute("max"));

            double sliderWidth = slider.FindElement(By.XPath("./ancestor::rui-slider")).Size.Width;

            double offset = (targetValue) * (sliderWidth / (maxValue - minValue));

            Actions moveSlider = new Actions(driver);

            //moveSlider.ClickAndHold(slider.FindElement(By.XPath("./ancestor::rui-slider"))).MoveByOffset(-(int)sliderWidth/2, 0).MoveByOffset((int)offset, 0).Release().Perform();

//            moveSlider.ClickAndHold(slider.FindElement(By.XPath("./ancestor::rui-slider")))
//.SendKeys(Keys.Home)
//.SendKeys(string.Join(string.Empty, Enumerable.Repeat(Keys.Right, targetValue).ToArray()))
//.Release().Perform();

            // Устанавливаем значение слайдера через JavaScript
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

            string script = "arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('change'));";
            jsExecutor.ExecuteScript(script, slider, targetValue);
        }
        

        [Test]
        public void SetSliderValue_Test()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");
            IWebElement slider = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Кафе и рестораны')]/following::input[1]")));

            new Actions(driver).MoveToElement(slider).Perform();
            int targetValue = 1234;
            SetSliderValue(slider, targetValue);

            IWebElement sliderFinal = driver.FindElement(By.XPath("//*[contains(text(), 'Кафе и рестораны')]/following::input[1]"));
            int newValue = Int32.Parse(sliderFinal.GetAttribute("value").Replace(" ", ""));

            Assert.That(newValue, Is.EqualTo(targetValue));

        }










        //Задание от Данила
        private IWebElement GetElement(string xPath)
        {

            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xPath)));

            return element;
        }

      

        class PageObject
        {
            

            public string acquisition = "//*[contains(text(), 'Приобретение')]/ancestor::button[1]";

            public string familyMortgage = "//div[contains(text(), 'Семейная ипотека')]/parent::div";

            public string loanSum = "//*[@data-testid = 'loan-sum']";

            public string mortgagedObject = "//*[contains(text(), 'Объект ипотеки')]/ancestor::rui-form-field-wrapper";

            public string choiseFlat = "//*[contains(text(), 'Квартира в готовом доме')]/parent::mat-option";

            public string motherCapital = "//*[contains(text(), 'Использовать материнский капитал')]/ancestor::label";

            public string motherCapitalInput = "//*[contains(text(), ' Материнский капитал ')]/following::input[1]";

        }


        [Test]
        public void InternetBankTest()
        {
            
            PageObject pageObject = new PageObject();

            //поиск и клик по кнопке приобретение
            IWebElement acquisition = GetElement(pageObject.acquisition);
            acquisition.Click();


            //поиск и клик по окну семейная ипотека
            IWebElement familyMortgage = GetElement(pageObject.familyMortgage);
            familyMortgage.Click();

            //забираем изначальную сумму кредита
            IWebElement loanSumStart = GetElement(pageObject.loanSum);
            string loanSumStartValue = loanSumStart.Text;

            //поиск и клик по выпадающему списку объект ипотеки
            IWebElement mortgagedObject = GetElement(pageObject.mortgagedObject);
            mortgagedObject.Click();

            //выбираем "Квартира в готовом доме"
            IWebElement choiseFlat = GetElement(pageObject.choiseFlat);
            choiseFlat.Click();

            //активируем свитчер "Использовать материнский капитал"
            IWebElement motherCapital = GetElement(pageObject.motherCapital);
            motherCapital.Click();

            //изменяем сумму материнского капитала
            IWebElement motherCapitalInput = GetElement(pageObject.motherCapitalInput);
            motherCapitalInput.Clear();
            motherCapitalInput.SendKeys("300000");

            //забираем финальную сумму кредита
            try
            {

                wait.Until(driver => !GetElement(pageObject.loanSum).Text.Equals(loanSumStartValue));
            }
            catch (Exception ex)
            {
                Assert.Fail(" ");
            }
            //string loanSumFinalValue = loanSumFinal.Text;

            ////проверка того, что сумма кредита изменилась
            //Assert.AreNotEqual(loanSumFinalValue, loanSumStartValue, "сумма кредита не изменилась");

        }





        //4.2
        public void ManageCategories(string categoryToSelect)
        {
            IWebElement otherCategoriesButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Выбрать другие категории')]/ancestor::button")));
            otherCategoriesButton.Click();

            // Находим все чек-боксы категорий
            IReadOnlyCollection<IWebElement> checkBoxes = wait.Until(driver => driver.FindElements(By.XPath("//*[@class = 'categories-list']/li")));

            // Создаем словарь чекбоксов с их текстом
            Dictionary<IWebElement, string> checkBoxDict = new Dictionary<IWebElement, string>();

            for(int i = 0; i < checkBoxes.Count; i++)
            {
                checkBoxDict.Add(checkBoxes.ElementAt(i), checkBoxes.ElementAt(i).Text);
            }

            // Получаем список выбранных чек-боксов
            var selectedCheckBoxes = checkBoxes.Where(cb => cb.FindElement(By.XPath(".//input")).Selected).ToList();

            //проверяем нет ли категории которую нужно добавить в существующих активных категориях
            foreach (var scb in selectedCheckBoxes)
            {
                if (scb.Text == categoryToSelect)
                {
                    return;
                }
            }

            // Если уже выбрано три категории, снимаем галочку с одной из них
            if (selectedCheckBoxes.Count == 3)
            {

                selectedCheckBoxes.First().FindElement(By.XPath(".//rui-checkbox")).Click();
            }

            // Выбираем новую категорию 
            IWebElement newCategory = checkBoxDict.FirstOrDefault(kv => kv.Value.Contains(categoryToSelect)).Key;

newCategory.FindElement(By.XPath(".//rui-checkbox")).Click();

            // кнопка "Выбрать" стала кликабельной
            IWebElement chooseButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), ' Выбрать ')]/ancestor::button[contains(@class, 'confirm')]")));
            chooseButton.Click();
        }

        [Test]
        public void ManageCategoriesTest()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");

            string str = "Общественный транспорт";

            ManageCategories(str);

            IWebElement otherCategoriesButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Выбрать другие категории')]/ancestor::button")));
            otherCategoriesButton.Click();

            IWebElement checkBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[contains(text(), ' {str} ')]/ancestor::li/descendant::label")));


            Assert.IsTrue(checkBox.GetAttribute("class").Contains("_checked"), $"Категория {str} не выбрана");
        }






        //4.3
        public void ManageDropDownList(string surnameToInput)
        {
            //набираем первые две буквы фамилии в поле ввода
            IWebElement inputSurname = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Фамилия')]/following::input[1]")));
            inputSurname.SendKeys(surnameToInput.Substring(0, 2));

            //забираем элементы выпадающего списка 
            var elementsFromDropDownList = wait.Until(driver => driver.FindElements(By.XPath("//div[@role = 'listbox']/mat-option")));

            //для каждого элемента проверяем не является ли он искомой фамилией
            foreach (var element in elementsFromDropDownList)
            {
                if (element.Text.ToLower().Equals(surnameToInput.ToLower()))
                {
                    element.Click();
                    return;
                }
            }


        }

        [Test]
        public void ManageDropDownListTest()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");

            string surname = "Лиманов";

            ManageDropDownList(surname);

            IWebElement inputSurname = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Фамилия')]/following::input[1]")));

            Assert.AreEqual(surname, inputSurname.GetAttribute("value"), "Фамилия введена некорректно");

        }







        //4.4
        public void SetDateOfBirth(string dateToSet)
        {
            // Разделяем входную дату на день, месяц и год
            string dayString = dateToSet.Substring(0, 2);
            string monthString = dateToSet.Substring(3, 2);
            string yearString = dateToSet.Substring(6);

            int year = int.Parse(yearString);
            int month = int.Parse(monthString);
            int day = int.Parse(dayString);

            // Находим иконку для открытия Datepicker
            IWebElement inputDateOfBirth = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Дата рождения')]/following::rui-icon[1]")));
            inputDateOfBirth.Click();

            // Выбираем год
            SelectYear(year);

            // Выбираем месяц
            SelectMonth(month);

            // Выбираем день
            SelectDay(day);
        }

        private void SelectYear(int year)
        {
            
            while (true)
            {
                IWebElement yearElement = null;
                try
                {
                    yearElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//td[contains(@aria-label, {year})]")));
                }
                catch 
                {
                    // Если год не найден, переходим к предыдущему периоду
                    IWebElement previousButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'mat-calendar-previous-button')]")));
                    previousButton.Click();
                }

                if (yearElement != null)
                {
                    yearElement.Click();
                    break;
                }
            }
        }

        private void SelectMonth(int month)
        {
            // Переходим к выбору месяца
            
            IWebElement monthElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//td[contains(@aria-label, '{month.ToString("D2", System.Globalization.CultureInfo.CurrentCulture)}.')]")));
            monthElement.Click();
        }

        private void SelectDay(int day)
        {
            // Переходим к выбору дня
            IWebElement dayElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//td[starts-with(@aria-label, '{day.ToString("D2", System.Globalization.CultureInfo.CurrentCulture)}.')]")));
            dayElement.Click();
        }

        [Test]
        public void SetDateOfBirthTest()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");

            string expectedDate = "19.05.1978";
            SetDateOfBirth(expectedDate);

            IWebElement dateOfBirth = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Дата рождения')]//following::input[1]")));

            string actualDate = dateOfBirth.GetAttribute("value");
            Assert.That(actualDate, Is.EqualTo(expectedDate), "Даты не совпадают");
        }










        //4.5
        private string downloadPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads.pdf");


        public string ManageDownload(IWebElement downloadButton)
        {

            // кликаем по кнопке скачивания
            downloadButton.Click();

            // Переключаемся на новую вкладку
            driver.SwitchTo().Window(driver.WindowHandles[1]);

            // Загружаем файл
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(driver.Url, downloadPath);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            // Забираем содержимое файла
            using (PdfReader reader = new PdfReader(downloadPath))
            {
                string textFromPage = PdfTextExtractor.GetTextFromPage(reader, 1);
                return textFromPage;
            }
           
        }

        [Test]
        public void ManageDownloadTest()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");

            IWebElement downloadButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), ' Дебетовая карта ')]//ancestor::psb-document")));
            
            string expectedHeader = "Дебетовая карта «Твой кешбэк»";
            string actualHeader = null;

            try
            {
                actualHeader = ManageDownload(downloadButton);
            }
            catch (Exception ex)
            {
                Assert.Fail("Ошибка при загрузке файла" + ex.Message);
            }

            Assert.IsTrue(actualHeader.Contains(expectedHeader), "шапка не соответствует ожидаемой");

        }










        //4.6
        public string FaqManage(string question)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[contains(text(), '{question}')]//following::psb-icon[1]")));
            element.Click();

            IWebElement textElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'expansion-body')]")));

            return textElement.Text;

        }
        [Test]
        public void FaqManageTest()
        {
            driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/your-cashback-new");

            string question = "При каких условиях обслуживание карты будет бесплатным?";
            string expectedAnswer = "Бесплатное обслуживание при ежемесячных покупках по карте от 10 000 рублей и для клиентов ПСБ Private Banking.";

            Assert.That(FaqManage(question), Is.EqualTo(expectedAnswer), "неверный ответ");

        }




    }


}



    

    
