Feature: Google
	As a test engineer
		In order to ensure the automation framework is functional
	I would like to perform a simple search at Google
Scenario: Simple Search
Given I go to http://www.google.com
And I enter Nic Cage in the search box
And I search