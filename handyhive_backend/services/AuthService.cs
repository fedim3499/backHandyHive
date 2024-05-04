using handyhive_backend.Dto;
using handyhive_backend.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mono.TextTemplating;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

namespace handyhive_backend.services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;


        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, IUserRepository userRepository)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userRepository = userRepository;



        }


        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void SetRefreshToken(User user, RefreshToken newRefreshToken)
        {
            var response = _httpContextAccessor.HttpContext.Response;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            // Update user's refresh token in the database
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _dbContext.SaveChanges();
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            // Update user's refresh token in the database
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            _dbContext.SaveChanges();

            return refreshToken;
        }

        public async Task<User> RegisterUser(UserDto request)
        {
            var existingUser = await _userRepository.FindByUsername(request.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("This username is already taken. Please choose a different one.");
            }
            // Generate password hash and salt
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role,
                ConfirmationCode = GenerateRandomCode(),
                IsEmailConfirmed = false,
                State=request.State,
                Country=request.Country,
                City=request.City,  
                Address=request.Address,
                Profession=request.Profession,
                Phone=request.Phone,
                Lastname=request.Lastname,
                Firstname=request.Firstname,
            };

            try
            {
                await _userRepository.add(user);

                return user;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {
                    throw new ArgumentException("This username is already taken. Please choose a different one.");
                }
                else
                {
                    throw new Exception("An error occurred while registering the user.", ex);
                }
            }
        }
        public async Task<User> login(AuthDto request)
        {
            var user = await _userRepository.FindByUsername(request.Username);
            Console.WriteLine("usernnnnnn", request);

            if (user == null)
            {
                Console.WriteLine("email ghalet");

                throw new Exception("email ghalet");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                Console.WriteLine("password ghalet");

                throw new Exception("pw ghalet") ;
            }

            string token = CreateToken(user);
            Console.WriteLine("token", token);

            var refreshToken = GenerateRefreshToken(user);
            SetRefreshToken(user, refreshToken);

            return user;
        }

        public async Task<(string Token, string Message)> RefreshToken(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

            if (user == null)
            {
                return (null, "Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return (null, "Token expired."); // Token expired
            }

            // Generate new JWT token
            string token = CreateToken(user);

            // Generate and set new refresh token
            var newRefreshToken = GenerateRefreshToken(user);
            SetRefreshToken(user, newRefreshToken);

            return (token, null); // Token refreshed successfully
        }


        public string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(00000, 99999); // Génère un nombre aléatoire entre 10000 et 99999


            return code.ToString();
        }



    }
    }
