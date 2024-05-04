using handyhive_backend.Dto;
using handyhive_backend.models;
using handyhive_backend.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace handyhive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;



        public AuthController(IConfiguration configuration, IUserService userService, IAuthService authService)
        {
            _configuration = configuration;
            _userService = userService;
            _authService = authService;


        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            try
            {
                var registeredUser = await _authService.RegisterUser(request);
                return Ok(registeredUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user.the error is:" + ex);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(AuthDto request)
        {
            var response = await _authService.login(request);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var (token, message) = await _authService.RefreshToken(refreshToken);

            if (token == null)
            {
                return Unauthorized(message);
            }

            return Ok(token);
        }

    }


    }
