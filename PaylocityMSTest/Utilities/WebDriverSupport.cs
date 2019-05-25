using System;
using BoDi;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using TechTalk.SpecFlow;

namespace PaylocityMSTest.Utilities
{
	[Binding]
	public class WebDriverSupport
	{

		#region Constants


		#endregion

		#region Variables

		private readonly IObjectContainer objectContainer;
		private IWebDriver driver;

		#endregion

		#region Properties

		private static string PreferredDriver => ConfigurationManager.AppSettings["PreferredDriver"];

		#endregion

		public WebDriverSupport(IObjectContainer objectContainer)
		{
			this.objectContainer = objectContainer;
		}

		#region Methods


		private void ConfigureChromeDriver()
		{
			var options = new ChromeOptions();
			options.AddArgument("--disable-extensions");
			options.AddArgument("--disable-infobars");
		}

		internal void SelectBrowser(Enums.BrowserType browserType)
		{
			string directory = "C:\\Users\\Computer\\source\\repos\\PaylocityMSTest\\PaylocityMSTest\\bin\\Debug\\netcoreapp2.2";

			switch (browserType)
			{

				case Enums.BrowserType.Chrome:
					var options = new ChromeOptions();
					options.AddArgument("no-sandbox");
					options.AddArgument("--disable-extensions");
					options.AddArgument("--disable-infobars");
					driver = new ChromeDriver(directory, options);
					break;
				case Enums.BrowserType.Firefox:
					driver = new FirefoxDriver(directory);
					break;
				case Enums.BrowserType.IE:
					driver = new InternetExplorerDriver(directory);
					break;
				default:
					var option = new FirefoxOptions();
					option.AddArgument("--headless");
					driver = new FirefoxDriver(directory, option);
					break;
			}

			driver.Manage().Window.Maximize();
			objectContainer.RegisterInstanceAs<IWebDriver>(driver);
		}

		[BeforeScenario]
		public void Initialize()
		{
			SelectBrowser(Enums.BrowserType.Firefox);
		}

		[AfterScenario]
		public void CleanUp()
		{
			driver.Quit();
			driver.Dispose();
		}
		#endregion
	}
}