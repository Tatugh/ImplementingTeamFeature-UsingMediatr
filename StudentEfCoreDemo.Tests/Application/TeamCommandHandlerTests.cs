using FluentAssertions;
using Moq;
using StudentEfCoreDemo.Application.Features.Teams.Commands;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Tests.Application
{
    public class TeamCommandHandlerTests
    {
        private readonly Mock<ITeamRepository> _repoMock;
        private readonly CreateTeamCommandHandler _createHandler;
        private readonly DeleteTeamCommandHandler _deleteHandler;
        private readonly UpdateTeamCommandHandler _updateHandler;
        public TeamCommandHandlerTests()
        {
            _repoMock = new Mock<ITeamRepository>();
            _createHandler = new CreateTeamCommandHandler(_repoMock.Object);
            _deleteHandler = new DeleteTeamCommandHandler(_repoMock.Object);
            _updateHandler = new UpdateTeamCommandHandler(_repoMock.Object);
        }

        [Fact]
        public async Task CreateTeamHandler()
        {
            //var handler = new CreateTeamCommandHandler(_repoMock.Object);

            var command = new CreateTeamCommand
            {
                Id = 1,
                Name = "Test",
                SportType = "Testoilu",
                FoundedDate = DateTime.Now,
                HomeStadium = "Testadium",
                MaxRosterSize = 10
            };

            var expectedTeam = new Team
            {
                Id = command.Id,
                Name = command.Name,
                SportType = command.SportType,
                FoundedDate = command.FoundedDate,
                HomeStadium = command.HomeStadium,
                MaxRosterSize = command.MaxRosterSize
            };

            _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Team>())).ReturnsAsync(expectedTeam);

            var result = await _createHandler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(expectedTeam.Id);
            result.Name.Should().Be(expectedTeam.Name);
            result.SportType.Should().Be(expectedTeam.SportType);
            result.FoundedDate.Should().Be(expectedTeam.FoundedDate);
            result.HomeStadium.Should().Be(expectedTeam.HomeStadium);
            result.MaxRosterSize.Should().Be(expectedTeam.MaxRosterSize);
        }

        [Fact]
        public async Task DeleteTeamHandler()
        {
            var command = new DeleteTeamCommand(1);

            _repoMock.Setup(repo => repo.DeleteByIdAsync(It.IsAny<int>()));

            await _deleteHandler.Handle(command, CancellationToken.None);

            _repoMock.Verify(repo => repo.DeleteByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task UpdateTeamHandler()
        {
            var command = new UpdateTeamCommand
            {
                Id = 1,
                Name = "Test",
                SportType = "Testoilu",
                FoundedDate = DateTime.Now,
                HomeStadium = "Testadium",
                MaxRosterSize = 10
            };

            _repoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Team>()));

            await _updateHandler.Handle(command, CancellationToken.None);

            _repoMock.Verify(repo => repo.UpdateAsync(It.IsAny<Team>()), Times.Once);
        }
    }
}
