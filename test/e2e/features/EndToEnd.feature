Feature: End to end with CVP

Scenario: End to end test
  Given I have created a case and schedule
  Then I can get a rtmps link from manage recordings
  Given I copy this into cvp and start a recording
  Then I can livestream the recording
 # Then i can check the stream (finish button disapears in test at the moment, comment back in and write step/page when fixed)
  Given i end the recording in cvp and finish in pre
  Then the recording is moved into view recordings
  Then I can view the recording in view recordings
  Given I share this recording with an external user
  Then the user can view the recording in the portal
  Given I un-share this recording with the external user
  Then the user can no longer view the recording in the portal