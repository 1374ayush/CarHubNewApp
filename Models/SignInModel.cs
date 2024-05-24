using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.API.Models
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
