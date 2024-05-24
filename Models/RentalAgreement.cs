using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.API.Models
{
    public class RentalAgreement
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; } 
        public string RentStartDate { get; set; }
        public int Duration { get; set; }
        public string TotalCost { get; set; }
        public bool ReturnStatus { get; set; } 


    }
}
