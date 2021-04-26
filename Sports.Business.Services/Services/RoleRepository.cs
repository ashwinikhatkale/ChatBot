using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Data.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ChatBotContext _context;
        public RoleRepository(ChatBotContext context)
        {
            _context = context;
        }
        public async Task<RoleModel> GetRole(long id)
        {
            var role = await _context.Roles
                                    .Where(x => x.Id == id)
                                    .Select(x => new RoleModel { Id = x.Id, Name = x.Name }).FirstOrDefaultAsync();

            return role;
        }
        
        public async Task<List<SelectListItem>> GetRoleSelectList(long teamId)
        {
            var role = await _context.Roles
                                   .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToListAsync();

            return role;
        }
        public async Task<bool> Insert(RoleModel model)
        {
            var role = new Data.Entities.Role { Id = model.Id, Name = model.Name };
            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(RoleModel model)
        {
            var role = await _context.Roles
                                    .Where(x => x.Id == model.Id).FirstOrDefaultAsync();

            if (role != null)
            {
                role.Id = model.Id;
                role.Name = model.Name;
                
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Delete(long id)
        {
            var role = await _context.Roles
                                    .Where(x => x.Id == id).FirstOrDefaultAsync();

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
