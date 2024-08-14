using System;
using OpenQA.Selenium;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebElements_Tests.Pages;
using WebElements_Tests.Steps;

namespace WebElements_Tests.FillFormAndVerifyEnteredData
{
    [Binding]
	public class FillFormStep : BaseStep
	{
        ApplicationData data;

        public FillFormStep(SeleniumBuilder builder) : base(builder) { }


        [When(@"заполнить форму с данными:")]
        public void WhenFillFormWithData(Table table)
        {
            
            var data = table.CreateInstance<ApplicationData>();

            

            DebitCardYourCashbackPageNew debitCardPage = new DebitCardYourCashbackPageNew(driver);
            debitCardPage.FillPageFields(data);
        }

        public ApplicationData GetApplicationData()
        {
            return data;
        }
    }
}

