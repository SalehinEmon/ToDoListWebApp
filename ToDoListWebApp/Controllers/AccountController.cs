using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebApp.Handler;
using ToDoListWebApp.Model;

namespace ToDoListWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        public AccountController(UserManager<User> userManager,
                                  JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Login(LogInDTO loginDTO)
        {
            User userToLogIn = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (await _userManager.CheckPasswordAsync(userToLogIn, loginDTO.Password))
            {
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(userToLogIn);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new
                {
                    IsAuthSuccessful = true,
                    UserId = userToLogIn.Id,
                    UserName = userToLogIn.UserName,
                    Token = token
                }); 
            }

            return BadRequest();
        }
        [HttpPost("signup")]

        public async Task<ActionResult> SignIn([FromBody] SigninDTO signInUser)
        {
            User newUser = new User
            {
                Email = signInUser.Email,
                FirstName = signInUser.FirstName,
                LastName = signInUser.LastName,
                UserName = signInUser.Email
            };

            IdentityResult newUserResult = await _userManager
                .CreateAsync(newUser, signInUser.Password);

            if (newUserResult.Succeeded)
            {
                User registerUser = await _userManager
                        .FindByIdAsync(newUser.Email);
                return Ok(registerUser);
            }

            return BadRequest(newUserResult.Errors);
        }


        [HttpPost("removeid")]
        [Authorize]
        public async Task<ActionResult> RemoveId(EmailDTO emailDTO)
        {
            User userToDelete = await _userManager.FindByEmailAsync(emailDTO.Email);
            IdentityResult deletedUserResult = await _userManager.DeleteAsync(userToDelete);
            if (deletedUserResult.Succeeded)
            {
                return Ok(new { email = emailDTO.Email });
            }

            return BadRequest(deletedUserResult.Errors);
        }
    }
}
