using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }

        [ForeignKey("Brand")]
        [Required(ErrorMessage = "Моля изберете ID на марка!")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required(ErrorMessage = "Моля изберете име!")]
        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
