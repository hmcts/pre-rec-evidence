# HMCTS DTS Project


## Purpose

This Repository contains the code for the DTS Shared Services Pre-recorded Evidence project.
For more details please look up the project details here: https://tools.hmcts.net/confluence/display/S28/Pre-Recorded+Evidence


## What's inside

The application has the following main components
1. MS Power Apps
2. MS Power Automate
3. MS Dataverse
4. Infrastructure build with Terraform.
5. Jenkins Build Pipelines


## Setup


## Notes

## Running Automated Tests.

For executing automated tests please follow below steps

1. Install .NET SDK https://dotnet.microsoft.com/en-us/download
2. Checkout this repository and navigate to pre-rec-evidence/test/e2e
3. For authentication please setup auth file and copy file location and update the file
    ``` test/e2e/Hooks/HooksInitializer.cs ``` with your auth file location
   //todo will update later to generate auth file
4. Build the project by running ``` dotnet build ```
5. Then for running the tests from terminal use ``` dotnet test ```

For Pa11y, you can set up the pa11y.ps1 file as a powershell config in your IDE and run the accessibility tests through there or you can run the commands within the file in your terminal
- You can also run this command to run the tests and see the results in the terminal: pa11y-ci --config .pa11yci.tests.portal.json - you'll need to cd into the pa11y directory

### Generating living Documentation.

After running above tests you can generate living documentation by using
install living doc from terminal using
```dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI ```

and execute below
``` livingdoc test-assembly bin/Debug/net6.0/pre.dll -t bin/Debug/net6.0/TestExecution.json ```

## Building and deploying the application

### Building the application

### Running the application

TBC


### Alternative script to run application



### Other


## License
1. MS Power Platform

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

