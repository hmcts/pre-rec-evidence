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