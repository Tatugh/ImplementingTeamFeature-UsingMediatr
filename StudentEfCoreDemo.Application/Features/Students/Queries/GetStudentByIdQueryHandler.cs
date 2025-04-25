using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;

namespace StudentEfCoreDemo.Application.Features.Students.Queries
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
    {
        private readonly IStudentRepository _repository;

        public GetStudentByIdQueryHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _repository.GetByIdAsync(request.Id);
            if (student == null)
            {
                return null;
            }

            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age
            };
        }
    }
} 