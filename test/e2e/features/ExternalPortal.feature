Feature: External portal recordings
 In order to view recordings as an external user
 I need to be able to see recordings shared with me

Scenario: View shared recording details
  Given there have been recordings shared with me
  When I go to the portal
  Then I can view the shared recording
  Then the recording is unshared and no longer visible

# S28-195 AC2 - test needs to be added once editing functionality has been set up
  # Scenario: View shared editied recording details
  # Given there have been recordings shared with me
  # When I go to the portal
  # Then I must see all versions of each recording
  # Then the recording is unshared and no longer visible

# Bug S28-419 - unskip when resolved
# Scenario: No recordings shared
#  Given there have been no recordings shared with me
#  Then a message should be displayed stating No recordings found

# Bugs S28-420, S28-421 - unskip when resolved
# Scenario: Updating shared recording witness details 
#   Given there have been recordings shared with me
#   When I go to the portal
#   Then I can see the witness names
#   Then I update these witness names
#   When I go to the portal
#   Then I can see the updated witness name 
#   Then the recording is unshared and no longer visible
