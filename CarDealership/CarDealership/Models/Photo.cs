using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [ForeignKey("CarId")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        public string Url { get; set; }
    }
}
