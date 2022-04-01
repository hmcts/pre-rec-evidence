Feature: Book Recording
 In order to create a schedule
 As a court clerk I want to create new case if case does not exist

Scenario: Create Case
  Given user on Book recording screen
  When all fields entered and click save
  Then case will be created
  

# Scenario: Arrange a schedule
#   Given user on Schedule page
#   When i fill required data for creating recording
#   Then schedules will be created