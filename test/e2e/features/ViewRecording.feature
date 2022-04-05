Feature: View Recording
 In order to view a recording
 As a judge I want to view recordings before trials

Scenario: View Recording
  Given I am on the view recording page
  When I search for a case reference
  Then the recordings for that case reference will show

  Scenario: View Recording without timestamp
  Given I am watching a recording
  When  I turn off the timestamp
  Then the video will no longer show a timestamp
  
  Scenario: View Recording with a timestamp
  Given I am watching a recording
  When  I turn on the timestamp
  Then the video will show a timestamp with hours, mins and seconds