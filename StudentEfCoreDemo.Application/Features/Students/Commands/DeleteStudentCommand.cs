using MediatR;

namespace StudentEfCoreDemo.Application.Features.Students.Commands
{
    public record DeleteStudentCommand(int Id) : IRequest;
} 