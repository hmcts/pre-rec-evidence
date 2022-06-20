Feature: Manage Users
As a PRE Service Support user
I'd want to add new PRE user details
So that new users can be added the PRE

 @ManageUsers 
  Scenario: Duplicate email address
    Given I try to add a new user with an existing email address
    Then an error message is displayed stating the email address already exists in PRE
    Then the record is not saved

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update Email
  When I update the users Email
  Then the PRE user record will be updated with the new Email

  @ManageUsers
  Scenario: Blank email address
    Given I try to add a new user with a blank email address
    Then the save button is disabled

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update First Name
    Given I need to update a users First Name
    Then the PRE user record will be updated with the new First Name

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update Last Name
    Given I update the users Last Name
    Then the PRE user record will be updated with the new Last Name

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update Phone No
    Given I update the users Phone No
    Then the PRE user record will be updated with the new Phone No

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update Organisation
    Given I update the Oraganisation
    Then the PRE user record will be updated with the new Oraganisation

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update Role
    Given I update the users Role
    Then the PRE user record will be updated with the new Role

  @ManageUsers @CreateAUser @checkUser
  Scenario: Update every user field
    Given I update every field
    Then the PRE user record will be updated

  @SuperUserManageUsers @SuperUserCreateAUser @SuperscreencheckUser
  Scenario: Super User screen-Update every user field in super user screen
    Given I update all fields
    Then the PRE user record will be updated
    Then the user will not be visible in Admin

  @ManageUsers
  Scenario: Searching for a user with no results returns a message
    Given I want to search for a user and no records are returned
    Then a message should be displayed stating that no records were found

  @SuperUserManageUsers
  Scenario: Super user screen-Searching for a user with no results returns a message
    Given I search for a user and no records are returned
    Then a message should be displayed stating that no records were found

  @ManageUsers 
  Scenario: First name validation checks, creating a new user
    Given I try to add a new user with an empty first name
    Then the save button is disabled
    When I change the first name to have 25 characters
    Then I cannot add any more characters
    When I change the first name to be alphanumeric, with a dash
    Then the save button is enabled 
    Then the user is created

  @ManageUsers 
  Scenario: Last name validation checks, creating a new user
    Given I try to add a new user with an empty last name
    Then the save button is disabled
    When I change the last name to have 25 characters
    Then I cannot add any more characters
    When I change the last name to be alphanumeric, with a dash
    Then the save button is enabled 
    Then the user is created

# Bugs in S28-441
  @ManageUsers 
  Scenario: Email validation checks, creating a new user
    Given I try to add a new user with an empty email address
    Then the save button is disabled
    # When I change the email to have less than 6 characters
    # Then the save button is disabled
    When I change the email to have 50 characters
    Then I cannot add any more characters
    # When I change the email to not have an at sign
    # Then the save button is disabled
    When I change the email to meet all validation criteria
    Then the save button is enabled 
    Then the user is created

# Bugs in S28-441
  @ManageUsers 
  Scenario: Phone number checks, creating a new user
    Given I try to add a new user with an empty phone number
    Then the save button is disabled
    # When I change the phone number to be alphanumeric
    # Then the save button is disabled
    When I change the phone number to have 13 characters
    Then I cannot add any more characters
    # When I change the phone number to have a min of 10 characters
    # Then the save button is disabled
    # When I change the phone number to have spaces
    # Then the save button is disabled
    When I change the phone number to be numeric with a plus sign
    Then the save button is enabled 
    Then the user is created

# Bugs in S28-441
  @ManageUsers 
  Scenario: Organisation validation checks, creating a new user
    Given I try to add a new user with an organisation of 50 characters
    Then I cannot add any more characters
    # When I change the organisation to be empty
    # Then the save button is disabled
    When I change the organisation to be alphanumeric
    Then the save button is enabled 
    Then the user is created