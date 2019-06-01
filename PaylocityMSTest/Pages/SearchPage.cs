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

		/// <summary>
		/// Method takes the parameter search
		/// Then inputs it into the search box
		/// </summary>
		/// <param name="search"></param>
		public void EnterSearchText(string search)
		{
			SearchBoxLocator.TypeText(search);
		}

		/// <summary>
		/// Method doesn't actually click but presses the enter key which accomplishes the same thing
		/// </summary>
		public void ClickSearchButton()
		{
			SearchBoxLocator.SendKeys(Keys.Enter);
		}

		/// <summary>
		/// Method navigates to the desired website using the parameter
		/// </summary>
		/// <param name="site"></param>
		public void GoToSite(string site)
		{
			webDriver.Navigate().GoToUrl(site);
		}

		#endregion Methods

	}
}