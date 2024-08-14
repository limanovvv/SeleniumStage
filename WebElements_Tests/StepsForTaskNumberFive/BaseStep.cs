using System;
using OpenQA.Selenium;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests.Steps
{
    [Binding]
    public class BaseStep
	{
        protected IWebDriver driver;

        public BaseStep(SeleniumBuilder builder)
        {
            this.driver = builder.WebDriver;
        }
    }
}

