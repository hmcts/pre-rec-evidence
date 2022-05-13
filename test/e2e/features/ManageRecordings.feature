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
