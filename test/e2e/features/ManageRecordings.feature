Feature: Manage recordings
  TBC

  @CaseAndScheduleCreate
  Scenario: Check stream button is hidden when there's no stream
    Given there's no active stream
    Then I cannot see a check stream button

  @CaseAndScheduleCreate
  Scenario: Amend button should not allow the schedule date to be updated to the past
    Given I update the schedule date to a past date
    Then an error message should come up to say the date cannot be in the past

  @CaseAndScheduleCreate
  Scenario: Amend button should not allow the court to be removed
    Given I have removed the court
    Then an error message should appear

  @CaseAndScheduleCreate
  Scenario: Success message after clicking save should say recording updated
    Given I update a recording
    Then the success message says recording updated

  @CaseAndScheduleCreate
  Scenario: Check recording version number
    Given I've created a case
    Then I can see the version number for the recording

  @CaseAndScheduleCreate
  Scenario: Success message after clicking delete should say scheduled recording deleted
    Given I delete a recording
    Then the success message says scheduled recording deleted

  @CaseAndScheduleCreate
  Scenario: Right buttons should be visible when there's no recording and when recording has started
    Given there's no recording
    Then the amend, manage and record buttons should be visible with a no recording status
    When I start the recording
    Then the manage, check, view and finish buttons should be visible with a in recording mode status

  @CaseAndScheduleCreate
  Scenario: Find recording by date
    Given I've selected today's date to find a case
    When I left all other fields empty
    Then I should be able to find the recording

  @CaseAndScheduleCreate
  Scenario: Find recording by partial case reference
    Given I've selected part of the case reference
    When I left all other fields empty
    Then I should be able to find the recording

  @CaseAndScheduleCreate
  Scenario: Find recording by court name
    Given I've selected the court name
    When I left all other fields empty
    Then I should be able to find the recording

  @CaseAndScheduleCreate
  Scenario: Find recording by date, care reference and court name
    Given I've selected date, case reference and court name
    Then I should be able to find the recording

@CaseAndScheduleCreate
Scenario: Success message after clicking delete should say scheduled recording deleted
  Given I delete a recording
  Then the success message says scheduled recording deleted
   Then the schedule is not in Manage Recordings
   Then the schedule is not in the schedule page
   Then the schedule is deleted in Admin Manage Cases
   
#  @CaseAndScheduleCreate
#  Scenario: File size, date and time shows for find recording results
#    Given I've selected date, case reference and court name
#    Then I should see file size, date and time

# @CaseAndScheduleCreate - The following
#   Scenario: find recording should return multiple versions of the recording
#     Given I've selected date, case reference and court name
#     And recording has multiple versions
#     Then I should see multiple versions of the recording
