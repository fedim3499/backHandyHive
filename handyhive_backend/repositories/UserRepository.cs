using handyhive_backend.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace handyhive_backend.services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) {
            _context = context;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public User GetUser(int id)
        {
            return _context.users.Find(id);
        }

        public async Task<IEnumerable<User>> Getusers()
        {
           return await _context.users.ToListAsync();      
        }

        public User PostUser(User user)
        {
            throw new NotImplementedException();
        }

        public IActionResult PutUser(int id, User user)
        {
            throw new NotImplementedException();
        }
        public async Task<User> add(User user)
        {
            try
            {
                // Add the user entity to the DbContext
                _context.users.Add(user);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the user entity as part of the response
                return user;

            }
            catch
            {
                return null;
            }


        }

        public async Task<User> FindByUsername(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
