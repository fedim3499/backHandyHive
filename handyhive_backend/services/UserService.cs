using handyhive_backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace handyhive_backend.services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _UserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _UserRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> DeleteUser(int id)
        {
            return _UserRepository.DeleteUser(id);
        }

        public User GetUser(int id)
        {
            return _UserRepository.GetUser(id); 
        }

        public Task<IEnumerable<User>> Getusers()
        {
            return _UserRepository.Getusers();
        }

        public User PostUser(User user)
        {
            throw new NotImplementedException();
        }

        public IActionResult PutUser(int id, User user)
        {
            throw new NotImplementedException();
        }

      
        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
