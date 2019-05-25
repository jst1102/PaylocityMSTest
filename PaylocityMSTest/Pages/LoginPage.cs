using OpenQA.Selenium;

namespace PaylocityTest.Pages
{
	class LoginPage
	{
		#region Variables
		private readonly IWebDriver driver;
		#endregion Variables

		#region Constructor
		public LoginPage(IWebDriver webDriver)
		{
			driver = webDriver;
		}
		#endregion Constructor

		#region Locators
		private IWebElement UsernameLocator => driver.FindElement(By.CssSelector("input.form-username.form-control"));

		private IWebElement PasswordLocator => driver.FindElement(By.CssSelector("input.form-password.form-control"));

		private IWebElement LoginButtonLocator => driver.FindElement(By.Id("btnLogin"));

		private IWebElement LoginValidationErrorMessageLocator => driver.FindElement(By.Id("validation-errors"));

		#endregion Locators

		#region Methods

		public void TypeUsername(string username)
		{
			UsernameLocator.Clear();
			UsernameLocator.SendKeys(username);
		}

		public void TypePassword(string password)
		{
			PasswordLocator.Clear();
			PasswordLocator.SendKeys(password);
		}

		public string GetLoginErrorMessage()
		{
			return LoginValidationErrorMessageLocator.Text;
		}

		public bool IsALoginErrorMessage()
		{
			bool textFound = (LoginValidationErrorMessageLocator.Text != null) ? true : false;
			return textFound;
		}

		public void ClickLoginButton()
		{
			LoginButtonLocator.Click();
		}
		#endregion Methods
	}
}
