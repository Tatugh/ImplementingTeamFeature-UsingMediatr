using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEfCoreDemo.Application.DTOs;
using MediatR;

namespace StudentEfCoreDemo.Application.Features.Teams.Commands
{
    public record DeleteTeamCommand(int Id) : IRequest;

}
