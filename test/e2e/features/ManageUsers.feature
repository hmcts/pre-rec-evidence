Feature: Manage Users
As a PRE Service Support user
I'd want to add new PRE user details
So that new users can be added the PRE

@ManageUsers  @CreateAUser @checkUser
Scenario: Duplicate email address
  Given I try to add a new user with an existing email address
  Then an error message is displayed stating the email address already exists in PRE
  Then the record is not saved
  When I update the users Email
  Then the PRE user record will be updated with the new Email

@ManageUsers
Scenario: Blank email address
  Given I try to add a new user with a blank email address
  Then the save button is disabled

@ManageUsers 
Scenario: Add user
  Given I try to add a new user 
  Then the record is saved

@ManageUsers @CreateAUser @checkUser
Scenario: Update First Name
  Given I need to update a users First Name
  Then the PRE user record will be updated with the new First Name

@ManageUsers 
Scenario: Update First Name
  Given I try to add a new user
  Given I need to update a users First Name
  When I make this change in PRE for the user
  Then the PRE user record will be updated with the new First Name

@ManageUsers 
Scenario: Update Last Name
Given I try to add a new user
When I update the users Last Name
Then the PRE user record will be updated with the new Last Name

@ManageUsers 
Scenario: Update Email
Given I try to add a new user
When I update the email to an existng email
Then an error message is displayed stating the email address already exists in PRE
When I update the users Email
Then the PRE user record will be updated with the new Email

@ManageUsers 
Scenario: Update Phone No
Given I try to add a new user
When I update the users Phone No
Then the PRE user record will be updated with the new Phone No

@ManageUsers 
Scenario: Update Organisation
Given I try to add a new user
When I update the Oraganisation
Then the PRE user record will be updated with the new Oraganisation

@ManageUsers 
Scenario: Update Role
Given I try to add a new user
When I update the users Role
Then the PRE user record will be updated with the new Role

@ManageUsers 
Scenario: Update every user field
Given I try to add a new user
When I update every field
Then the PRE user record will be updated 
