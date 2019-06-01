using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using PaylocityMSTest.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaylocityMSTest.Utilities;

namespace PaylocityMSTest.Steps
{
	[Binding]
	public sealed class BenefitsDashboardSteps : TechTalk.SpecFlow.Steps
	{
		#region Variables
		private readonly IWebDriver driver;
		private readonly BenefitsDashboardPage Benefits;
		#endregion Variables

		#region Constructor

		public BenefitsDashboardSteps(IWebDriver webDriver)
		{
			driver = webDriver;
			Benefits = new BenefitsDashboardPage(driver);
		}

		#endregion Constructor

		#region Steps

		[Given(@"I land on the (.*)")]
		[Then(@"I land on the (.*)")]
		public void GivenILandOnThe(string pageTitle)
		{
			Assert.IsTrue(driver.TitleWithWait(pageTitle));
		}


		[Given(@"I click the Add Employee button on the Benefits Dashboard page")]
		public void GivenIClickTheAddEmployeeButtonOnTheBenefitsDashboardPage()
		{
			Benefits.ClickAddEmployeeButton();
		}


		[When(@"I enter the following information on the Add Employee & His dependents pop up")]
		public void WhenIEnterTheAddEmployeeHisDependentsPopUp(Table table)
		{
			Benefits.TypeEmployeeInfo(table);
		}

		[When(@"I (.*) the following information on the Add Employee & His dependents pop up")]
		public void WhenITheFollowingInformationOnTheAddEmployeeHisDependentsPopUp(string buttonClick, Table table)
		{
			Benefits.TypeEmployeeInfo(table);
			switch (buttonClick.Trim().ToUpper())
			{
				case Constants.SUBMIT:
				case Constants.UPDATE:
					buttonClick = Constants.SUBMIT;
					break;
				case Constants.CLOSE:
					buttonClick = Constants.CLOSE;
					break;
				default:
					throw new Exception($"From the Feature file, 'Update' received a bad value of : {buttonClick}. " +
						$"Valid parameters are 'UPDATE', 'SUBMIT' or 'CLOSE'. Please check the Feature file and correct.");
			}
			When(string.Format(@"I click the {0} button on the Add Employee & His dependents pop up", buttonClick));
		}

		[When(@"I click the (.*) button on the Add Employee & His dependents pop up")]
		public void WhenIClickTheSubmitButtonOnTheAddEmployeeHisDependentsPopUp(string buttonClick)
		{
			switch (buttonClick.Trim().ToUpper())
			{
				case Constants.SUBMIT:
					Benefits.ClickEmployeeSubmitButton();
					break;
				case Constants.CLOSE:
					Benefits.ClickEmployeeCloseButton();
					break;
				default:
					throw new Exception($"From the Feature file, 'Submit' received a bad value of : {buttonClick}. " +
						$"Valid parameters are 'SUBMIT' or 'CLOSE'. Please check the Feature file and correct.");
			}
		}

		[Then(@"I should see the employee (.*) (.*) in the table")]
		public void ThenIShouldSeeTheEmployeeInTheTable(string firstName, string lastName)
		{
			
			Assert.IsTrue(Benefits.ReturnEmployeeName(firstName, lastName), $"The employee {firstName} {lastName} was not found.");
		}
				
		[Then(@"the benefit cost calculations are correct for (.*) (.*)")]
		public void ThenTheBenefitCostCalculationsAreCorrectFor(string firstName, string lastName, Table table)
		{
			Assert.AreEqual(Benefits.ReturnEmployeeInfo(firstName, lastName), Benefits.TableToString(table));
		}


		[Given(@"I click the edit button for (.*)")]
		public void GivenIClickTheEditButtonFor(string name)
		{
			Benefits.ClickEmployeeEditButton(name);
		}

		#endregion Steps
	}
}
