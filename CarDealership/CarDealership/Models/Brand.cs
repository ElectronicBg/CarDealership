using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Моля изберете Име!")]
        public string Name { get; set; }
        public List<Model> Models { get; set; }
        public List<Car> Cars { get; set; }
    }

}
