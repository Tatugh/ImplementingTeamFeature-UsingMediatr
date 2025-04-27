using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentEfCoreDemo.Domain.Entities;

namespace StudentEfCoreDemo.Tests.Domain
{
    public class TeamTests
    {
        [Fact]
        public void CreateTeam_WithValidData()
        {
            var team = new Team
            {
                Id = 1,
                Name = "Test",
                SportType = "Joo",
                FoundedDate = DateTime.Now,
                HomeStadium = "Stadioni",
                MaxRosterSize = 10
            };

            Assert.IsType<int>(team.Id);
            Assert.IsType<string>(team.Name);
            Assert.IsType<string>(team.SportType);
            Assert.IsType<DateTime>(team.FoundedDate);
            Assert.IsType<int>(team.MaxRosterSize);
        }
    }
}
