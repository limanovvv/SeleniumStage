using System;
using OpenQA.Selenium;
using SeleniumInitialize_Builder;
using TechTalk.SpecFlow;

namespace WebElements_Tests
{
	[Binding]
	public class Hooks
	{
        private SeleniumBuilder _builder;

        public IWebDriver driver;


        [BeforeScenario]
        public void Setup()
        {

            driver = _builder.Build();

        }
        [AfterScenario]
        public void Teardown()
        {

            driver.Quit();

        }

        public Hooks(SeleniumBuilder builder)
		{
            this._builder = builder;
            

		}


	}
}

