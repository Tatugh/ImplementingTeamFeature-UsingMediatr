using StudentEfCoreDemo.Application.Interfaces;
using StudentEfCoreDemo.Infrastructure.Data;
using StudentEfCoreDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StudentEfCoreDemo.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly StudentContext _studentContext;

        public TeamRepository (StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _studentContext.Teams.ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(int id)
        {
            return await _studentContext.Teams.FindAsync(id);
        }

        public async Task<Team> AddAsync(Team team)
        {
            await _studentContext.Teams.AddAsync(team);
            await _studentContext.SaveChangesAsync();
            return team;
        }

        public async Task UpdateAsync(Team team)
        {
            _studentContext.Teams.Update(team);
            await _studentContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var team = await _studentContext.Teams.FindAsync(id);
            if (team != null)
            {
                _studentContext.Teams.Remove(team);
                await _studentContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _studentContext.Teams.AnyAsync(t => t.Id == id);
        }
    }
}
