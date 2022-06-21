Feature: External portal recordings
 In order to view recordings as an external user
 I need to be able to see recordings shared with me

# @SharedRecordingAtPortal
# S28-195 AC2 - test needs to be added once editing functionality has been set up
  # Scenario: View shared editied recording details
  # Given a recording has been edited
  # Then I can see all versions of each recording

@SharedRecordingAtPortal
Scenario: No recordings shared
 Given there have been no recordings shared with me
 Then a message should be displayed stating No recordings found

@SharedRecordingAtPortal @unSharedRecordingAtPortal
  Scenario: View shared recording id
  Given I go to Manage cases
  Then I can find the recording id
  When I go to the portal
  Then I can view the recording uid

# Bug s28-179
@unlockAccount
Scenario: Invalid password
  Given I enter the wrong log-in details
  Then I see an error message
  When I do this five times
  Then my account is locked
 # Then I can see a link to reset my password

# Bug s28-179
# @unlockAccount - add back in when uncommenting locking lines
Scenario: Invalid 2FA Code
  Given I enter the wrong 2FA code
  Then I see an error message
  # When I do this five times
  # Then my account is locked
 # Then I can see a link to reset my password
