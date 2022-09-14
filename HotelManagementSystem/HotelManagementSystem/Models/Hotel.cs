using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Hotel
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string GeoLocation
        {
            get
            {
                return Latitude + "," + Longitude;
            }
        }
    }
}
