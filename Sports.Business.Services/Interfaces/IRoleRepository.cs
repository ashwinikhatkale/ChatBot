using ChatBot.Business.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Interfaces
{
   public interface IRoleRepository
    {
        Task<RoleModel> GetRole(long id);
        Task<List<SelectListItem>> GetRoleSelectList(long teamId);
        Task<bool> Insert(RoleModel model);
        Task<bool> Update(RoleModel model);
        Task<bool> Delete(long id);
    }
}

