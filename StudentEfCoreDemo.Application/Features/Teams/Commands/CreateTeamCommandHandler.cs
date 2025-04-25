using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Commands
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamDto>
    {
        private readonly ITeamRepository _repository;

        public CreateTeamCommandHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateTeamDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Id = request.Id,
                Name = request.Name,
                SportType = request.SportType,
                FoundedDate = request.FoundedDate,
                HomeStadium = request.HomeStadium,
                MaxRosterSize = request.MaxRosterSize,
            };

            var createdTeam = await _repository.AddAsync(team);
            return new CreateTeamDto
            {
                Id = createdTeam.Id,
                Name = createdTeam.Name,
                SportType = createdTeam.SportType,
                FoundedDate = createdTeam.FoundedDate,
                HomeStadium = createdTeam.HomeStadium,
                MaxRosterSize = createdTeam.MaxRosterSize
            };
        }
    }
}
