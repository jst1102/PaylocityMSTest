using OpenQA.Selenium;
using System.Collections.Generic;
using PaylocityMSTest.Utilities;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using System.Text.RegularExpressions;

namespace PaylocityMSTest.Pages
{
	class BenefitsDashboardPage
	{
		#region Variables
		private readonly IWebDriver driver;

		#endregion Variables

		#region Constructor
		public BenefitsDashboardPage(IWebDriver webDriver)
		{
			driver = webDriver;
		}
		#endregion Constructor

		#region Locators

		private IWebElement AddEmployeeButtonLocator => driver.FindElement(By.Id("btnAddEmployee"));

		private IWebElement AddEmployeeFirstNameFieldLocator => driver.FindElements(By.ClassName("form-control"))[0];

		private IWebElement AddEmployeeLastNameFieldLocator => driver.FindElements(By.ClassName("form-control"))[1];

		private IWebElement AddDependentNumberLocator => driver.FindElements(By.ClassName("form-control"))[2];

		private IWebElement AddEmployeeSubmitButtonLocator => driver.FindElement(By.XPath("//button[@type='button' and contains(., 'Submit')]"));

		private IWebElement AddEmployeeCloseButtonLocator => driver.FindElement(By.XPath("//button[@type='button' and contains(., 'Close')]"));

		private IWebElement GeneralEditEmployeeButtonLocator(string name) => driver.FindElement(By.XPath("//table[@id='employee-table']//td[contains(text(),'" + name + "')]/following-sibling::td//span[contains(@id,'btnEdit')]"));

		private IList<IWebElement> EmployeeBenefitsLocator => driver.FindElements(By.TagName("tr"));
		
		#endregion Locators

		#region Methods

		/// <summary>
		/// Method clicks the Add Employee button
		/// </summary>
		public void ClickAddEmployeeButton()
		{
			AddEmployeeButtonLocator.Click();
		}

		/// <summary>
		/// Method takes the parameter and inputs it into the first name field of the Add Employee pop-up
		/// </summary>
		/// <param name="firstName"></param>
		private void TypeEmployeeFirstName(string firstName)
		{
			AddEmployeeFirstNameFieldLocator.TypeText(firstName);
		}

		/// <summary>
		/// Method takes the parameter and inputs it into the last name field of the Add Employee pop-up
		/// </summary>
		/// <param name="lastName"></param>
		private void TypeEmployeeLastName(string lastName)
		{
			AddEmployeeLastNameFieldLocator.TypeText(lastName);
		}

		/// <summary>
		/// Method takes the parameter and inputs it into the dependents field of the Add Employee pop-up
		/// </summary>
		/// <param name="dependents"></param>
		private void TypeEmployeeDependents(string dependents)
		{
			AddDependentNumberLocator.TypeText(dependents);
		}

		/// <summary>
		/// Method takes the parameter of the table
		/// loops through all values
		/// skips over any values equal to "skip"
		/// inputs the table values into the correct pop-up field by utilizing a switch statement
		/// throws an error if a table value has a different field than what is available
		/// </summary>
		/// <param name="table"></param>
		public void TypeEmployeeInfo(Table table)
		{
			
			foreach (var tableRow in table.Rows.Where(tableRow => !tableRow[Constants.VALUE].Equals(Constants.SKIP, StringComparison.InvariantCultureIgnoreCase)))
				{
				var field = tableRow[Constants.FIELD].ToUpper();
				var value = tableRow[Constants.VALUE];

				switch(field)
				{
					case Constants.FIRSTNAME:
						TypeEmployeeFirstName(value);
						break;
					case Constants.LASTNAME:
						TypeEmployeeLastName(value);
						break;
					case Constants.DEPENDENTS:
						TypeEmployeeDependents(value);
						break;
					default:
						throw new Exception($"From the Feature file, the Field column received a bad value of : {field}. " +
											$"Valid parameters are 'FirstName', 'LastName', or 'Dependents'. Please check the Feature file and correct.");
				}
			}
		}

		/// <summary>
		/// Method clicks the submit button in the Add Employee pop-up
		/// </summary>
		public void ClickEmployeeSubmitButton()
		{
			AddEmployeeSubmitButtonLocator.Click();
		}

		/// <summary>
		/// Method clicks the close button in the Add Employee pop-up
		/// </summary>
		public void ClickEmployeeCloseButton()
		{
			AddEmployeeCloseButtonLocator.Click();
		}

		/// <summary>
		/// Method refreshes the page
		/// Then looks in the webElement list that contains the table data 
		///		for the parameters first name and last name
		/// </summary>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <returns></returns>
		public bool ReturnEmployeeName(string firstName, string lastName)
		{

			driver.Navigate().Refresh();
			return EmployeeBenefitsLocator.Any(e => e.Text.Contains(firstName + " " + lastName));
		}

		/// <summary>
		/// Method clicks the edit button using the parameter name
		/// </summary>
		/// <param name="name"></param>
		public void ClickEmployeeEditButton(string name)
		{
			GeneralEditEmployeeButtonLocator(name).Click();
		}

		/// <summary>
		/// Method goes through the list of webElements containing the data table
		/// Then returns the full row of data as a string
		/// Based on finding the element that contains the parameters first and last name
		/// </summary>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <returns></returns>
		public string ReturnEmployeeInfo(string firstName, string lastName)
		{
			string info = string.Empty;

			foreach (var e in EmployeeBenefitsLocator)
			{
				if (e.Text.Contains(firstName) && e.Text.Contains(lastName) )
				{
					info = e.Text;
					break;
				}
			}
			return info;
		}

		/// <summary>
		/// Method takes the parameter of the table
		/// Then manipulates it to:
		///		put in a string
		///		remove the extra spaces
		///		remove the |
		///		remove the table headers
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public string TableToString(Table table)
		{
			string tabled = table.ToString().Replace("|", string.Empty);
			tabled = tabled.Substring(tabled.IndexOf('\n'));
			tabled = Regex.Replace(tabled, @"\s+", " ");
			tabled = tabled.Trim();
			return tabled;
		}

		#endregion Methods
	}
}
