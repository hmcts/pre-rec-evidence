Feature: Update Schedules
In order to update existing schedules
I want to update the date, witness, defendant and child indicator

@UpdatingSchedule
Scenario: Update child witness indicator
Given I have selected a particular scheduled recording
When I change the Child Witness Indicator and save 
Then the schedule will show the updated Child Witness Indicator

@UpdatingSchedule
Scenario: Update schedule date
Given I have a particular scheduled recording
When I make a change to the Date and save
Then the scheduled recording will show the updated Date

@UpdatingSchedule
Scenario: Update Defendant
Given I have selected a scheduled recording
When I add an additional defendant and save
Then the scheduled recording will show the additional defendant

@UpdatingSchedule
Scenario: Update witness
Given I have a scheduled recording
When I change the witness and save the updated record
Then the scheduled recording will show the new witness

@UpdatingSchedule
Scenario: Update Court
Given I have a particular schedule
When I change the court and save 
Then the schedule will show the updated court

@UpdatingSchedule
Scenario: Remove defendant
Given I have chosen a particular scheduled recording
When I remove a defendant and save the updated record
Then the scheduled recording will not show that defendant

@UpdatingSchedule
Scenario: Update all fields
Given I have scheduled a recording
When I change every field and save the updated record
Then the scheduled recording will show the updated fields

@UpdatingSchedule
Scenario: Close view with cross button
Given I have a recording scheduled
When I click Amend and then click the cross button
Then the view is closed

