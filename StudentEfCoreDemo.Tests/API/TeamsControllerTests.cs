using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentEfCoreDemo.API.Controllers;
using StudentEfCoreDemo.Application.DTOs;
using StudentEfCoreDemo.Application.Features.Teams.Commands;
using StudentEfCoreDemo.Application.Features.Teams.Queries;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Tests.API
{
    public class TeamsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly TeamsController _teamsController;

        public TeamsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _teamsController = new TeamsController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnTeams()
        {
            // Arrange
            var teams = new List<TeamDto>
            {
                new ()
                {
                    Id = 1,
                    Name = "TestTeam1",
                    SportType = "Jääkiekko",
                    FoundedDate = DateTime.Now,
                    HomeStadium = "TestStadium",
                    MaxRosterSize = 10
                },
                new ()
                {
                    Id = 2,
                    Name = "TestTeam2",
                    SportType = "Jalkapallo",
                    FoundedDate = DateTime.Now.AddHours(-1),
                    HomeStadium = "TestStadium2",
                    MaxRosterSize = 2
                }
            };

            // Act
            _mockMediator.Setup(mock => 
                mock.Send(It.IsAny<GetTeamsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(teams);

            // Assert
            var result = await _teamsController.GetAll();
            var actionResult = result.Result as OkObjectResult;
            var resultTeams = actionResult.Value as List<TeamDto>;
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(2, resultTeams.Count);
            //resultTeams.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_ShouldReturnTeam_ById()
        {
            var teams = new List<TeamDto>
            {
                new ()
                {
                    Id = 1,
                    Name = "TestTeam1",
                    SportType = "Jääkiekko",
                    FoundedDate = DateTime.Now,
                    HomeStadium = "TestStadium",
                    MaxRosterSize = 10
                },
                new ()
                {
                    Id = 2,
                    Name = "TestTeam2",
                    SportType = "Jalkapallo",
                    FoundedDate = DateTime.Now.AddHours(-1),
                    HomeStadium = "TestStadium2",
                    MaxRosterSize = 2
                }
            };


            _mockMediator.Setup(mock =>
                mock.Send(It.IsAny<GetTeamByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(teams[1]);

            var result = await _teamsController.GetById(2);

            var actionResult = result.Result as OkObjectResult;
            var teamResult = actionResult.Value as TeamDto;
            Assert.False(actionResult == null);
            Assert.False(teamResult == null);
            Assert.True(teamResult.Id == 2);
        }

        [Fact]
        public async Task Add_ShouldAddTeam()
        {
            var teamToAddCommand = new CreateTeamCommand
            {
                Id = 3,
                Name = "TestTeam3",
                SportType = "Futsal",
                FoundedDate = DateTime.Now.AddMinutes(10),
                HomeStadium = "Ei ole",
                MaxRosterSize = 2
            };

            var teamToAdd = new CreateTeamDto
            {
                Id = teamToAddCommand.Id,
                Name = teamToAddCommand.Name,
                SportType = teamToAddCommand.SportType,
                FoundedDate = teamToAddCommand.FoundedDate,
                HomeStadium = teamToAddCommand.HomeStadium,
                MaxRosterSize = teamToAddCommand.MaxRosterSize
            };

            _mockMediator.Setup(mock => 
                mock.Send(It.IsAny<CreateTeamCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(teamToAdd);


            var result = await _teamsController.Add(teamToAddCommand);

            var actionResult = result.Result as OkObjectResult;
            var teamResult = actionResult.Value as CreateTeamDto;
            Assert.False(actionResult == null);
            Assert.False(teamResult == null);
            Assert.Equal("Futsal", teamResult.SportType);
        }

        [Fact]
        public async Task Update_ReturnsNoContentResult_WhenUpdatesTeam()
        {
            var teamToUpdateCommand = new UpdateTeamCommand
            {
                Id = 3,
                Name = "TestTeam3",
                SportType = "Futsal",
                FoundedDate = DateTime.Now.AddMinutes(10),
                HomeStadium = "Ei ole",
                MaxRosterSize = 2
            };

            var result = await _teamsController.Update(3, teamToUpdateCommand);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentResult_WhenDeletesTeam ()
        {
            var result = await _teamsController.DeleteTeam(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
