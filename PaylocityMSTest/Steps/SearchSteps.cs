using OpenQA.Selenium;
using PaylocityMSTest.Pages;
using PaylocityMSTest.Utilities;
using TechTalk.SpecFlow;

namespace PaylocityMSTest.Steps
{
	[Binding]
	public sealed class Steps : TechTalk.SpecFlow.Steps
	{

		#region Variables

		private readonly IWebDriver driver;
		//private SeleniumContext seleniumContext;

		#endregion Variables

		#region Properties
		private SearchPage SearchPage { get; }
		#endregion Properties

		#region Constructor

		//private IWebDriver driver;
		public Steps(IWebDriver webDriver)
		{
			driver = webDriver;
			SearchPage = new SearchPage(driver);
		}

		#endregion Constructor

		#region Steps

		[Given(@"I go to (.*)")]
		public void GivenIGoTo(string site)
		{
			SearchPage.GoToSite(site);
		}

		[Given(@"I enter (.*) in the search box")]
		public void GivenIEnterInTheSearchBox(string searchText)
		{
			SearchPage.EnterSearchText(searchText);
		}

		[Given(@"I search")]
		public void GivenISearch()
		{
			SearchPage.ClickSearchButton();
		}


		#endregion Steps
	}//end class
}//end namespace