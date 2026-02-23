using BackMakerHub.DbConnection;
using BackMakerHub.DTO_s;
using BackMakerHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace BackMakerHub.Services
{
    public class userService
    {
        private readonly DbLink _context;

        public userService(DbLink context)
        {
            _context = context;
        }

        public async Task<User> AddUser(AddUserDTO u, string role)
        {
            var user = await _context.User.FirstOrDefaultAsync(existingUser => existingUser.UserName == u.UserName);
            if(user != null)
            {
                throw new Exception("Utilisateur déjà existant");
            }
            var newUser = new User
            {
                UserName = u.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(u.Password),
                Email = u.Email,
                Role = role
            };
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> ModifyUser(User u)
        {
            var user = await _context.User.AnyAsync(uId => uId.Id == u.Id);
            if(!user)
            {
                throw new Exception("Aucun n'utilisateur trouvé");
            }
            _context.User.Update(u);
            await _context.SaveChangesAsync();
            return u;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            {
                if (user == null) return false;
               
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<User> GettUserById(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new Exception("Aucun utilisateur trouvé avec cet Id");
            }
            return user;
        }
        public async Task<int> GetUserCount()
        {
            return await _context.User.CountAsync();
        }

       public async Task<User?> Authenticate(string identifier, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == identifier || u.Email == identifier);
            if(user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            return null;
        }
    }
}
