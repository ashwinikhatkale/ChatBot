using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Data.Entities;
using ChatBot.Data.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Business.Services.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatBotContext _context;
        public UserRepository(ChatBotContext context)
        {
            _context = context;
        }
        public async Task<UserModel> GetUser(long id)
        {
            var user = await _context.Users
                                     .Where(x => x.Id == id)
                                     .Select(x => new UserModel { Id = x.Id, FirstName = x.FirstName,LastName = x.LastName, Address = x.Address, BirthDate = x.BirthDate, ContactNumber = x.ContactNumber, Email = x.Email, Password = x.Password, Height = x.Height, Weight = x.Weight,RoleId = x.RoleId, RoleName = ((UserRole)x.RoleId).ToString() }).FirstOrDefaultAsync();

            return user;
        }
        public async Task<bool> Insert(UserModel x)
        {
            var user = new Data.Entities.Users { FirstName = x.FirstName, LastName = x.LastName, Address = x.Address, BirthDate = x.BirthDate, ContactNumber = x.ContactNumber, Email = x.Email, Password = x.Password, Height = x.Height, Weight = x.Weight, RoleId = x.RoleId };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(UserModel x)
        {
            var user = await _context.Users
                                    .Where(u => u.Id == x.Id).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Id = x.Id;
                user.FirstName = x.FirstName;
                user.LastName = x.LastName;
                user.Address = x.Address; user.BirthDate = x.BirthDate;
                user.ContactNumber = x.ContactNumber;
                user.Email = x.Email;
                user.Password = x.Password;
                user.Height = x.Height;
                user.Weight = x.Weight;
                user.RoleId = x.RoleId;
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ChangePassword(long userId, string password)
        {
            var user = await _context.Users
                                    .Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user != null)
                user.Password = password;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Delete(long id)
        {
            var user = await _context.Users
                                    .Where(x => x.Id == id).FirstOrDefaultAsync();

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<UserModel> CheckUserExists(string username, string password)
        {
            return await _context.Users
                                    .Where(x => x.Email == username && x.Password == password)
                                    .Select(x => new UserModel { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, Address = x.Address, BirthDate = x.BirthDate, ContactNumber = x.ContactNumber, Email = x.Email, Password = x.Password, Height = x.Height, Weight = x.Weight, RoleId = x.RoleId, RoleName = ((UserRole)x.RoleId).ToString() }).FirstOrDefaultAsync();
        }

        public bool CheckEmailIdExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public async Task<bool> ChangePasswordByEmail(string email, string password)
        {
            var user = await _context.Users
                                    .Where(x => x.Email == email).FirstOrDefaultAsync();

            if (user != null)
                user.Password = password;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsUserWithEmailIdExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}