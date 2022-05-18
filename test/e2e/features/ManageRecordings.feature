Feature: Manage recordings
TBC

@CaseAndScheduleCreate
Scenario: Check stream button is hidden when there's no stream
  Given there's no active stream
  Then I cannot see a check stream button

@CaseAndScheduleCreate
Scenario: Amend button should not allow the schedule date to be updated to the past
  Given I update the schedule date to a past date
  Then an error message should come up to say the date cannot be in the past

@CaseAndScheduleCreate
Scenario: Amend button should not allow the court to be removed
  Given I have removed the court
  Then the save button should be disabled

@CaseAndScheduleCreate
Scenario: Success message after clicking save should say recording updated
  Given I update a recording
  Then the success message says recording updated

@CaseAndScheduleCreate
Scenario: Check recording version number
  Given I've created a case
  Then I can see the version number for the recording