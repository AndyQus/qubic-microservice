# Qubic Microservice

A backend microservice built with **ASP.NET Core** and **MongoDB** for managing transactions, following **Clean Architecture** principles. This structure provides modularity and separation of concerns by grouping code into different layers.

## Project Structure

This project follows a **Clean Architecture** approach, separating the application into different layers to ensure maintainability, scalability, and ease of testing.

```plaintext
QubicMicroservice/
├── requests/Transactions.http           # App requests for testing API
├── Application/                         # Application layer for core logic and DTOs
│   ├── Interfaces/
│   │   └── ITransactionService.cs       # Service interface for transactions
│   ├── Services/
│   │   └── TransactionService.cs        # Service implementation for transaction logic
│   └── DTOs/
│       └── TransactionDto.cs            # Data Transfer Objects for transactions
├── Domain/                              # Domain layer with entities and core interfaces
│   ├── Entities/
│   │   └── Transaction.cs               # Domain entity for transactions
│   └── Interfaces/
│       └── ITransactionRepository.cs    # Repository interface for transaction persistence
├── Infrastructure/                      # Infrastructure layer for database and repository implementation
│   ├── Data/
│   │   └── MongoDBContext.cs            # MongoDB context setup
│   │   └── MongoDBSettings.cs           # MongoDB settings
│   └── Repositories/
│       └── TransactionRepository.cs     # Implementation of transaction repository
├── WebAPI/                              # ASP.NET Core Web API layer
│   ├── Controllers/
│   │   └── TransactionsController.cs    # API controller for transaction endpoints
│   ├── Program.cs                       # Application entry point
│   └── Startup.cs                       # Configures services and dependency injection
├── appsettings.json                     # Application configuration file for environment variables
└── README.md                            # Project documentation
```

## Getting Started

### Prerequisites

- **Docker** and **Docker Compose**

### Running the Application

1. **Clone the repository**:

   ```bash
   git clone <repo-url>
   cd QubicMicroservice
   ```

2. **Run the app using Docker Compose**:

   ```bash
   docker-compose up --build
   ```

   This command will:

   - Build the ASP.NET Core API service.
   - Start MongoDB and seed the database with initial transaction data from `mongo-seed/seed.js`.

3. **Access the API**:
   The service will be available at `http://localhost:8080`.

4. **Verify Seeding** (Optional):
   To verify that MongoDB was seeded successfully:

   ```bash
   docker exec -it qubic_mongo mongosh QubicMicroserviceDB --eval "db.transactions.find()"
   ```

5. **Swagger API Documentation**:
   When the application is running, Swagger documentation is accessible at:
   ```plaintext
   http://localhost:8080/swagger
   ```
   Use this interface to explore and test all available API endpoints.

### Environment Variables

- **MongoDBSettings\_\_ConnectionString**: `mongodb://mongo:27017`
- **MongoDBSettings\_\_DatabaseName**: `QubicMicroserviceDB`
- **ASPNETCORE_ENVIRONMENT**: `Development`

### Additional Notes

- **Controller Layer**: `TransactionsController` in `WebAPI/Controllers` manages HTTP requests related to transactions.
- **Application Layer**: Contains business logic (`TransactionService`) and Data Transfer Objects (`TransactionDto`).
- **Domain Layer**: Houses the core `Transaction` entity and repository interface `ITransactionRepository`.
- **Infrastructure Layer**: Manages MongoDB connections through `MongoDBContext` and implements data persistence in `TransactionRepository`.

---
