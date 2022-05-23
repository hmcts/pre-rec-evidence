Feature: Admin > Manage Case
TBC
## When test is aligned, add a test to check 'avaliable recordings' is yes for the case we use in auto tests with a recording
#Cases now need to have recordings to be in view recordings....should these steps become zephyr tests?

# - Only one court in MVP - test not needed currently
# @CreateAndManageCase @CreateAndManageCaseAndSchedule @AdminManageCases @RevertCourt
# Scenario: Update Court
#   Given I update the court on a case
#   Then the court will be updated in book recordings
#   Then the court will be updated in manage recordings
#  # Then the court will be updated in view recordings 

@CreateAndManageCase @CreateAndManageCaseAndSchedule @AdminManageCases @RevertDateCase
  Scenario: Update Scheduled date
  Given I update the scheduled date on a recording
  Then the scheduled date will be updated in book recordings
  Then the scheduled date will be updated in manage recordings
#   Then the scheduled date will be updated in view recordings

  @CreateAndManageCase 
  Scenario: Delete case with no schedule and then restore it
  Given I have created a case to search for
  When I delete the case 
  Then the case is no longer visible in book recordings
  When I restore the case
  Then the case is visible in book recordings but not in schedule recordings

  @CreateAndManageCase @CreateAndManageCaseAndSchedule
  Scenario: Delete schedule 
  Given I have created a case to search for
  When I delete the schedule 
  Then the case is visible in book recordings but not in schedule recordings
  Then the schedule is no longer visible in manage recordings
#  Then the schedule is no longer visible in view recordings
#  # Bug S28-489 - unskip when resolved and add restore steps
#  When I delete the case 
#  Then the case is no longer visible in book recordings

# # add this test once the test env is working again so can use case with recording
#   # @AdminManageCases
#   # Scenario: Delete case with recording 
#   # Given I have created a case and a schedule for it
#   # When I delete the case
#   # Then the case is no longer visible in book recordings
#   # Then the case is no longer visible in schedule recordings
#   # Then the schedule is no longer visible in manage recordings
#   # Then the schedule is no longer visible in view recordings

  @CreateAndManageCase
  Scenario: Search by case reference
  Given I have created a case to search for
  Then I can search for it by case ref in manage cases

  @CreateAndManageCase
  Scenario: Search by case id
  Given I have created a case to search for
  Then I can search for it by case id in manage cases

  @CreateAndManageCase
  Scenario: Search by case court
  Given I have created a case to search for
  Then I can search for it by court in manage cases

  @CreateAndManageCase @CreateAndManageCaseAndSchedule
  Scenario: Remove case reference in case details
  Given I have created a case to search for
  Then I cannot remove the case reference in case details

  @CreateAndManageCase @CreateAndManageCaseAndSchedule
  Scenario: Remove case reference in schedule details
  Given I have created a case to search for
  Then I cannot remove the case reference in schedule details

# Add this test once we have test env to use the case with a recording in it
  # Scenario: Remove case reference in recording details
  # Given I have a case with a recording
  # Then I cannot remove the case reference in recording details
