using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZBUKSWALKS.API.Models.DTO;
using NZBUKSWALKS.API.Repositories;

namespace NZBUKSWALKS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }



        //POST : /api/Auth/Register
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var IdentityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);


            if (IdentityResult.Succeeded)

                // Add roles to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())

                { await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles); }


            if (IdentityResult.Succeeded)

            {
                return Ok("User was registered , please login!");
            }

            return BadRequest("something went wrong");




        }



        //POST : /api/Auth/Login

        [Route("Login")]
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] LoginResponsetDto loginRequestDto)

        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)

            {

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)

                {
                    // Get Roles for the user

                    var roles = await userManager.GetRolesAsync(user);


                    if (roles != null)
                    {
                        //create token

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto

                        { 
                              JwtToken = jwtToken

                        };

                    

                    return Ok(response);

                    }
                }


            }

            return BadRequest("username or password is incorrect");

        }


    }
}
