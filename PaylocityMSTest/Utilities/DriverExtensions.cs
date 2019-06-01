using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace PaylocityMSTest.Utilities
{
	public static class DriverExtensions
	{
		/// <summary>
		/// Extension combines the clear method and sendkeys method into one
		/// </summary>
		/// <param name="element"></param>
		/// <param name="text"></param>
		public static void TypeText(this IWebElement element, string text)
		{
			element.Clear();
			element.SendKeys(text);
		}

		/// <summary>
		/// Extension creates a default wait time that may be overridden as a parameter
		/// Then will wait until the condition of the element sent in the parameter is displayed (the parameter of title) is found
		/// If the title is found, return true
		/// If the title is not found, then catch the exception and return false
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="title"></param>
		/// <param name="seconds"></param>
		/// <returns></returns>
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
