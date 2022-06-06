Feature: View Recording
 In order to view a recording
 As a judge I want to view recordings before trials

View recordings currently not working in test env
  @View 
  Scenario: View Recording
  Given I search for a case reference
  Then the recordings for that case reference will show
  Then I can see the version for the recording

  @View 
  Scenario: View Recording without timestamp
  Given I turn off the timestamp
  Then the video will no longer show a timestamp
  
  @View
  Scenario: View Recording with a timestamp
  Given  I turn on the timestamp
  Then the video will show a timestamp with hours, mins and seconds


