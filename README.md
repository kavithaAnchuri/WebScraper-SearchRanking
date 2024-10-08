# SearchRankingSPA

Steps to Run the project:
Ensure Node.js is installed.
- git clone <repository-url>
- cd <project-directory>
- npm install
- ng build
- ng serve 
The application will be available at http://localhost:4200/

This project is built using ASP.NET Core v8 Web API, featuring a structured architecture that includes:

Controllers - for handling HTTP requests.
Data Access Layer - for migrations and repository operations.
Application (Service) Layer - for business logic.
Angular - for the frontend.
SQL Server is used for database operations, with Entity Framework handling data access.

ASP.Net Core WebAPI:
The project is designed to be clean and testable. It uses MediatR to handle web requests and responses, combined with the Repository Pattern. All MediatR handlers and repository dependencies are registered with the DI container, ensuring a decoupled and maintainable architecture.
The project includes Unit Tests to ensure code functionality.
Swagger is integrated for API testing.

WebAPP:
The application is designed for simplicity. You can enter a keyword and URL to search for rankings. The ranking result will be displayed in a banner for 3 seconds. Live, you can observe that the search history updates with new search values.

![SearchEngine_1](https://github.com/user-attachments/assets/20283ab2-abf5-462b-9513-427810feea7d)

![SearchEngine_2](https://github.com/user-attachments/assets/be7746c3-7124-472a-a230-c69b03d4bba7)


