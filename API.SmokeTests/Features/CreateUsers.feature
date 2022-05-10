Feature: Create User

@smoke
Scenario: Add a user
	Given I input name "Mike"
	And I input role "QA"
	When I send create user request
	Then validate user is created

Scenario: Delete a user
	Given I created a user
	When I send request to delete user
	Then validate user is deleted