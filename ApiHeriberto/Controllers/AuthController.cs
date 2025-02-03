using ApiHeriberto.CustomActionFilters;
using ApiHeriberto.Models.DTO;
using ApiHeriberto.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiHeriberto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Username,
            };
            var identityResult = await userManager.CreateAsync(identityUser, dto.Password);

            if (identityResult.Succeeded)
            {
                if (dto.Roles != null && dto.Roles.Any())
                {
                    // Add roles to this User
                    identityResult = await userManager.AddToRolesAsync(identityUser, dto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered, please login");
                    }
                }
            }
            return BadRequest("Something went wrong; " + identityResult.ToString());
        }


        [HttpPost]
        [Route("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Username);
            if (user == null)
            {
                return BadRequest("Username or password not found");
            }

            var pwdResult = await userManager.CheckPasswordAsync(user, dto.Password);

            if (!pwdResult)
            {
                return BadRequest("Username or password not found");
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles == null)
            {
                return BadRequest("User does not have any roles");
            }

            var jwt = tokenRepository.CreateJwtToken(user, roles.ToList());

            var response = new LoginResponseDto
            {
                JwtToken=jwt
            };

            return Ok(response);

        }

    }
}
