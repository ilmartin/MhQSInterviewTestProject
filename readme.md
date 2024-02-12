# Repository Name
This repository contains two projects:

## 1. MartinHuiLoanApplicationApi 

Description: This project contains the ASP.NET API application. Since I created the Api repo before this, therefore I added the api repo as a submodule to this repo, please navigate to the https://github.com/ilmartin/MHInterviewTestAtQS directory for more information.

## 2. MartinHuiLoanApplicationApi.Test

Description: This project contains unit tests for the ASP.NET API project.


# Getting Started

Follow the instructions below to run the project

## Preacquisition

- The project is build by Visual Studio 2022
- Using .NET 6 Asp.net
- Database should be deployed to SQL server

## Create Database

Runs the following script to create a new Database and tables in SQL Server

- There are SQL scripts provided in the [SQL script]https://github.com/ilmartin/MHInterviewTestAtQS/tree/master/SQL%20script folder
- Use the [Create_Database.sql](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/SQL%20script/Create_Database.sql) script to create a new database in your Database server instance.
- Use the [Create_ServiceAccount.sql.dist](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/SQL%20script/Create_ServiceAccount.sql.dist) script file to create a service account to let the API access to the database via the account. Please do enter a password `CREATE LOGIN ServiceAccount WITH PASSWORD = 'Enter Password';` in the code block before running. The file with `.dist` extension is because, the original script with a password written is gitignored, you may want to remove the `.dist` extension before running if it is not working.
- Use the [Create_Table_LoanProduct.sql](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/SQL%20script/Create_Table_LoanProduct.sql) script to create the LoanProduct table, that will store all the data for loan products.
- Use the [Insert_LoanProduct_Sample.sql](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/SQL%20script/Insert_LoanProduct_Sample.sql) script to insert dummy data into the loan product table.
- Use the [Create_Table_LoanApplicants.sql](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/SQL%20script/Create_Table_LoanApplicants.sql) script to create a loan applicant table, the stores applicants data.

> All create table scripts contain a delete table script at the top of each script, it is commented out, but do run it if necessary and if helpful.

## API setup

Provide the connection strings to the Database in the application setting files, so that the API can reach the database.

- Under the [appsettings.Production.json](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/appsettings.Production.json), please enter the following details to the connection strings:
	- Name of the server
	- Name of the database
	- Initial catalog, which is the name of the database
	- User account, the service account name
	- User Password, the service account password
	
> If the appsettings is not picked up, try changing the name of the file `appsettings.Production.json` to `appsettings.Development.json` or `appsettings.json`

## Running the Api

Suggest to open Visual studio using the solution file at [MartinHuiLoanApplicationApi.sln
](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/MartinHuiLoanApplicationApi.sln), this should contains both the API project and Unit test project.

The API is powered by Swagger, it provides a friendly interface and demostration to call the api. The API endpoint as requested in the test is: 
|Detail|Value|Notes|
|-----|-----|-----|
|*Endpoint*|`/api/Prequalification`||
|*Sample Request Body*|```json{
  "firstName": "Martin",
  "lastName": "Hui",
  "address": "111 A Street",
  "city": "Leicester",
  "country": "gb",
  "postalCode": "LE1 1AA",
  "dateOfBirth": "2000-02-12T14:23:25.514Z",
  "annualIncome": 31000
}```|The provided properties are mandatory, the rest of the property in the model are optional as they are not a part of the requirment|

> The controller will validate the incoming data, then insert the new applicant in the loan aplicant table, and return a result of all qualified loan products using the encapsulated LINQ query in the [LoanProductService.cs](https://github.com/ilmartin/MHInterviewTestAtQS/blob/master/Services/LoanProductService.cs) class.



## Running Tests

Follow the instructions to run unit test:

- If the solution did not contain the Unit Test project, please import the project to the solution from the parent folder.
- Use Test , Run All Tests in Visual Studio, to run all test cases.
- There are 13 test cases:
|PostPrequalification_Ok|Test if controller returning the correct result when a valid body is given|
|PostPrequalification_NotFound|Test if controller returning the not found result when a valid body is given but no matching loan product can returned|
|PostPrequalification_InvalidAddressLength|Test if the controller returns bad request when address property lenght is over maximum|
|PostPrequalification_InvalidAnnualIncome_OverMaximum|Test if the controller returns bad request when annual income property value is over maximum|
|PostPrequalification_InvalidAnnualIncome_LessThanZero|Test if the controller returns bad request when annual income property value is lower than zero|
|PostPrequalification_InvalidCityLength|Test if the controller returns bad request when city property lenght is over maximum|
|PostPrequalification_InvalidCountryCode|Test if the controller returns bad request when country property provided a invalid code, the country property is expecting to receive a two letter iso reigion code.|
|PostPrequalification_InvalidCountryLength|Test if the controller returns bad request when country property lenght is over maximum|
|PostPrequalification_InvalidDateOfBirth_TooOld|Test if the controller returns bad request when Date of birth property value is older than 150 years old|
|PostPrequalification_InvalidDateOfBirth_TooYoung|Test if the controller returns bad request when Date of birth property value is lower than 18 years old|
|PostPrequalification_InvalidFirstnameLength|Test if the controller returns bad request when firstname property lenght is over maximum|
|PostPrequalification_InvalidLastnameLength|Test if the controller returns bad request when lastname property lenght is over maximum|
|PostPrequalification_InvalidPostCode_WithSymbol|Test if the controller returns bad request when postcode property format is incorrect|

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvement, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
