using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.API.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Maker { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string RentalPrice   { get; set; }
        [Required]
        public string IsAvailable { get; set; }


    }
}
