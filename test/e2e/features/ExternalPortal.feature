Feature: External portal recordings
 In order to view recordings as an external user
 I need to be able to see recordings shared with me

# @SharedRecordingAtPortal
# S28-195 AC2 - test needs to be added once editing functionality has been set up
  # Scenario: View shared editied recording details
  # Given a recording has been edited
  # Then I can see all versions of each recording

# @SharedRecordingAtPortal
# # Bug S28-419 - unskip when resolved
# Scenario: No recordings shared
#  Given there have been no recordings shared with me
#  Then a message should be displayed stating No recordings found

# @SharedRecordingAtPortal
# # Bugs S28-420, S28-421 - unskip when resolved
# Scenario: Updating shared recording witness details 
#   Given I can see the witness names
#   Then I update these witness names
#   When I go to the portal
#   Then I can see the updated witness name 

@SharedRecordingAtPortal @unSharedRecordingAtPortal
Scenario: View shared recording id
Given I go to Manage cases
  Then I can find the recording id
  When I go to the portal
 Then I can view the recording uid

# @AddingAndRemovingParticipant 
# Scenario: Confirmation message when removing participant from share
#   Given I remove access for a participant to view a recording
# #   Then  a message is displayed to confirm I want to remove this participant - Bug S28-152, unskip when resolved
#   Then the participant is removed when I confirm



