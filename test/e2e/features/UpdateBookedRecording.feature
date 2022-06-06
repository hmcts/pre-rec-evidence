Feature: Update Booked Recording

In order to update existing cases
I want to update witnesses and defendents

# #Cases now need to have recordings to be in view recordings....check this using a recorded vid once we have test env

@createCase
Scenario: Search Case
Given I want to find an existing Cases and enter the case reference
Then the application will check if the case reference already exists
# Then I cannot make a case with the same name - Bug S28-491
Then I can create additional schedules
Then this schedule will be visible in manage recordings

@createCase 
Scenario: Update Witnesses
Given I enter and save additional witnesses
Then the case will be updated in manage recordings
Then the case will be updated in schedule recordings
Then the case will be updated in book recordings as well
Then the case will be updated in admin, manage cases, recordings

@createCase
Scenario: Update defendant
Given I enter and save additional defendants
Then the case will be updated in manage recordings
Then the case will be updated in schedule recordings
Then the case will be updated in book recordings as well
Then the case will be updated in admin, manage cases, recordings

@createCase 
Scenario: Update defendant and witness
Given I enter and save additional defendants and witnesses
Then the case will be updated in manage recordings
Then the case will be updated in schedule recordings
Then the case will be updated in book recordings as well
Then the case will be updated in admin, manage cases, recordings

@createCase 
Scenario: Remove Witness that has not been scheduled
Given I attempt to remove the witnesses from the case
Then an error message will appear Or the delete button is disabled 
Then the case will be updated in schedule recording
Then the case will be updated in book recordings
#bug S28-534
#Then the case will be updated in manage recording
Then the case will be updated in manage cases

@createCase 
Scenario: Remove Witness that has been scheduled
Given I try to remove the witnesses from the case
Then an error message will show Or the delete button is disabled 
Then the case in schedule recording will be updated
Then the case in book recordings will be updated
Then the case in manage recording will be updated
Then the case in manage cases will be updated

@createCase 
Scenario: Remove Defendant that has been scheduled
Given I try to remove the defendant from the case
Then an error message will show up Or the delete button is disabled 
Then the case will be updated within schedule recording
Then the case will be updated within book recordings
Then the case will be updated within manage recording
Then the case will be updated within manage cases

@createCase 
Scenario: Remove all Defendants and witnesses
Given I try to remove the defendants and witnesses from the case
Then an error message will show up Or the Save button is disabled 
Then the case within book recordings will be updated
Then the case within manage recording will be updated
Then the case within manage cases will be updated

@createCase 
Scenario: Remove Witness and defendant that has been scheduled
Given I try to remove the witness and defendant from the case
Then an error message will be presented Or the delete button is disabled 
Then the case inside schedule recording will be updated
Then the case inside book recordings will be updated
Then the case inside manage recording will be updated
Then the case inside manage cases will be updated

@createCase 
Scenario: Remove Witness and defendant that has not been scheduled
Given I try to remove the non scheduled witness and defendant from the case
Then an error message appears Or the delete button is disabled 
Then the case on schedule recording will be updated
Then the case on book recordings will be updated
# #bug S28-534
# #Then the case on manage recording will be updated
Then the case on manage cases will be updated

# Commented out as assertion needs to be figured out
# @createCase
# Scenario: Remove Witness and defendants and save 
# Given I remove the text form witness and defendant fields and attempt to click save
# Then the save icon is disabled 

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


