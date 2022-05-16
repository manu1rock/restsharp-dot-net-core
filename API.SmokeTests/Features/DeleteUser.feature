Feature: DeleteUser
	I want to validate if user can be deleted
	after creation

@mytag
Scenario: Delete a user
	Given I created a user:
	 | Name    | Job     |
	 | Michael | Manager |
	When I send request to delete user
	Then validate user is deleted