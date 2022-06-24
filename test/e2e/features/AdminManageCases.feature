Feature: Admin > Manage Case
TBC

# # - Only one court in MVP - test not needed currently
# # @CreateAndManageCase @CreateAndManageCaseAndSchedule @AdminManageCases @RevertCourt
# # Scenario: Update Court
# #   Given I update the court on a case
# #   Then the court will be updated in book recordings
# #   Then the court will be updated in manage recordings

@CreateAndManageCase @CreateAndManageCaseAndSchedule @AdminManageCases @RevertDateCase
  Scenario: Update Scheduled date
  Given I update the scheduled date on a recording
  Then the scheduled date will be updated in book recordings
  Then the scheduled date will be updated in manage recordings

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
  When I delete the case 
  Then the case is no longer visible in book recordings
  When I restore the schedule and case
  # Bug S28-606
  # Then the schedule is visible in manage recordings

Scenario: Cannot delete or edit a case with recording 
  Given I have a case with a recording
  Then I cannot edit or delete the case
  Then I cannot edit or delete the schedule
  Then I cannot edit or delete the recording
  Then the witness and defendants cannot be edited

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

# Figure me out mwahahahhha
# @CreateAndManageCase @AdminManageCases 
# Scenario: Update case ref with existing case 
#   Given I update the case ref on a case
#   Then an error message stating the case already exists will be displayed