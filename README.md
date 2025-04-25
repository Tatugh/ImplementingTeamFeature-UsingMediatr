# StudentEfCoreDemo - Clean Architecture Implementation Guide

This guide explains how to implement new features in the StudentEfCoreDemo system using Clean Architecture principles and CQRS pattern with MediatR.

## Project Structure

The solution is organized into four projects following Clean Architecture principles:

- **StudentEfCoreDemo.Domain**: Contains business entities and domain logic
- **StudentEfCoreDemo.Application**: Contains business rules, interfaces, and CQRS commands/queries
- **StudentEfCoreDemo.Infrastructure**: Contains data access and external service implementations
- **StudentEfCoreDemo.API**: Contains API controllers and configuration

## Step-by-Step Guide for Implementing New Features

### 1. Domain Layer (StudentEfCoreDemo.Domain)

Start by defining your domain entities in the Domain layer:

1. Create a new entity class in `Domain/Entities/`
   ```csharp
   namespace StudentEfCoreDemo.Domain.Entities
   {
       public class YourEntity
       {
           public int Id { get; set; }
           // Add other properties
       }
   }
   ```

2. Add any domain-specific validation or business rules
3. Keep the domain layer pure and free of dependencies on other layers

### 2. Application Layer (StudentEfCoreDemo.Application)

The Application layer is where most of the implementation work happens. Follow these steps:

1. **Create DTOs**
   - Create a new DTO class in `Application/DTOs/`
   ```csharp
   namespace StudentEfCoreDemo.Application.DTOs
   {
       public class YourEntityDto
       {
           public int Id { get; set; }
           // Add properties matching your entity
       }
   }
   ```

2. **Define Repository Interface**
   - Create a new interface in `Application/Interfaces/`
   ```csharp
   namespace StudentEfCoreDemo.Application.Interfaces
   {
       public interface IYourEntityRepository
       {
           Task<IEnumerable<YourEntity>> GetAllAsync();
           Task<YourEntity?> GetByIdAsync(int id);
           Task<YourEntity> AddAsync(YourEntity entity);
           Task UpdateAsync(YourEntity entity);
           Task DeleteAsync(int id);
           Task<bool> ExistsAsync(int id);
       }
   }
   ```

3. **Create Commands and Queries**
   - Create command classes in `Application/Features/YourFeature/Commands/`
   ```csharp
   public record CreateYourEntityCommand : IRequest<YourEntityDto>
   {
       // Add properties needed for creation
   }
   ```
   - Create query classes in `Application/Features/YourFeature/Queries/`
   ```csharp
   public record GetYourEntityQuery : IRequest<YourEntityDto>
   {
       public int Id { get; init; }
   }
   ```

4. **Implement Command/Query Handlers**
   - Create handlers in the same folders as their commands/queries
   ```csharp
   public class CreateYourEntityCommandHandler : IRequestHandler<CreateYourEntityCommand, YourEntityDto>
   {
       private readonly IYourEntityRepository _repository;

       public CreateYourEntityCommandHandler(IYourEntityRepository repository)
       {
           _repository = repository;
       }

       public async Task<YourEntityDto> Handle(CreateYourEntityCommand request, CancellationToken cancellationToken)
       {
           // Implement the business logic
       }
   }
   ```

### 3. Infrastructure Layer (StudentEfCoreDemo.Infrastructure)

Implement the data access and external service integrations:

1. **Update DbContext**
   - Add your entity to `Infrastructure/Data/StudentContext.cs`
   ```csharp
   public DbSet<YourEntity> YourEntities { get; set; } = null!;
   ```

2. **Implement Repository**
   - Create repository implementation in `Infrastructure/Repositories/`
   ```csharp
   public class YourEntityRepository : IYourEntityRepository
   {
       private readonly StudentContext _context;

       public YourEntityRepository(StudentContext context)
       {
           _context = context;
       }

       // Implement interface methods
   }
   ```

### 4. API Layer (StudentEfCoreDemo.API)

Create the API endpoints:

1. **Create Controller**
   - Create a new controller in `API/Controllers/`
   ```csharp
   [ApiController]
   [Route("api/[controller]")]
   public class YourEntityController : ControllerBase
   {
       private readonly IMediator _mediator;

       public YourEntityController(IMediator mediator)
       {
           _mediator = mediator;
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<YourEntityDto>>> GetAll()
       {
           var query = new GetYourEntitiesQuery();
           var result = await _mediator.Send(query);
           return Ok(result);
       }

       // Add other endpoints
   }
   ```

2. **Register Dependencies**
   - Add repository registration in `Program.cs`
   ```csharp
   builder.Services.AddScoped<IYourEntityRepository, YourEntityRepository>();
   ```

### 5. Database Migration

After implementing the feature, create and apply the database migration:

1. Open Package Manager Console
2. Set Default Project to `StudentEfCoreDemo.Infrastructure`
3. Run the following commands:
   ```powershell
   Add-Migration AddYourEntity
   Update-Database
   ```

## Best Practices

1. **Dependency Direction**
   - Domain layer should have no dependencies
   - Application layer depends only on Domain
   - Infrastructure depends on Application
   - API depends on Application and Infrastructure

2. **CQRS Pattern**
   - Use commands for write operations (Create, Update, Delete)
   - Use queries for read operations (Get, List)
   - Keep commands and queries focused and single-purpose

3. **DTOs**
   - Use DTOs to decouple API models from domain models
   - Keep DTOs simple and focused on data transfer
   - Avoid business logic in DTOs

4. **Repository Pattern**
   - Keep repository interfaces in Application layer
   - Implement repositories in Infrastructure layer
   - Use async/await for all database operations

5. **Error Handling**
   - Implement proper error handling in handlers
   - Return appropriate HTTP status codes from controllers
   - Consider adding validation using FluentValidation

## Testing

1. **Unit Tests**
   - Test command/query handlers
   - Test domain logic
   - Mock dependencies using Moq

2. **Integration Tests**
   - Test repository implementations
   - Test API endpoints
   - Use test database for integration tests

## Example Implementation

For a complete example, look at the Student feature implementation in the codebase, which follows all these principles and patterns. 