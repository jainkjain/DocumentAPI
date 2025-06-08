## How to Try Out the Project Web API
You can explore and test the functionality of our Project Web API through our Swagger interface. Follow these steps to get started:

Access the Swagger UI: Open your web browser and navigate to the Swagger UI by clicking on this [link]( 

Explore the Endpoints: Browse through the available API endpoints listed in the Swagger interface. Each endpoint represents a different operation you can perform with the API.

Test the API: To test an endpoint, select it from the list, and then click the "Try it out" button. Fill in any required parameters and click "Execute" to send a request to the API.

View Responses: After executing a request, you can view the response details, including the status code, response body, and any headers returned by the API.

The Swagger UI provides an intuitive way to interact with the API and understand its capabilities without writing any code. Feel free to explore and experiment with different endpoints to get a better understanding of how the API works.

## Tech Stack

| Layer    | Technology                     |
| -------- | ------------------------------ |
| Backend  | ASP.NET Core (.NET 8/9)        |
| Language | C#                             |
| ORM      | Entity Framework Core          |
| Database | PostgreSQL                     |
| Auth     | JWT + Role-based Authorization |

## Architecture

The project follows a clean architecture with the following components:
Documents Management API

- **Documents.Infrastructure** : Implements services and repositories declared in the Domain or Application layers. Includes Entity Framework Core implementations, DBContext, file services, etc.
- **Document.Domain** : Holds core domain entities (e.g., Document) and interfaces (abstractions). It’s pure C# and has no dependencies on external libraries. Defines the business rules.
- **Document.Application** : Contains business logic, use cases, service interfaces. This is the core layer for implementing application features data is stored or retrieved.
- **Document.API**: The entry point of the application. Hosts controllers, middleware, routing, and handles HTTP requests. Communicates with the Application layer via dependency injection.

User Management API
The project follows a architecture with the following components:

- **Controllers**: Handle HTTP requests and responses.
- **Services**: Contain business logic and interact with repositories.
- **Repositories**: Handle data access and database operations.
- **Models**: Define the data structures and validation rules.

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (https://visualstudio.microsoft.com/vs/)
- Microsoft SQL Management Studio (SSMS)  (https://aka.ms/ssmsfullsetup)
- PostgreSQL (https://www.postgresql.org/download/


### Building the Solution

1. Clone the repository:
    ```sh
    git clone  
    ```

2. Restore the dependencies and build the solution:
    ```sh
    dotnet restore
    dotnet build
    ```
	
### Running the Solution
1. Run the application:
    ```sh
    dotnet run --project UserManagement
    ```
Document Management API-
1. The API will be available at `http://localhost:5137` and `http://localhost:18749`.

User Management API-
1. The API will be available at `http://localhost:5086` and `http://localhost:51653`.

## Unit Tests
User Management API
	The project includes unit tests for controllers, services, and repository layers using xUnit and Moq.

### Running Unit Tests

1. Navigate to the test project directory:
    ```sh
    cd UserManagement.Tests
    ```

2. Run the tests:
    ```sh
    dotnet test
    ```

### Test Coverage

The unit tests cover various scenarios, including:

- Register users it basic test case is made.
- Handling errors and logging. 


### Deploy

Prerequisites

✅ Windows with IIS installed

Enable from Control Panel → Programs → Turn Windows features on/off → Internet Information Services

✅ .NET Hosting Bundle installed

Download and install from:
👉 https://dotnet.microsoft.com/en-us/download/dotnet/8.0
(Install the Hosting Bundle, not just the SDK)

For a more in-depth understanding, you might also find this 20-minute tutorial helpful:

👉  .NET Core Blog Tutorial: Setting up IIS & SQL Server (https://www.youtube.com/watch?v=OlkpRoE2mYQ&feature=youtu.be