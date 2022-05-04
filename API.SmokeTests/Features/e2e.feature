Feature: e2e test

Scenario: Update a existing user
	Given I created a user with Name 'Mike' and Job 'Dev'
	When I send request to update user with Name 'Michael'and Job 'Dev Lead'
	Then I validate user is updated