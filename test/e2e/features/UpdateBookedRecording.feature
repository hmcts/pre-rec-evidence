Feature: Update Booked Recording
In order to update existing cases
I want to update witnesses and defendents

# Scenario: Search Case
#   Given I am on the book recordings page and I want to find an existing case
#   When I want to find an existing Cases and enter the case reference 
#   Then the application will check if the case reference already exists

Scenario: Update Case
 Given I have found an existing case
 When I enter and save additional witnesses
# Then the case will be updated with additional witnesses

