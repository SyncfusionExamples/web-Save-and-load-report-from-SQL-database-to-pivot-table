# How to save and load reports from a SQL Server Database to a Pivot Table

A quick start project that shows how to save and load reports from a SQL Server database and load them into the Syncfusion Pivot Table at run time. This repository includes a ASP.NET Core Web App Controller ([MyWebService](../MyWebService/)) for saving and loading reports from a SQL server database, as well as a quick start samples in the React platform that displays the loaded report in a Syncfusion Pivot Table.

## Project prerequisites

Before beginning work on the server and client projects, ensure the following software to be installed in the machine.

* [git](https://git-scm.com/downloads)
* [Node.js](https://nodejs.org/en/)
* [React](https://reactjs.org/)
* [Visual Studio Code](https://code.visualstudio.com/)
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/ ) and [.NET Core SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or later installed on your machine.

## How to run this application?

* To run this application, clone the [Save-and-load-report-from-SQL-database-to-pivot-table](https://github.com/SyncfusionExamples/Save-and-load-report-from-SQL-database-to-pivot-table) repository and then open **MyWebService** in Visual Studio 2022. Simply build and run your project in IIS Express, and it will host and display the URL [`https://localhost:44313`](https://localhost:44313).

* Now open React sample in Visual Studio Code and and install the necessary npm packages using the following command.

```sh
npm install
```

* Initialize the Pivot Table, create a pivot report based on the given data source, map the hosted url to perform save and load reports with the SQL database, and finally run your project with the following command.

```sh
npm start
```