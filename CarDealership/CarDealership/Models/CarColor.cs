using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarColor
    {
        [Key]
        public int CarColorId { get; set; }
        public string Name { get; set; }
        public string Value {  get; set; }
    }
}