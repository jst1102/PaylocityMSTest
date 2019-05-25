using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using PaylocityTest.Pages;
using PaylocityMSTest.Utilities;

namespace PaylocityTest.Steps
{
	[Binding]
	public sealed class LoginSteps : TechTalk.SpecFlow.Steps
	{
		#region Variables
		private readonly IWebDriver driver;
		private LoginPage LoginPage;
		#endregion Variables

		#region Constructor
		public LoginSteps(IWebDriver webDriver)
		{
			driver = webDriver;
			LoginPage = new LoginPage(driver);
		}
		#endregion Constructor

		#region Steps

		[Given(@"I login as a valid employer")]
		public void GivenILoginAsAValidEmployer()
		{
			Given("I navigate to the login page");
			Given(string.Format(@"I enter {0} and {1} on the login page", Constants.USERNAME, Constants.PASSWORD));
			Given(string.Format(@"I click the Login button on the login page"));
		}

		[Given(@"I navigate to the login page")]
		public void GivenINavigateToTheLoginPage()
		{
			driver.Navigate().GoToUrl("file:///C:/Paylocity/login.html");
		}

		[Given(@"I click the Login button on the login page")]
		[When(@"I click the Login button on the login page")]
		public void GivenIClickTheLoginButtonOnTheLoginPage()
		{
			LoginPage.ClickLoginButton();
		}

		[Given(@"I enter (.*) and (.*) on the login page")]
		[When(@"I enter (.*) and (.*) on the login page")]
		public void WhenIEnterAndOnTheLoginPage(string username, string password)
		{
			if (!username.Trim().Equals(Constants.SKIP, StringComparison.InvariantCultureIgnoreCase))
				LoginPage.TypeUsername(username);

			if(!password.Trim().Equals(Constants.SKIP, StringComparison.InvariantCultureIgnoreCase))
				LoginPage.TypePassword(password);
		}

		[Then(@"I (.*) see an error (.*)")]
		public void ThenISeeAnError(string doOrDoNot, string expectedMessage)
		{
			if (!expectedMessage.Trim().Equals(Constants.SKIP, StringComparison.InvariantCultureIgnoreCase))
			{
				string returnedErrorMessage = LoginPage.GetLoginErrorMessage();
				switch (doOrDoNot.Trim().ToUpper())
				{
					case Constants.DO:
						Assert.IsTrue(LoginPage.IsALoginErrorMessage(), "Expected an error message, but instead got nothing. Please address.");
						Assert.AreEqual(expectedMessage, returnedErrorMessage);
						
						break;
					case Constants.DO_NOT:
						Assert.IsFalse(LoginPage.IsALoginErrorMessage(), $"Did not expect an error message, but got this: {returnedErrorMessage}. Please address.");
						break;
					default:
						throw new Exception($"From the Feature file, Do / Do Not received a bad value of : {doOrDoNot}. " +
							$"Valid parameters are 'DO' or 'DO NOT'. Please check the Feature file and correct.");
				}
			}
		}

		[Then(@"I see the (.*)")]
		public void ThenISeeThe(string pageTitle)
		{
			Console.WriteLine("test");
		}
		#endregion Steps
	}//end class
}//end namespace
