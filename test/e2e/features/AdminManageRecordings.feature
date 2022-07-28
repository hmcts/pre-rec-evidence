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

@EditingRecordingDate
  Scenario: Clicking save without making a change
  Given I do not make a change 
  Then the save button will be disabled

@goToAdmin
Scenario: Search for a recorded recording and ensure it is in the results
Given I have a recording
Then I can search for it in Manage Recording

@goToAdmin
Scenario: Search for a recorded recording using case ref and ensure it is in the results
Given I have a recording to search for using case ref
Then I can search for it in Manage Recording using case ref

@goToAdmin @RestoreCase
Scenario: Delete a recording
Given I delete a recording in manage recordings
Then I can see it in show deleted items
Then I cannot see it in View recordings

