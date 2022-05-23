Feature: Admin > Manage recordings
TBC

# Once this page has case references, add a step to check the date changes throughout the system
 @EditingRecordingDate @RevertDate
Scenario: Editing recording date
  Given I change the date of a recording
  Then the date is changed

@EditingRecordingDate
Scenario: Editing recording date to the past
  Given I change the date of a recording to the past
  Then an error message is displayedd

# bug s28-404
# @EditingRecordingDate
#   Scenario: Clicking save without making a change
#   Given I do not make a change 
#   Then the save button will be disabled
