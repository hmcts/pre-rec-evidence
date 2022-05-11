Feature: Admin > Manage Case
TBC

@ManageCases @RevertCourt
Scenario: Update Court
  Given I update the court on a case
  Then the court will be updated in book recordings
  Then the court will be updated in manage recordings
  Then the court will be updated in view recordings

@ManageCases @RevertDate
  Scenario: Update Scheduled date
  Given I update the scheduled date on a recording
  Then the scheduled date will be updated in book recordings
  Then the scheduled date will be updated in manage recordings
  Then the scheduled date will be updated in view recordings

# add this test once search feature is developed in manage cases
  # @ManageCases
  # Scenario: Delete case with no schedule
  # Given I have created a case
  # When I delete the case (add an assertion to check it deletes and isn't shown on page anymore, remove brackets when implementing)
  # Then the case is no longer visible in book recordings

# add this test once search feature is developed in manage cases
  # @ManageCases
  # Scenario: Delete schedule 
  # Given I have created a case and a schedule for it
  # When I delete the schedule (add an assertion to check it deletes and isn't shown on page anymore, remove brackets when implementing)
  # Then the case is visible in book recordings but not in schedule recordings
  # Then the schedule is no longer visible in manage recordings
  # Then the schedule is no longer visible in view recordings
  # When I also delete the case 
  # Then the case is no longer visible in book recordings

# add this test once search feature is developed in manage cases
  # @ManageCases
  # Scenario: Delete case with schedule 
  # Given I have created a case and a schedule for it
  # When I delete the case
  # Then the case is no longer visible in book recordings
  # Then the case is no longer visible in schedule recordings
  # Then the schedule is no longer visible in manage recordings
  # Then the schedule is no longer visible in view recordings