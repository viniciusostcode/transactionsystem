
# Transaction System

This is a modest CRUD project to keep safe anyone's transaction, using .NET Core 6 and ASP.NET Core 6.5 . About data acess, has been used SQL and Entity Framework Core 6.0 as ORM with migrations, adopting a approach code first.


## Stack

**Front-end:** HTML, CSS, JavaScript

**Back-end:** .NET, SQL


## Installation 

Install .NET SDK

```bash
  winget install Microsoft.DotNet.SDK.6
```

Install EntityFrameworkCore

```bash
  dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0
```
```bash
  dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.0
```
Create a local SQL server (https://stackoverflow.com/questions/43355971/create-local-sql-server-database), after that, set the appsettings.json file with your string connection

Example:

![App Screenshot](https://uploaddeimagens.com.br/images/004/714/167/original/Screenshot.png?1705427509)

Run the migration

```bash
  Add-Migration FirstExec -Context TransactionSystemDbContext

  Update-Database -Context TransactionSystemDbContext
```
## Run 

Now you can run the ASP.NET web api with:

```bash
  dotnet run
```

## Contribuitons

Contribuitons are welcome!

Check out `contribuindo.md` to learn how to get started.

Please, follow code of conduct about this project.

## Feedback

If you got any feedback,  let me know via fnkcontato@gmail.com

## [V2.0.0] - 29/01/2024

### Novidades

- Switching the project to a razor pages structure.



