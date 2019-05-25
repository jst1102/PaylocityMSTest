using OpenQA.Selenium;
using System.Collections.Generic;
using PaylocityMSTest.Utilities;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PaylocityMSTest.Pages
{
	class BenefitsDashboardPage
	{
		#region Variables
		private readonly IWebDriver driver;

		class Benefits
		{
			public string ID { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Salary { get; set; }
			public string Dependents { get; set; }
			public string GrossPay { get; set; }
			public string BenefitCost { get; set; }
			public string NetPay { get; set; }
		}
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

		private IWebElement DeleteEmployeeButtonLocator => driver.FindElement(By.Id("btnDelete"));

		private IWebElement EditEmployeeButtonLocator => driver.FindElement(By.Id("btnEdit"));

		private IWebElement GeneralEditEmployeeButtonLocator(string name) => driver.FindElement(By.XPath("//table[@id='employee-table']//td[contains(text(),'" + name + "')]/following-sibling::td//span[contains(@id,'btnEdit')]"));

		private IList<IWebElement> EmployeeBenefitsLocator => driver.FindElements(By.TagName("tr"));
		
		#endregion Locators

		#region Methods

		public void ClickAddEmployeeButton()
		{
			AddEmployeeButtonLocator.Click();
		}

		private void TypeEmployeeFirstName(string firstName)
		{
			AddEmployeeFirstNameFieldLocator.TypeText(firstName);
		}

		private void TypeEmployeeLastName(string lastName)
		{
			AddEmployeeLastNameFieldLocator.TypeText(lastName);
		}
		
		private void TypeEmployeeDependents(string dependents)
		{
			AddDependentNumberLocator.TypeText(dependents);
		}

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

		public void ClickEmployeeSubmitButton()
		{
			AddEmployeeSubmitButtonLocator.Click();
		}

		public void ClickEmployeeCloseButton()
		{
			AddEmployeeCloseButtonLocator.Click();
		}

		public bool ReturnEmployeeName(string firstName, string lastName)
		{

			//driver.Navigate().Refresh();
			return EmployeeBenefitsLocator.Any(e => e.Text.Contains(firstName + " " + lastName));
		}

		public void ClickEmployeeEditButton(string name)
		{
			GeneralEditEmployeeButtonLocator(name).Click();
		}

		public string ReturnEmployeeInfo(string firstName, string lastName)
		{
			Benefits employeeBenefits = new Benefits();
			string info = string.Empty;
			//List<String> bene = new List<string>();
			

			foreach (var e in EmployeeBenefitsLocator)
			{
				if (e.Text.Contains(firstName) && e.Text.Contains(lastName) )
				{
					Console.WriteLine(e.Text + " is the benefit info in a string");
					info = e.Text;
					break;
					//bene = info.Split(new Char[] { ' ' });
					//bene.ToList().ForEach(i => Console.WriteLine(i.ToString()));
					//bene = info.Split(' ').ToList();
				}
			}
			return info;
		}

		public string TableToString(Table table)
		{
			string tabled = table.ToString().Replace("|", string.Empty);
			tabled = tabled.Substring(tabled.IndexOf('\n'));
			tabled = Regex.Replace(tabled, @"\s+", " ");
			tabled = tabled.Trim();
			Debug.WriteLine(tabled + " is the new string");
			return tabled;
		}

		public void EditEmployee(string name)
		{
			ClickEmployeeEditButton(name);

		}


		#endregion Methods

	}
}
