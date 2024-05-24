using CarRentalApp.API.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.API.Services_layer.Implementation
{
    public interface IAuthService
    {
        Task<SignInResult> Login(SignInModel signInModel);
        Task<IdentityResult> SignUp(User obj);
    }
}