Feature: Book Recording
 In order to create a schedule
 As a court clerk I want to create new case if case does not exist

# @ScheduleCreate 
# Scenario: Create Case
#   Given all fields entered and click save
#   Then case will be created
  
# @ScheduleCreate 
# Scenario: Create schedule
#  Given i fill required data for creating recording
#  Then the recordings box is filled
#  Then schedules will be created

# @ScheduleCreate 
#  Scenario: Scheduling recording in the past error message
#  Given I select a date in the past
#  Then an error message is displayed

# # # # Bug - will be fixed for MVP
# # @ScheduleCreate  
# # Scenario: Check Courts
# #   Given I select a court name
# #   Then I am presented only with MVP court names

# Bug S28-480 - unskip when fixed
@ScheduleCreate @cleanUpRecordings 
Scenario: Test microsoft quota more than 5 recordings
 Given i fill required data for creating ten recordings
 Then schedules will be created
 Then i can start recordings for the ten schedules

# # # Bug S28-552
# # @ScheduleCreate 
# # Scenario: Create case with all blank values
# #   Given I create a case with blank values
# #   # Then an error message is displayed about the blank values - Bug S28-240

# # # Bug S28-552
# # @ScheduleCreate 
# # Scenario: Create case with court blank value
# #   Given I create a case with blank values in court
# # #   Then an error message is displayed about the blank values - Bug S28-240

# # # Bug S28-552
# # @ScheduleCreate 
# # Scenario: Create case with case ref blank value
# #   Given I create a case with blank values in case ref
# # #   Then an error message is displayed about the blank values - Bug S28-240

# # # Bug S28-552
# # @ScheduleCreate 
# # Scenario: Create case with witness blank values
# #   Given I create a case with blank values in witnesses
# # #   Then an error message is displayed about the blank values - Bug S28-240

# # # Bug S28-552
# # @ScheduleCreate 
# # Scenario: Create case with defendant blank values
# #   Given I create a case with blank values in defendants
# #   # Then an error message is displayed about the blank values - Bug S28-240

# @ScheduleCreate 
# Scenario: Update case with blank values
#   Given all fields entered and click save
#   Then case will be created
#   When I update the case with blank values
# #   Then an error message is displayed about the blank values - Bug S28-240

# @ScheduleCreate 
# Scenario: Create case with blank values in list
#   Given I create a case with blank values in a list
#   # Then the case is created but the blank values are ignored - Bug S28-240

# # Bug S28-240
# # @ScheduleCreate 
# # Scenario: Update case with blank values in list
# #   Given all fields entered and click save
# #   Then case will be created
# #   When I update a case with blank values they cannot be saved

# @ScheduleCreate 
# Scenario: Create case with Duplicate case ref
# Given I create a case with a duplicate case ref
# Then an error message is displayed stating the case exists

