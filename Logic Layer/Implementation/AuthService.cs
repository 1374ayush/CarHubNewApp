using CarRentalApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalApp.API.Services_layer.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configureation)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configureation;
        }
        public async Task<string> Login(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);

            if (result.Succeeded)
            {
                string token = GenerateToken(signInModel);
                return token;
            }
            return null;

        }

        public async Task<IdentityResult> SignUp(User obj)
        {
            var user = new IdentityUser() { UserName = obj.Email, Email = obj.Email };
            var result = await _userManager.CreateAsync(user, obj.Password);

            return result;
        }

        private string GenerateToken(SignInModel userObj)
        {
            //create claims
            var claims = new[]
            {
                new Claim("Email",userObj.Username)
            };

            //get the key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenAsString;
        }

    }
}
