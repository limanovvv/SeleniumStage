using System;
using System.Net;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumInitialize_Builder;
using static NUnit.Framework.Constraints.Tolerance;


namespace WebElements_Tests.Tests;


public class Tests
{
    private SeleniumBuilder _builder;

    public IWebDriver driver;

    //IWebElement dropdownList_MortgageObject;
    //IWebElement button_FillThroughGovermentServices;
    //IWebElement card_FamilyMortgage;
    //IWebElement switcher_LifeInsurance;
    //IWebElement field_LoanTerm;

    [SetUp]
    public void Setup()
    {

        _builder = new SeleniumBuilder();
        driver = _builder.WithURL("https://ib.psbank.ru/store/products/classic-mortgage-program").WithTimeout(TimeSpan.FromSeconds(5)).Build();
      
    }
    [TearDown]
    public void Teardown()
    {

        driver.Quit();

    }



    //Задание 1. Поиск элементов на странице

    [Test]
    public void Has_DropdownList_MortgageObject_Test()
    {
        IWebElement dropdownList_MortgageObject = driver.FindElement(By.XPath("//*[@data-testid='calc-input-mortgageCreditType']"));

        //IWebElement dropdownList_MortgageObject = driver.FindElement(By.XPath("//*[contains(text(), 'Объект ипотеки')]"));

        Assert.NotNull(dropdownList_MortgageObject, "Could not find the element");
       
    }

    [Test]
    public void Has_Button_FillThroughGovermentServices_Test()
    {
        //IWebElement button_FillThroughGovermentServices = driver.FindElement(By.XPath("//*[contains(text(), 'Заполнить через Госуслуги')/../..]")); span/родитель/родитель
        IWebElement button_FillThroughGovermentServices = driver.FindElement(By.XPath("//button[@appearance='primary' and @icon='gosuslugi']"));
        Assert.NotNull(button_FillThroughGovermentServices, "Could not find the element");
    }

    [Test]
    public void Has_Card_FamilyMortgage_Test()
    {
        IWebElement card_FamilyMortgage = driver.FindElement(By.XPath("//div[contains(text(), 'Семейная ипотека')]/.."));
        //IWebElement card_FamilyMortgage = driver.FindElement(By.XPath("//div[contains(@class, 'brands-cards__header') and text()='Семейная ипотека — 6%']"));
        Assert.NotNull(card_FamilyMortgage, "Could not find the element");

        
    }

    [Test]
    public void Has_Switcher_LifeInsuranceTest()
    {
        IWebElement switcher_LifeInsurance = driver.FindElement(By.XPath("//*[contains(text(), 'Страхование жизни')]/ancestor::psb-switcher"));
        //IWebElement switcher_LifeInsurance = driver.FindElement(By.XPath("//*[contains(text(), 'Страхование жизни')]/ancestor::psb-switcher[contains(@class, 'deltas__switcher')]"));

        Assert.NotNull(switcher_LifeInsurance, "Could not find the element");
    }

    [Test]
    public void Has_Field_LoanTerm_Test()
    {
        IWebElement field_LoanTerm = driver.FindElement(By.XPath("//rui-range-slider[@id='loanPeriod']"));

        Assert.NotNull(field_LoanTerm, "Could not find the element");
    }



    ////Задание 2. Атрибуты элементов

    [Test]
    public void DropdownList_MortgageObject_DisplayedAndEnable_Test()
    {
        IWebElement dropdownList_MortgageObject = driver.FindElement(By.XPath("//*[@data-testid='calc-input-mortgageCreditType']"));

        Assert.IsTrue(dropdownList_MortgageObject.Displayed, "Dropdown list Mortgage Object is not visible");
        Assert.IsTrue(dropdownList_MortgageObject.Enabled, "Dropdown list Mortgage Object is not enabled");
    }

    [Test]
    public void Button_FillThroughGovermentServices_DisplayedAndEnable_Test()
    {
        IWebElement button_FillThroughGovermentServices = driver.FindElement(By.XPath("//button[@appearance='primary' and @icon='gosuslugi']"));

        Assert.IsTrue(button_FillThroughGovermentServices.Displayed, "Button FillThroughGovermentServices is not visible");
        Assert.IsTrue(button_FillThroughGovermentServices.Enabled, "Button FillThroughGovermentServices is not enabled");
    }

    [Test]
    public void Card_FamilyMortgage_DisplayedAndEnable_Test()
    {
        IWebElement card_FamilyMortgage = driver.FindElement(By.XPath("//*[contains(text(), 'Семейная ипотека')]/.."));

        Assert.IsTrue(card_FamilyMortgage.Displayed, "Card FamilyMortgage is not visible");
        Assert.IsTrue(card_FamilyMortgage.Enabled, "Card FamilyMortgage is not enabled");
    }

    [Test]
    public void Switcher_LifeInsurance_DisplayedAndEnable_Test()
    {
        IWebElement switcher_LifeInsurance = driver.FindElement(By.XPath("//*[contains(text(), 'Страхование жизни')]/ancestor::psb-switcher"));

        Assert.IsTrue(switcher_LifeInsurance.Displayed, "Switcher LifeInsurance is not visible");
        Assert.IsTrue(switcher_LifeInsurance.Enabled, "Switcher LifeInsurance is not enabled");
    }

    [Test]
    public void Field_LoanTerm_DisplayedAndEnable_Test()
    {
        IWebElement field_LoanTerm = driver.FindElement(By.XPath("//rui-range-slider[@id='loanPeriod']"));

        Assert.IsTrue(field_LoanTerm.Displayed, "Field LoanTerm is not visible");
        Assert.IsTrue(field_LoanTerm.Enabled, "Field LoanTerm is not enabled");
    }




   
    [Test]
    public void DropdownList_MortgageObject_GetAttribute_Tests()
    {
        IWebElement dropdownList_MortgageObject_Span = driver.FindElement(By.XPath("//*[@data-testid='calc-input-mortgageCreditType']"));

        string mortgageObject = dropdownList_MortgageObject_Span.Text;//заберет или нет

        Assert.AreEqual(mortgageObject, "Квартира в строящемся доме", "Mortgage Object value is incorrect");
    }

    [Test]
    public void Field_LoanTerm_GetAttribute_Tests()
    {

        IWebElement field_LoanTerm = driver.FindElement(By.XPath("//*[@data-testid = 'calc-input-loanPeriod']/descendant::input"));

        int value = Int32.Parse(field_LoanTerm.GetAttribute("value"));//заберет или нет

        Assert.AreEqual(value, 30, "LoanTerm value is incorrect");
    }

    [Test]
    public void IsSwitcher_LifeInsurance_On()
    {
        IWebElement switcher_LifeInsurance = driver.FindElement(By.XPath("//*[contains(text(), 'Страхование жизни')]/ancestor::psb-switcher"));
        bool isSwitcherLifeInsuranceOn = switcher_LifeInsurance.GetAttribute("class").Contains("_checked");
        //bool isSwitcherLifeInsuranceOn = switcher_LifeInsurance.Selected;

        Assert.IsTrue(isSwitcherLifeInsuranceOn, "Life Insurance Switcher is not turned on");
    }

    [Test]
    public void IsCard_FamilyMortgage_Selected()
    {
        IWebElement card_FamilyMortgage = driver.FindElement(By.XPath("//*[contains(text(), 'Семейная ипотека')]/.."));
        bool isCardFamilyMortgageSelected = card_FamilyMortgage.GetAttribute("class").Contains("_active");

        Assert.IsFalse(isCardFamilyMortgageSelected, "Family Mortgage Card is selected");
        
    }





    //Задание 3. Стили элементов

    [Test]
    public void IsButton_FillThroughGovermentServices_Clickable_Test()
    {
        IWebElement button_FillThroughGovermentServices = driver.FindElement(By.XPath("//button[@appearance='primary' and @icon='gosuslugi']"));

        Assert.DoesNotThrow(() => button_FillThroughGovermentServices.Click(), "Button Fill Through Government Services is not clickable");
    }

    [Test]
    public void Button_FillThroughGovermentServices_Color_Test()
    {
        IWebElement button_FillThroughGovermentServices = driver.FindElement(By.XPath("//button[@appearance='primary' and @icon='gosuslugi']/rui-wrapper"));

        string buttonColor = button_FillThroughGovermentServices.GetCssValue("background-color");
        string expectedColor = "#F26126";

        Assert.AreEqual(expectedColor, ConvertRgbToHex(buttonColor), $"Button 'Fill Through Government Services' color is not {expectedColor}");
    }

    private string ConvertRgbToHex(string rgb)
    {
        var parts = rgb.Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Split(',');
        int r = int.Parse(parts[0].Trim());
        int g = int.Parse(parts[1].Trim());
        int b = int.Parse(parts[2].Trim());

        return $"#{r:X2}{g:X2}{b:X2}";
    }

    [Test]
    public void Button_FillThroughGovermentServices_Height_Test()
    {
        IWebElement button_FillThroughGovermentServices = driver.FindElement(By.XPath("//*[@id=\"mortgage-calculator\"]/div[2]/rtl-mortgage-calculator-output/div/div[1]/div[2]/div[6]/button[1]/rui-wrapper/span"));

        string buttonHeight = button_FillThroughGovermentServices.GetCssValue("height");
        string expectedHeight = "48px";

        Assert.AreEqual(expectedHeight, buttonHeight, $"Button 'Fill Through Government Services' height is not {expectedHeight}");
    }




    //Задание 4. Ожидания элементов. Часть 1

    [Test]
    public void ClickOn_ButtonFillWithouthGovermentServices_WindowErrorMessage_Test()
    {

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/military-family-mortgage-program");

        IWebElement buttonFillWithouthGovermentServices = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonFillWithouthGovermentServices.Click();

        IWebElement windowErrorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'mortgage-calculator-output__alert_show')]")));
        //IWebElement windowErrorMessage = wait.Until(driver => driver.FindElement(By.XPath("//*[contains(text(), 'Оформление заявки станет доступным после заполнения обязательных полей')]"))); 

        string expectedErrorMessage = "Оформление заявки станет доступным после заполнения обязательных полей";

        Assert.That(expectedErrorMessage, Is.EqualTo(windowErrorMessage.Text), "Incorrect window error message");
        
    }

    [Test]
    public void ClickOn_ButtonFillWithouthGovermentServices_NoButton_Test()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/military-family-mortgage-program");

        IWebElement buttonFillWithouthGovermentServices = wait.Until(driver =>
                                     driver.FindElement(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonFillWithouthGovermentServices.Click();

        bool buttonIsGone = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));


        Assert.IsTrue(buttonIsGone, "Button FillWithouthGovermentServices is still displayed");

    }

    [Test]
    public void ButtonIsVisibleAfterFiveSeconds_Test()
    {
        bool buttonIsVisibleAfterFiveSeconds = false;
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); 

        driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/military-family-mortgage-program");

        IWebElement buttonFillWithouthGovermentServices = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonFillWithouthGovermentServices.Click();

        IWebElement windowErrorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'mortgage-calculator-output__alert_show')]")));

        IWebElement button = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonIsVisibleAfterFiveSeconds = true;

        Assert.IsTrue(buttonIsVisibleAfterFiveSeconds, "Button is not visible after five seconds");

    }

    [Test]
    public void WindowErrorMessageDisappearedAfter5seconds_Test()
    {
        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        driver.Navigate().GoToUrl("https://ib.psbank.ru/store/products/military-family-mortgage-program");

        IWebElement buttonFillWithouthGovermentServices = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonFillWithouthGovermentServices.Click();

        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'mortgage-calculator-output__alert_show')]")));
       

        bool windowErrorMessageIsNotVisibleAfterFiveSeconds = wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[contains(@class, 'mortgage-calculator-output__alert_show')]")));

        Assert.IsTrue(windowErrorMessageIsNotVisibleAfterFiveSeconds, "WindowErrorMessage is visible after five seconds");

    }



    // Задание 5. Ожидания элементов.Часть 2

    [Test]
    public void AllSwitchersAreSwitched0ffAndContainMain_Test()
    {

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        IWebElement lifeInsuranceSwitcher = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Страхование жизни')]/ancestor::psb-switcher")));
        List<IWebElement> switchers = new List<IWebElement>();

       
        switchers.Add(lifeInsuranceSwitcher);

        IWebElement platinumSegmentPartner = driver.FindElement(By.XPath("//*[contains(text(), 'Покупка у')]/ancestor::psb-switcher"));
        switchers.Add(platinumSegmentPartner);

        IWebElement defenseIndustryWorker = driver.FindElement(By.XPath("//*[contains(text(), 'Работник ОПК или клиенты премиального пакета Orange Premium Club')]/ancestor::psb-switcher"));
        switchers.Add(defenseIndustryWorker);

        IWebElement receiveSalaryOnpsbCard = driver.FindElement(By.XPath("//*[contains(text(), 'Получаю зарплату на карту ПСБ')]/ancestor::psb-switcher"));
        switchers.Add(receiveSalaryOnpsbCard);


        foreach (var sw in switchers)
        {
            if (sw.GetAttribute("class").Contains("_checked"))
            {
                sw.FindElement(By.XPath("./label")).Click();

                wait.Until(d => !sw.GetAttribute("class").Contains("_checked"));
            }
        }

        Assert.Multiple(() =>
        {
            foreach (var sw in switchers)
            {
                Assert.IsFalse(sw.GetAttribute("class").Contains("_checked"), $"Switcher {sw} selected");
            }
        });

        List<IWebElement> percentsForSwitchers = new List<IWebElement>();

        IWebElement percentsLifeInsuranceSwitcher = driver.FindElement(By.XPath("//*[contains(text(), '-2%')]"));
        percentsForSwitchers.Add(percentsLifeInsuranceSwitcher);

        IWebElement percentsPlatinumSegmentPartner = driver.FindElement(By.XPath("//*[contains(text(), '-0.5%')]"));
        percentsForSwitchers.Add(percentsLifeInsuranceSwitcher);

        var percentsDefenseIndustryWorkers = driver.FindElements(By.XPath("//*[contains(text(), '-0.6%')]"));
        percentsForSwitchers.AddRange(percentsDefenseIndustryWorkers);

        Assert.Multiple(() =>
        {
            foreach (var pfs in percentsForSwitchers)
            {
                Assert.IsTrue(pfs.GetAttribute("class").Contains("main"), $"{pfs} dont contain Main"); //Split()??
            }
        });
    }
        


    // Задание 6.Действия с элементами

    [Test]
    public void ContinueButtonDisabled_Test()
    {

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement buttonFillWithouthGovermentServices = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonFillWithouthGovermentServices.Click();

        IWebElement buttonContinue = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(), 'Продолжить')]/ancestor::button")));

        Assert.IsTrue(buttonContinue.GetAttribute("class").Contains("_disabled"), "Button is clickable");

    }

    [Test]
    public void ContinueButtonIsActive_Test()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        IWebElement buttonFillWithouthGovermentServices = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(text(), 'Заполнить без Госуслуг')]/ancestor::button")));

        buttonFillWithouthGovermentServices.Click();

        IWebElement inputSurname = driver.FindElement(By.XPath("//*[contains(text(), 'Фамилия')]/following::input[1]"));
        inputSurname.Click();
        inputSurname.SendKeys("Лиманов");

        IWebElement inputName = driver.FindElement(By.XPath("//*[contains(text(), 'Имя')]/following::input[1]"));
        inputName.Click();
        inputName.SendKeys("Валерий");

        IWebElement inputMiddleName = driver.FindElement(By.XPath("//*[contains(text(), 'Отчество')]/following::input[1]"));
        inputMiddleName.Click();
        inputMiddleName.SendKeys("Витальевич");

        IWebElement yourSex = driver.FindElement(By.XPath("//*[contains(text(), 'Мужской')]"));
        yourSex.Click();


        IWebElement inputDateOfBirth = driver.FindElement(By.XPath("//*[contains(text(), 'Дата рождени')]/following::input[1]"));
        inputDateOfBirth.Click();
        inputDateOfBirth.SendKeys("30 09 2001");

        IWebElement inputTelephoneNumber = driver.FindElement(By.XPath("//*[contains(text(), 'Номер мобильного телефона')]/following::input[1]"));
        inputTelephoneNumber.Click();
        inputTelephoneNumber.SendKeys("9686238325");

        IWebElement inputEmailAddress = driver.FindElement(By.XPath("//*[contains(text(), 'Адрес email')]/following::input[1]"));
        inputEmailAddress.Click();
        inputEmailAddress.SendKeys("vvlimanov@gmail.com");


        

        IWebElement inputSitizenship = driver.FindElement(By.XPath("//*[contains(text(), 'Гражданство')]/ancestor::rui-form-field-wrapper"));
        inputSitizenship.Click();
        IWebElement inputSitizenshipRussian = driver.FindElement(By.XPath("//mat-option[@data-test-id = 'select-option-0']"));
        inputSitizenshipRussian.Click();


        IWebElement personalDataAgreement = driver.FindElement(By.XPath("//rui-checkbox[@name = 'PersonalDataProcessingAgreementConcent']"));
        personalDataAgreement.Click();

        IWebElement mortgageCentreAdress = driver.FindElement(By.XPath("//*[contains(text(), 'Адрес ипотечного центра')]/ancestor::rui-form-field-wrapper"));
        mortgageCentreAdress.Click();

        IWebElement adress = driver.FindElement(By.XPath("//*[contains(text(), 'г. Москва, пл. Славянская, д. 2/5 ')]/ancestor::mat-option"));
        adress.Click();

        IWebElement officialEmployment = driver.FindElement(By.XPath("//*[contains(text(), 'Официальное трудоустройство')]/ancestor::rui-form-field-wrapper"));
        officialEmployment.Click();
        IWebElement officialEmploymentYes = driver.FindElement(By.XPath("//mat-option[@data-test-id = 'select-option-0']"));
        officialEmploymentYes.Click();

        IWebElement requestAgreement = driver.FindElement(By.XPath("//rui-checkbox[@name = 'BkiRequestAgreementConcent']"));
        requestAgreement.Click();

        IWebElement buttonContinue = driver.FindElement(By.XPath("//*[contains(text(), 'Продолжить')]/ancestor::button"));

        Assert.IsFalse(buttonContinue.GetAttribute("class").Contains("_disabled"), "Button Continue abled");

    }



    //Задание 7. Actions.Часть 1

    [Test]
    public void WithActionsButton_Test()
    {
        IWebElement buttonFillWithGovermentServices = driver.FindElement(By.XPath("//*[contains(text(), 'Заполнить через Госуслуги')]/ancestor::rui-wrapper"));

        Assert.Multiple(() =>
        {
            Assert.IsTrue(buttonFillWithGovermentServices.Enabled, "Button Fill with Goverment Services is not clickable");

            string buttonColour = buttonFillWithGovermentServices.GetCssValue("background-color");
            string expectedColour = "#F26126";
            Assert.That(ConvertRgbToHex(buttonColour), Is.EqualTo(expectedColour));
        });

        Actions actions = new Actions(driver);
        actions.MoveToElement(buttonFillWithGovermentServices).Perform();

        string buttonColourHovered = buttonFillWithGovermentServices.GetCssValue("background-color");
        string expectedColourHovered = "#D44921";
        Assert.That(ConvertRgbToHex(buttonColourHovered), Is.EqualTo(expectedColourHovered));
    }

    [Test]
    public void WithActionsSlider_Test()
    {
        IWebElement slider = driver.FindElement(By.XPath("//*[contains(text(), 'Стоимость недвижимости')]/following::rui-slider"));

        Actions moveSlider = new Actions(driver);
        moveSlider.ClickAndHold(slider).MoveByOffset(50, 0).Release().Perform();

        IWebElement inputSlider = driver.FindElement(By.XPath("//*[@data-testid = 'calc-input-amountPledge']/descendant::input[2]"));
        string sliderValue = inputSlider.GetAttribute("value");
        int sliderValueWithoutSpaces = Int32.Parse(sliderValue.Replace(" ",""));

        int originalValue = 250000000;
        TestContext.WriteLine(sliderValue);
        TestContext.WriteLine(slider.Size.Width);
        
        Assert.AreNotEqual(originalValue, sliderValueWithoutSpaces, "Slider value did not change");


    }

}
