using MediatR;
using StudentEfCoreDemo.Application.Interfaces;

namespace StudentEfCoreDemo.Application.Features.Students.Commands
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IStudentRepository _repository;

        public DeleteStudentCommandHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
        }
    }
} 