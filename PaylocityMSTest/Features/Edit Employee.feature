Feature: Edit Employee
	As a test engineer
		In order to validate the edit employee function
			of the benefits dashboard page
	I would like to modify the last name, first name, and dependents information
		Ensuring the dashboard page updates accordingly

Scenario: Edit Last Name
Given I login as a valid employer
	And I land on the Benefits Dashboard
	And I click the edit button for Zack
When I update the following information on the Add Employee & His dependents pop up
	| FIELD      | VALUE  |
	| FirstName  | Bobber |
Then I should see the employee Bobber Siler in the table
	And the benefit cost calculations are correct for Bobber Siler
		| ID | FirstName | LastName | Salary   | Dependents | GrossPay | BenefitCost | NetPay  |
		| 1  | Bobber    | Siler    | 52000.00 | 1          | 2000     | 57.69       | 1942.31 |