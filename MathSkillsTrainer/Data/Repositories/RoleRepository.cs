using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSkillsTrainer.Data.Interfaces;
using MathSkillsTrainer.Models;
using System.Data.Entity;

namespace MathSkillsTrainer.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MathSkillsTrainerDbContext _context;

        public RoleRepository(MathSkillsTrainerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetRoleBySystemNameAsync(string systemName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.SystemName == systemName);
        }

        public async Task AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
