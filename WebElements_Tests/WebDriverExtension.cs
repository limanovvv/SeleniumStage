using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebElements_Tests
{
	public static class WebDriverExtension
	{
		public static WebDriverWait Wait(this IWebDriver driver, int timeout)
		{
			return new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
		}

    }
}

