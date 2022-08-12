using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Identity;
using DTO.Identity;
using Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace AccountingApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;


        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"WebApi login. User {dto.Email} not found!");
                return NotFound();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJWT(
                    claimsPrincipal.Claims
                        .Append(new Claim(ClaimTypes.Name, appUser.FullName)),
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"WebApi login. User {appUser.Email} logged in.");
                return Ok(new JwtResponseDTO()
                {
                    Token = jwt,
                    Status = $"User {appUser.Email} logged in.",
                    Name = appUser.FullName
                });
            }

            _logger.LogInformation($"WebApi login. User {appUser.Email} failed login attempt!");
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {

            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogInformation($"WebApi register. User {dto.Email} already registered!");
                return NotFound();
            }

            appUser = new AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.Name
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("User creation failed!");
            }
            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (!roleResult.Succeeded)
            {
                return BadRequest("Failed to assign a role to user!");
            }
            _logger.LogInformation($"User {appUser.Email} created a new account with password.");
            return Ok();

        }


        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
        }
    }
}

