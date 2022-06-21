Feature: Book Recording
 In order to create a schedule
 As a court clerk I want to create new case if case does not exist

@ScheduleCreate 
Scenario: Create Case
  Given all fields entered and click save
  Then case will be created
  
@ScheduleCreate 
Scenario: Create schedule
 Given i fill required data for creating recording
 Then the recordings box is filled
 Then schedules will be created

@ScheduleCreate 
 Scenario: Scheduling recording in the past error message
 Given I select a date in the past
 Then an error message is displayed
 Then the save button disabled

 @ScheduleCreate 
 Scenario: Scheduling without a witness
 Given I do not select a witness 
 Then the save button disabled

 @ScheduleCreate 
 Scenario: Scheduling without a defendant
 Given I do not select a defendant 
 Then the save button disabled

 @ScheduleCreate 
 Scenario: Scheduling without a date
 Given I do not select a date 
 Then the save button disabled

# # # Bug - will be fixed for MVP
# @ScheduleCreate  
# Scenario: Check Courts
#   Given I select a court name
#   Then I am presented only with MVP court names

# quota test, use when required
# @ScheduleCreate @cleanUpRecordings 
# Scenario: Test microsoft quota more than 5 recordings
#  Given i fill required data for creating ten recordings
#  Then schedules will be created
#  Then i can start recordings for the ten schedules

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

# # # # #validation message will be implemented post MVP
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
Scenario: Create case with blank values in list
  Given I create a case with blank values in a list
Then the case is created but the blank values are ignored 

# Need to figure out assertion
# @ScheduleCreate 
# Scenario: Update case with blank values in list
#   Given all fields entered and click save
#   Then case will be created
#   When I update a case with blank values they cannot be saved

@ScheduleCreate 
Scenario: Create case with Duplicate case ref
Given I create a case with a duplicate case ref
Then an error message is displayed stating the case exists

