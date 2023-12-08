using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
