Feature: Update Schedules
In order to update existing schedules
I want to update the date, witness, defendant and child indicator

Scenario: Update child witness indicator
Given I have selected a particular scheduled recording
When I change the Child Witness Indicator and save 
Then the schedule will show the updated Child Witness Indicator

Scenario: Update schedule date
Given I have a particular scheduled recording
When I make a change to the Date and save
Then the scheduled recording will show the updated Date

Scenario: Update Defendant
Given I have selected a scheduled recording
When I add an additional defendant and save
Then the scheduled recording will show the additional defendant

Scenario: Update witness
Given I have a scheduled recording
When I change the witness and save the updated record
Then the scheduled recording will show the new witness

Scenario: Update Court
Given I have a particular schedule
When I change the court and save 
Then the schedule will show the updated court

Scenario: Remove defendant
Given I have chosen a particular scheduled recording
When I remove a defendant and save the updated record
Then the scheduled recording will not show that defendant

Scenario: Update all fields
Given I have scheduled a recording
When I change every field and save the updated record
Then the scheduled recording will show the updated fields

Scenario: Close view with cross button
Given I have a recording scheduled
When I click Amend and then click the cross button
Then the view is closed

