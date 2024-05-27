using CarRentalApp.API.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient.Server;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

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
            //3rd parameter: This parameter indicates whether the authentication session should persist after the user
            //closes their browser.

            // 4th parameter : If lockoutOnFailure is set to true, the user account will be locked out according
            // to the lockout settings configured in the Identity options (e.g., after a certain number of failed attempts within a specified time frame)
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
            //A claim is a piece of information about a user. they can store user info and roles, we can not use Object in place of claims,
            //as claims play an important role , as they can be encoded. Jwt libraries are build in such a way to use claims.

            var claims = new[]
            {
                new Claim("Email",userObj.Username)
            };

            //get the key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
           //s SymmetricSecurityKey is essential for creating a secure symmetric key used to sign and validate JWTs.

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            // HMAC - SHA256, the algorithm takes the concatenated header and payload string and a secret key to generate a signature.
            //JWT uses key for signing or encoding it , so to maintain integrity and it is using this algo to encode it.
            /* Console.WriteLine(token);
             { "alg":"HS256","typ":"JWT"}.{ "Email":"test123@gmail.com","exp":1716797403,"iss":"https://localhost:7018/","aud":"User"}*/

            //When you call WriteToken(token) on the JwtSecurityTokenHandler, it internally encodes the JWT by combining the header, payload(claims),
            //and signature(if applicable).The encoding process involves converting these components to JSON format, Base64Url encoding them, and
            //concatenating them with dots to form the final JWT string.
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }

    }
}

/* 
 * Another way of generating claim 
 * subject: new ClaimsIdentity(new[] { new Claim("payload", payloadJson) })
 */
