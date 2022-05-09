Feature: Admin > Manage recordings
TBC

#Once this page has case references, add a step to check the date changes throughout the system
@EditingRecordingDate
Scenario: Editing recording date
  Given I change the date of a recording
  Then the date is changed

@EditingRecordingDate
Scenario: Editing recording date to the past
  Given I change the date of a recording to the past
  Then an error message is displayedd
