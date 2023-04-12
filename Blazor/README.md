# How to bind Microsoft Sql database to a Pivot Table 

A quick start project that shows how to save and load reports from a SQL Server database and load them into the Syncfusion Pivot Table at run time. This repository includes a ASP.NET Core Web App Controller ([MyWebService](../MyWebService/)) for saving and loading reports from a SQL server database, as well as a quick start sample in the Blazor platform that displays the loaded report in a Syncfusion Pivot Table.

## Project prerequisites

Before beginning work on the server and client projects, ensure the following software to be installed in the machine.

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.NET Core SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or later installed on your machine


## How to run this application?

To run this application, clone the [Save-and-load-report-from-SQL-database-to-pivot-table](https://github.com/SyncfusionExamples/Save-and-load-report-from-SQL-database-to-pivot-table) repository.

### Using the Web API service to connect MySql to a Pivot Table

* To connect a SQL server database to a Pivot Table using the Web API Controller (aka, MyWebService), open the **MyWebService** project in Visual Studio 2022. Simply build and run your project in IIS Express, and it will host and display the URL `https://localhost:44313`.

* Now open the **MyBlazorServerApp** project in Visual Studio 2022. Initialize the Pivot Table, map the hosted URL, create a pivot report based on the given data source, and finally, run your project to perform save and load reports with the SQL database.