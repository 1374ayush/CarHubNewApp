using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.API.Models
{
    public class CarInspection
    {
        [Key]
        public int Id { get; set; }
        
        public int  RentalAgreementId { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
