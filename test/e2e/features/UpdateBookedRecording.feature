Feature: Update Booked Recording
In order to update existing cases
I want to update witnesses and defendents

#Cases now need to have recordings to be in view recordings....should checking the update here become zephyr tests? -TBC

@createCase
Scenario: Search Case
  Given I want to find an existing Cases and enter the case reference 
  Then the application will check if the case reference already exists
#  Then I cannot make a case with the same name - Bug S28-491

@createCase
Scenario: Update Case
 Given I enter and save additional witnesses
 Then the case will be updated in manage recordings 
 Then the case will be updated in schedule recordings

@createCase
Scenario: Update defendant
 Given I enter and save additional defendants
 Then the case will be updated in manage recordings 
 Then the case will be updated in schedule recordings

@createCase
Scenario: Update defendant and witness
 Given I enter and save additional defendants and witnesses
 Then the case will be updated in manage recordings 
 Then the case will be updated in schedule recordings

