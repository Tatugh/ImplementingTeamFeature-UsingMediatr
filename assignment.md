# Teams Feature Implementation Assignment

## Overview
Implement a new feature for managing sports teams in the StudentEfCoreDemo application. This feature will allow users to perform CRUD operations on sports teams, including team details and player management.

## User Story
As a sports team manager, I want to be able to manage sports teams in the system so that I can track team information and roster changes.

### Acceptance Criteria
1. Create a new team with the following information:
   - Team name
   - Sport type
   - Founded date
   - Home stadium
   - Maximum roster size

2. View all teams in the system
3. View details of a specific team
4. Update team information
5. Delete a team
6. Add/remove players from team roster (optional bonus feature)

## Technical Requirements

### 1. Domain Layer
Create a new Team entity in the Domain layer following the Clean Architecture principles (see `theory.md` - Clean Architecture section).

Required properties:
- Id (int)
- Name (string)
- SportType (string)
- FoundedDate (DateTime)
- HomeStadium (string)
- MaxRosterSize (int)
- Players (ICollection<Player>) - Optional for bonus feature

### 2. Application Layer
Implement the following components (see `README.md` - Step-by-Step Guide section):

1. Create DTOs:
   - TeamDto
   - CreateTeamDto
   - UpdateTeamDto

2. Create Repository Interface:
   - ITeamRepository

3. Create Commands and Queries:
   - CreateTeamCommand
   - UpdateTeamCommand
   - DeleteTeamCommand
   - GetTeamsQuery
   - GetTeamByIdQuery

4. Implement Command/Query Handlers for each command and query

### 3. Infrastructure Layer
1. Update StudentContext to include Teams DbSet
2. Implement TeamRepository
3. Create and apply database migration

### 4. API Layer
Create TeamsController with the following endpoints:
- GET /api/teams
- GET /api/teams/{id}
- POST /api/teams
- PUT /api/teams/{id}
- DELETE /api/teams/{id}

### 5. Testing Requirements
Create a new test project `StudentEfCoreDemo.Tests` and implement the following tests:

1. Unit Tests:
   - Test all command handlers
   - Test all query handlers
   - Test domain logic

2. Integration Tests:
   - Test repository implementation
   - Test API endpoints
   - Test database operations

## Implementation Steps

1. Review the existing documentation:
   - Read `README.md` for implementation guidelines
   - Read `theory.md` for technology and pattern explanations

2. Follow the Clean Architecture implementation guide in `README.md`:
   - Start with Domain layer
   - Move to Application layer
   - Implement Infrastructure layer
   - Create API endpoints

3. Implement tests following the testing guidelines in `README.md`

## Bonus Feature
Implement player management for teams:
- Add player to team
- Remove player from team
- View team roster
- Validate roster size limits

## Submission Requirements

1. Source Code:
   - All new files and modifications
   - Database migration files
   - Test project with all test cases

2. Documentation:
   - Update README.md with new feature documentation
   - Add any new patterns or approaches used
   - Document any challenges faced and solutions

## Evaluation Criteria

1. Code Quality:
   - Clean Architecture principles followed
   - SOLID principles applied
   - Proper error handling
   - Async/await usage
   - Code documentation

2. Testing:
   - Test coverage
   - Test quality
   - Test organization

3. Feature Implementation:
   - All acceptance criteria met
   - Bonus feature implementation (if attempted)
   - API endpoint functionality

## Resources

1. Documentation:
   - `README.md` - Implementation guide
   - `theory.md` - Technology and pattern explanations

2. Reference Implementation:
   - Student feature implementation in the codebase
   - Existing test examples

## Time Estimate
- Basic CRUD implementation: 4-6 hours
- Testing: 2-3 hours
- Bonus feature: 2-3 hours
- Documentation: 1 hour

Total estimated time: 9-13 hours

## Notes
- Follow the existing patterns in the codebase
- Ensure proper error handling and validation
- Use async/await for all database operations
- Implement proper logging
- Consider adding Swagger documentation for new endpoints 