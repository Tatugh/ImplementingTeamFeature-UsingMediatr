using MediatR;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;

namespace StudentEfCoreDemo.Application.Features.Students.Commands
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _repository;

        public UpdateStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            await _repository.UpdateAsync(student);
        }
    }
} 