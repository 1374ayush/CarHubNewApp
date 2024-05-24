using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.API.Models
{
    public class UserRentalAgreement
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public int RentalAgreementId { get; set; } 


    }

}
