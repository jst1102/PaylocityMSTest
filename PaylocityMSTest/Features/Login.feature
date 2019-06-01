Feature: Login
	As a test engineer
		In order to validate the login page
	I would like to try different variations of usernames and passwords


Scenario Outline: Check Login
Given I navigate to the login page
When I enter <username> and <password> on the login page
	And I click the Login button on the login page
Then I <do/do not> see an error <error>
	And I see the <page>
Examples: 
| Description                  | username | password | do/do not | error                                    | page               |
| Login Correct                | testUser | Test1234 | do not    | Skip                                     | Benefits Dashboard |
| Login Bad User               | hulk     | Test1234 | do        | Invalid login attempt. Please try again. | Login              |
| Login Bad Password           | testUser | password | do        | Invalid login attempt. Please try again. | Login              |
| Login Nothing                | Skip     | Skip     | do        | Username and password cannot be blank.   | Login              |
| Login No User                | Skip     | Test1234 | do        | Username cannot be blank.                | Login              |
| Login Valid User No Password | testUser | Skip     | do        | Password cannot be blank.                | Login              |
| Login All Lowercase          | testuser | test1234 | do        | Invalid login attempt. Please try again. | Login              |
| Login Lowercase Username     | testuser | Test1234 | do        | Invalid login attempt. Please try again. | Login              |
| Login Lowercase Password     | testUser | test1234 | do        | Invalid login attempt. Please try again. | Login              |
| Login Bad User No Password   | test     | Skip     | do        | Password cannot be blank.                | Login              |
