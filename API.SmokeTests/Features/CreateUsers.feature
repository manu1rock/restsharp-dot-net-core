Feature: Create User

@smoke
Scenario: Add a user
	Given I input name "Mike"
	And I input role "QA"
	When I send create user request
	Then validate user is created