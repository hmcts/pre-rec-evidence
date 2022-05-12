Feature: Book Recording
 In order to create a schedule
 As a court clerk I want to create new case if case does not exist

@ScheduleCreate 
Scenario: Create Case
  Given all fields entered and click save
  Then case will be created
  
@ScheduleCreate  
Scenario: Arrange a schedule
 Given i fill required data for creating recording
 # Bug being worked on by Chris  
#  Then the recordings box is filled
 Then schedules will be created

@ScheduleCreate  
 Scenario: Scheduling recording in the past error message
 Given I select a date in the past
 Then an error message is displayed

# # Bug - will be fixed for MVP
# @ScheduleCreate  
# Scenario: Check Courts
#   Given I select a court name
#   Then I am presented only with MVP court names

# Bug S28-480 - unskip when fixed
# @ScheduleCreate @cleanUpRecordings 
# Scenario: Test microsoft quota more than 5 recordings
#  Given i fill required data for creating ten recordings
#  Then schedules will be created
#  Then i can start recordings for the ten schedules
