using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;

namespace StudentEfCoreDemo.Application.Features.Students.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _repository;

        public CreateStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            var createdStudent = await _repository.AddAsync(student);
            return new StudentDto
            {
                Id = createdStudent.Id,
                FirstName = createdStudent.FirstName,
                LastName = createdStudent.LastName,
                Age = createdStudent.Age
            };
        }
    }
} 