using CarRentalApp.API.Models;
using CarRentalApp.API.Services_layer.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn( SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result =await _authService.Login(signInModel);
                if (result.Succeeded)
                {
                    return Ok(new { username = signInModel.Username });
                }
            }
            ModelState.AddModelError("", "Invalid Credentials");
            return BadRequest(ModelState);

        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(User obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.SignUp(obj);

                //if user created successfully
                if (result.Succeeded)
                {
                    return Ok(new { message = "Signup success" });
                }
                else
                {
                    return BadRequest(new { message = result.Errors });
                }
            }
            ModelState.AddModelError("", "invalid");
            return BadRequest(ModelState);
        }

    }
}
