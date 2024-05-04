using handyhive_backend.models;
using Microsoft.AspNetCore.Mvc;

namespace handyhive_backend.services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> Getusers();
        public User GetUser(int id);
        public IActionResult PutUser(int id, User user);
        public User PostUser(User user);
        public Task<bool> DeleteUser(int id);
        string GetMyName();

    }
}
