using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace WebElements_Tests.StepsForWindow
{
	public class NavigationStep
	{
        private readonly IWebDriver driver;

        public NavigationStep(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given("Открыть страницу '(.*)'")]
        public void OpenPage(string url)
        {
            driver.Navigate().GoToUrl(url);
        }



    }
}

