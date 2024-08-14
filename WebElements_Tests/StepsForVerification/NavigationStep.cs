using System;
using OpenQA.Selenium;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;
using WebElements_Tests.Steps;

namespace WebElements_Tests.StepsForVerification
{
	public class NavigationStep : BaseStep
	{
        public NavigationStep(SeleniumBuilder builder) : base(builder) { }

        [When(@"открыть страницу по URL ""(.*)""")]
        public void OpenPageByUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}



