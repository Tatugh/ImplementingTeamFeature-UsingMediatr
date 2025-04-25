using MediatR;
using StudentEfCoreDemo.Application.DTOs;

namespace StudentEfCoreDemo.Application.Features.Students.Queries
{
    public record GetStudentsQuery : IRequest<List<StudentDto>>;
} 