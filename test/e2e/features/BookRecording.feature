Feature: Book Recording
 In order to create a schedule
 As a court clerk I want to create new case if case does not exist

@ScheduleCreate 
Scenario: Create Case
  Given all fields entered and click save
  Then case will be created

#quota test- comment in when needed 
# @ScheduleCreate 
# Scenario: Create schedules
#  Given i fill required data for creating recording
#  Then the recordings box is filled
#  Then schedules will be created
#  Then i can start recordings for the ten schedules

@ScheduleCreate
Scenario:Create schedule
Given i fill required data for creating recording
Then the recordings box is filled
Given I click the open case button
Given all fields entered and click save
Then case will be created

@ScheduleCreate
Scenario:Create schedule as child witness
Given i fill required data for creating schedule as a child
Then the recordings box is filled
Then schedules will be created

@ScheduleCreate 
Scenario: Create case with all blank values
  Given I create a case with blank values
  Then an error message is displayed about the blank values

@ScheduleCreate 
Scenario: Create case with court blank value
  Given I create a case with blank values in court
  Then an error message is displayed about the blank values 

 @ScheduleCreate 
Scenario: Create case with case ref blank value
  Given I create a case with blank values in case ref
  Then an error message is displayed about the blank values 

@ScheduleCreate 
Scenario: Create case with witness blank values
  Given I create a case with blank values in witnesses
  Then an error message is displayed about the blank values 

@ScheduleCreate 
Scenario: Create case with defendant blank values
  Given I create a case with blank values in defendants
  Then an error message is displayed about the blank values 

#validation message will be implemented post MVP
@ScheduleCreate 
Scenario: Cannot Create case with more than 13 characters
  Given I try to create a case ref more than thirteen characters
  Then I am Unable to 

 @ScheduleCreate 
Scenario: Update case with blank values
  Given all fields entered and click save
  Then case will be created
  When I delete all witnesses and defendants the save button is disabled

@ScheduleCreate 
Scenario: Update case with blank values in list
  Given all fields entered and click save
  Then case will be created
  When I update a case with blank values they cannot be saved

@ScheduleCreate 
Scenario: Create case with blank values in list
  Given I create a case with blank values in a list
Then the case is created but the blank values are ignored 

# Bug in S28-269
@ScheduleCreate 
Scenario: Delete schedule
  Given I create a schedule
  Then I can search for the schedule
  Then I can delete the schedule
  # Then I see a confirmation message
  Then I can no longer search for the schedule

  Scenario: Delete schedule with recording
    Given there's a schedule with a recording
    Then I cannot delete the schedule

  @ScheduleCreate
  Scenario: Create case with Duplicate case ref
    Given I create a case with a duplicate case ref
    Then an error message is displayed stating the case exists

  Scenario: Display Terms and conditions
    Given I click the Terms and Conditions and link
    Then the terms and conditions are displayed
    When I click back
    Then it goes back to the correct page