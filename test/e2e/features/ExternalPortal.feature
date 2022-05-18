Feature: External portal recordings
 In order to view recordings as an external user
 I need to be able to see recordings shared with me

# Needs test env
# @SharedRecordingAtPortal
# Scenario: View shared recording details and status change
#   Given I can view the shared recording and I click on it
#   Then the status changes

# @SharedRecordingAtPortal
# S28-195 AC2 - test needs to be added once editing functionality has been set up
  # Scenario: View shared editied recording details
  # Given a recording has been edited
  # Then I can see all versions of each recording

# @SharedRecordingAtPortal
# Bug S28-419 - unskip when resolved
# Scenario: No recordings shared
#  Given there have been no recordings shared with me
#  Then a message should be displayed stating No recordings found

# @SharedRecordingAtPortal
# Bugs S28-420, S28-421 - unskip when resolved
# Scenario: Updating shared recording witness details 
#   Given I can see the witness names
#   Then I update these witness names
#   When I go to the portal
#   Then I can see the updated witness name 

# #check recording uid from admin page - waiting for test to be aligned with sandbox for the search in the manage case page
# Scenario: View shared recording id
#   Given there have been recordings shared with me
#   When I go to Manage cases
#   Then I can find the recording id
#   When I go to the portal
# #  Then I can view the recording uid
#   Then the recording is unshared and no longer visible

@AddingAndRemovingParticipant
Scenario: Confirmation message when removing participant from share
  Given I remove access for a participant to view a recording
#   Then  a message is displayed to confirm I want to remove this participant - Bug S28-152, unskip when resolved
  Then the participant is removed when I confirm

# Add this test once test env is aligned
# @AddingAndRemovingParticipant
# Scenario: 
#   Given I have a participant with access to a recording
#   When I remove this access
#   Then the participant will not be able to access to the recording

