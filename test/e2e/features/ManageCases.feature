Feature: Admin > Manage Case
TBC

Scenario: Update Court
  Given I am on manage cases
  When I update the court on a case
  Then the court will be updated in manage recordings
  Then the court will be updated in view recordings
  Then I will update the court back to the original court