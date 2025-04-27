using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Domain.Entities;
using StudentEfCoreDemo.Infrastructure.Data;
using StudentEfCoreDemo.Infrastructure.Repositories;
using Xunit;

namespace StudentEfCoreDemo.Tests.Infrastructure
{
    public class TeamRepositoryTests
    {
        private readonly DbContextOptions<StudentContext> _options;
        private readonly StudentContext _studentContext;
        private readonly TeamRepository _teamRepository;

        public TeamRepositoryTests() 
        {
            _options = new DbContextOptionsBuilder<StudentContext>()
                .UseInMemoryDatabase(databaseName: $"TeamDb_{Guid.NewGuid()}")
                .Options;
            _studentContext = new StudentContext(_options);
            _teamRepository = new TeamRepository(_studentContext);
        }

        public List<Team> TeamList()
        {
            return new List<Team>
            {
                new Team { Id = 1, Name = "testiJoukkue", SportType = "TestiLaji", FoundedDate = DateTime.Now, HomeStadium = "Hartvalli", MaxRosterSize = 10},
                new Team { Id = 2, Name = "testiJoukkue2", SportType = "TestiLaji2", FoundedDate = DateTime.Now.AddMinutes(30), HomeStadium = "Hartvalli2", MaxRosterSize = 15}
            };
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTeams()
        {
            var teams = TeamList();

            await _studentContext.Teams.AddRangeAsync(teams);
            await _studentContext.SaveChangesAsync();

            var result = await _teamRepository.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTeam_WithCorrectId()
        {
            var id = 2;
            var teams = TeamList();

            await _studentContext.AddRangeAsync(teams);
            await _studentContext.SaveChangesAsync();

            var result = await _teamRepository.GetByIdAsync(id);

            Assert.True(result.Id == id);
        }

        [Fact]
        public async Task AddAsync_AddsTeamToDb()
        {
            var team = new Team
            {
                Id = 1,
                Name = "Test",
                SportType = "Jääkiekko",
                FoundedDate = DateTime.Now,
                HomeStadium = "Ulkojää4",
                MaxRosterSize = 10
            };

            await _teamRepository.AddAsync(team);

            var result = _studentContext.Teams.FindAsync(team.Id);

            Assert.Equal("Jääkiekko", result.Result.SportType);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTeam()
        {
            var team = new Team
            {
                Id = 1,
                Name = "Test",
                SportType = "Kaukalopallo",
                FoundedDate = DateTime.Now,
                HomeStadium = "Ulkojääd2",
                MaxRosterSize = 10
            };

            await _studentContext.Teams.AddAsync(team);
            await _studentContext.SaveChangesAsync();

            team.Name = "Testi Onnistui";
            await _teamRepository.UpdateAsync(team);

            var result = await _studentContext.Teams.FindAsync(team.Id);

            Assert.Equal("Testi Onnistui", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_DeletesTeam()
        {
            var teams = TeamList();

            var id = 2;

            await _studentContext.AddRangeAsync(teams);
            await _studentContext.SaveChangesAsync();

            await _teamRepository.DeleteByIdAsync(id);

            var result = await _studentContext.Teams.FindAsync(id);
            Assert.True(result == null);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrueIfExists()
        {
            var teams = TeamList();

            await _studentContext.AddRangeAsync(teams);
            await _studentContext.SaveChangesAsync();

            var result = await _teamRepository.ExistsAsync(1);

            Assert.True(result);
        }

        
    }
}
