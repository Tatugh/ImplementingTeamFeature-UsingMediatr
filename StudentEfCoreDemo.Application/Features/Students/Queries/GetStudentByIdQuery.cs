using MediatR;
using StudentEfCoreDemo.Application.DTOs;

namespace StudentEfCoreDemo.Application.Features.Students.Queries
{
    public record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;
} 