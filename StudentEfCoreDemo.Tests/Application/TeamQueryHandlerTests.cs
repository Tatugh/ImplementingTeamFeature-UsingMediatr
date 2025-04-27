using Moq;
using StudentEfCoreDemo.Application.Features.Teams.Queries;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace StudentEfCoreDemo.Tests.Application
{
    public class TeamQueryHandlerTests
    {
        private readonly Mock<ITeamRepository> _mockRepo;
        private readonly GetTeamsQueryHandler _getTeamsQueryHandler;
        private readonly GetTeamByIdQueryHandler _getTeamByIdQueryHandler;

        public TeamQueryHandlerTests()
        {
            _mockRepo = new Mock<ITeamRepository>();
            _getTeamsQueryHandler = new GetTeamsQueryHandler(_mockRepo.Object);
            _getTeamByIdQueryHandler = new GetTeamByIdQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task GetTeamsHandler()
        {
            var getTeamsQuery = new GetTeamsQuery();

            var teamsList = new List<Team>
            {
                new () { Id = 0, Name = "AA", SportType = "AAType", FoundedDate = DateTime.Now, HomeStadium = "AAdium", MaxRosterSize = 40 },
                new () { Id = 1, Name = "BB", SportType = "BBType", FoundedDate = DateTime.Now, HomeStadium = "BBdium", MaxRosterSize = 30 },
                new () { Id = 2, Name = "CC", SportType = "CCType", FoundedDate = DateTime.Now, HomeStadium = "CCdium", MaxRosterSize = 20 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(teamsList);

            var result = await _getTeamsQueryHandler.Handle(getTeamsQuery, CancellationToken.None);

            result.Count().Should().Be(3);
            _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetTeamByIdHandler()
        {
            var teamToGet = 1;
            var getTeamByIdQuery = new GetTeamByIdQuery(teamToGet);
            var team = new Team
            {
                Id = 0, Name = "AA", SportType = "AAType", FoundedDate = DateTime.Now, HomeStadium = "AAdium", MaxRosterSize = 40
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(team);

            var result = await _getTeamByIdQueryHandler.Handle(getTeamByIdQuery, CancellationToken.None);

            result.Should().NotBeNull();
            result.Name.Should().Be(team.Name);
            result.HomeStadium.Should().Be(team.HomeStadium);
            _mockRepo.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
