using handyhive_backend.models;
using Microsoft.AspNetCore.Mvc;

namespace handyhive_backend.services
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> Getusers();
        public User GetUser(int id);
        public IActionResult PutUser(int id, User user);
        public User PostUser(User user);
        public Task<bool> DeleteUser(int id);
        Task<User> add(User userDto);
        Task<User> FindByUsername(string email );
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);

    }
}
