using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using StudentEfCoreDemo.Application.DTOs;

namespace StudentEfCoreDemo.Application.Features.Teams.Queries
{
    public record GetTeamsQuery : IRequest<IEnumerable<TeamDto>>
    {
    }
}
