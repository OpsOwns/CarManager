# CarManager

The purpose of the system is mange cars of customers. The system help workers identify which car is whose. Administrator of the system can manage workers accounts. The system will be expanded about new features.

# Technological stack

 - .NET 6
 - C#
 - Azure SQL Database
 - Azure Blob storage
 - Azure Key Vault
 - Application Insights

## Architecture/Project Structure

The system was built with simple clean architecture with cqrs and domain driven design approach.
-   Domain (entities, value objects, aggregates, repositories interfaces, exceptions etc.)
-   Infrastructure (Database operations, Queries, authentication etc.)
-   Application (Domain logic implementation, Commands)
-   Api (Presentation layer)

## Libraries/Frameworks

 - Entity Framework Core
 - Xunit
 - Hellang Problem Details
 - Swagger
 -  Azure libs

# Project details

Domain value objects and other stuff was delegate to use Result pattern to return error instead of throwing exception. The idea was to focus on better performance than complexity. So the api will returns error results as bad request instead of throwing exception. The exception will be handled by problem details in unexpected situation like database connection error. All commands are wrapped by decorator it was used Unit of work pattern on whole command not only invoked as a single line from code. 

# License

This project is licensed under the  [MIT License](https://github.com/OpsOwns/CarManager/blob/master/LICENSE.md).

