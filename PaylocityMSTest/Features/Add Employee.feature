Feature: Add Employee
	As a test engineer
		In order to validate the add employee function
			of the benefits dashboard page
	I would like to try different variations of employee information

GIVEN an Employer
AND I am on the Benefits Dashboard page
WHEN I select Add Employee
THEN I should be able to enter employee details
AND First Name does not begin with “A” or “a”
AND the employee should save
AND I should see the employee in the table
AND the benefit cost calculations are correct

Scenario: Add Employee
Given I login as a valid employer
	And I land on the Benefits Dashboard
	And I click the Add Employee button on the Benefits Dashboard page
When I submit the following information on the Add Employee & His dependents pop up
	| FIELD      | VALUE  |
	| FirstName  | George |
	| LastName   | Stacy  |
	| Dependents | 2      |
Then I should see the employee George Stacy in the table
	And the benefit cost calculations are correct for George Stacy
		| ID | FirstName | LastName | Salary   | Dependents | GrossPay | BenefitCost | NetPay  |
		| 1  | George    | Stacy    | 52000.00 | 2          | 2000     | 76.92       | 1923.08 |
#	Examples: 
#| Description             | FirstName | LastName | Salary   | Dependents | GrossPay | BenefitCost | NetPay  |
#| Full Name, 2 Dependents | George    | Stacy    | 52000.00 | 2          | 2000     | 76.92       | 1923.08 |
