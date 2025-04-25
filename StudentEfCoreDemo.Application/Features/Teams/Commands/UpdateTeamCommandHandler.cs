using MediatR;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.Features.Teams.Commands
{
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamCommandHandler (ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new Team
            {
                Id = request.Id,
                Name = request.Name,
                SportType = request.SportType,
                FoundedDate = request.FoundedDate,
                HomeStadium = request.HomeStadium,
                MaxRosterSize = request.MaxRosterSize
            };

            await _teamRepository.UpdateAsync(team);


        }
    }
}
