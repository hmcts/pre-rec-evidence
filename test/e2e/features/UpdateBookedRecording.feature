Feature: Update Booked Recording

In order to update existing cases
I want to update witnesses and defendents

#Cases now need to have recordings to be in view recordings....check this using a recorded vid once we have test env

@createCase
Scenario: Search Case
Given I want to find an existing Cases and enter the case reference
Then the application will check if the case reference already exists
# Then I cannot make a case with the same name - Bug S28-491

@createCase
Scenario: Update Witness
Given I enter and save additional witnesses
Then the case will be updated in manage recordings
Then the case will be updated in schedule recordings
Then the case will be updated in admin, manage cases, recordings

@createCase
Scenario: Update defendant
Given I enter and save additional defendants
Then the case will be updated in manage recordings
Then the case will be updated in schedule recordings
Then the case will be updated in admin, manage cases, recordings

@createCase
Scenario: Update defendant and witness
Given I enter and save additional defendants and witnesses
Then the case will be updated in manage recordings
Then the case will be updated in schedule recordings
Then the case will be updated in admin, manage cases, recordings

@createCase
Scenario: Add defendant
Given I add an extra defendant
Then the defendant will be visible in book recordings
Then the defendant will be visible in schedule recordings
Then the defendant will be visible in manage recordings
Then the defendant will be visible in admin, manage cases, recordings

@createCase
Scenario: Add witness
Given I add an extra witness
Then the witness will be visible in book recordings
Then the witness will be visible in schedule recordings
Then the witness will be visible in admin, manage cases, recordings

@createCase
Scenario: Add witness and defendant
Given I add an extra witness and defendant
Then the witness and defendant will be visible in book recordings
Then the witness and defendant will be visible in schedule recordings
Then the witness and defendant will be visible in admin, manage cases, recordings
