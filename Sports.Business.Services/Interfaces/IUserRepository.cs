using ChatBot.Business.Services.Models;
using System.Threading.Tasks;

namespace ChatBot.Business.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUser(long id);
        Task<bool> Insert(UserModel model);
        Task<bool> Update(UserModel model);
        Task<bool> ChangePassword(long userId, string password);
        Task<bool> Delete(long id);
        Task<UserModel> CheckUserExists(string username, string password);
        bool CheckEmailIdExists(string email);
        Task<bool> ChangePasswordByEmail(string email, string password);
        Task<bool> IsUserWithEmailIdExists(string email);
        
    }
}
