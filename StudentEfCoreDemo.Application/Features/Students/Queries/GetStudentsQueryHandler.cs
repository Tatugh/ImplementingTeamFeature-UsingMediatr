using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;

namespace StudentEfCoreDemo.Application.Features.Students.Queries
{
    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentDto>>
    {
        private readonly IStudentRepository _repository;

        public GetStudentsQueryHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _repository.GetAllAsync();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Age = s.Age
            }).ToList();
        }
    }
} 