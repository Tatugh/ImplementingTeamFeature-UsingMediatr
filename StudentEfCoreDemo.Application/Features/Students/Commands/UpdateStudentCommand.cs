using MediatR;

namespace StudentEfCoreDemo.Application.Features.Students.Commands
{
    public record UpdateStudentCommand : IRequest
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public int Age { get; init; }
    }
} 