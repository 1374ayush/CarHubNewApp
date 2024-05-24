using CarRentalApp.API.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.API.Services_layer.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<SignInResult> Login(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);

            return result;

        }

        public async Task<IdentityResult> SignUp(User obj)
        {
            var user = new IdentityUser() { UserName = obj.Email, Email = obj.Email };
            var result = await _userManager.CreateAsync(user, obj.Password);

            return result;
        }

    }
}
