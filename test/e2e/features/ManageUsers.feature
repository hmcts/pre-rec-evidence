Feature: Manage Users
As a PRE Service Support user
I'd want to add new PRE user details
So that new users can be added the PRE

Scenario: Add user
  Given I am on the manage users page
  When I try to add a new user 
  Then the record is saved

Scenario: Duplicate email address
  Given I am on the manage users page
  When I try to add a new user with an existing email address
  Then an error message is displayed stating the email address already exists in PRE
  Then the record is not saved