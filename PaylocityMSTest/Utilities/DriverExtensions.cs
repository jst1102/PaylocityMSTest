using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace PaylocityMSTest.Utilities
{
	public static class DriverExtensions
	{

		public static void TypeText(this IWebElement element, string text)
		{
			element.Clear();
			element.SendKeys(text);
		}

		public static bool TitleWithWait(this IWebDriver driver, string title, int seconds = -1)
		{
			bool found = false;
			if (seconds == -1)
				seconds = 5;

			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
			wait.Until(condition =>
			{
				
				try
				{
					var elementToBeDisplayed = driver.Title;

					if(elementToBeDisplayed == title)
					found = true;
				}
				catch (Exception e) when (e is StaleElementReferenceException || e is NoSuchElementException)
				{
					found = false;
				}

				return found;
			});

			return found;
		}
	}
}
