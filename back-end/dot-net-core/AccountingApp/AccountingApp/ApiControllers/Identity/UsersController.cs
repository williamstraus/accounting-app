using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;


        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {


            List<UserDTO> UserDTOs = new List<UserDTO>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {

                var roles1 = await _userManager.GetRolesAsync(user);
                List<string> roles = new List<string>();
                foreach (var role in roles1)
                {
                    roles.Add(role);
                }


                UserDTO userDTO = new UserDTO()
                {
                    UserName = user.UserName,
                    Roles = roles,
                    Name = user.FullName,
                    UserId = user.Id

                };
                UserDTOs.Add(userDTO);
            }
            return Ok(UserDTOs);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDTO>> GetUser(string userName)
        {


            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                if (user.UserName == userName)
                {
                    var roles1 = await _userManager.GetRolesAsync(user);
                    List<string> roles = new List<string>();
                    foreach (var role in roles1)
                    {
                        roles.Add(role);
                    }
                    UserDTO userDTO = new UserDTO()
                    {
                        UserName = user.UserName,
                        Roles = roles,
                        Name = user.FullName,
                        UserId = user.Id
                    };
                    return userDTO;
                }

            }
            return NotFound();
        }



        [HttpDelete("{userName}")]
        public async Task<ActionResult<AppUser>> DeleteUser(string userName)
        {

            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                if (user.UserName == userName)
                {
                    await _userManager.DeleteAsync(user);
                    return Ok(user);
                }
            }
            return NotFound();
        }


        public class UserDTO
        {
            public string UserName { get; set; } = default!;
            public List<string> Roles { get; set; } = default!;
            public string Name { get; set; } = default!;
            public Guid UserId { get; set; }
        }
    }
}

