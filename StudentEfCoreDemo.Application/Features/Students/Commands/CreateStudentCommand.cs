using MediatR;
using StudentEfCoreDemo.Application.DTOs;

namespace StudentEfCoreDemo.Application.Features.Students.Commands
{
    public record CreateStudentCommand : IRequest<StudentDto>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public int Age { get; init; }
    }
} 