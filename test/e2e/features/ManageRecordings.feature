Feature: Manage recordings
TBC

Scenario: Check stream button is hidden when there's no stream
  Given I have created a new case and scheduled a recording
  When I go to manage recordings and search for the case
  Then I cannot see a check stream button