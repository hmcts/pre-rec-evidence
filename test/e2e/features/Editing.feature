Feature: Editing

@goToViewRecordings 
Scenario: Get editing details
  Given I have a recorded video
  When I click on the edit icon
  Then I can view the editing details  