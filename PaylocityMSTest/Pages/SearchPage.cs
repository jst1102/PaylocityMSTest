using OpenQA.Selenium;
using PaylocityMSTest.Utilities;

namespace PaylocityMSTest.Pages
{
	public class SearchPage
	{
		private readonly IWebDriver webDriver;

		#region Constructor

		public SearchPage(IWebDriver driver)
		{
			webDriver = driver;
		}


		#endregion Constructor

		#region Locators

		private IWebElement SearchBoxLocator => webDriver.FindElement(By.XPath("//input[@name='q']"));



		#endregion Locators

		#region Methods

		public void EnterSearchText(string search)
		{
			SearchBoxLocator.Clear();
			SearchBoxLocator.SendKeys(search);
		}

		public void ClickSearchButton()
		{
			SearchBoxLocator.SendKeys(Keys.Enter);
		}

		public void GoToSite(string site)
		{
			webDriver.Navigate().GoToUrl(site);
		}

		#endregion Methods

	}
}