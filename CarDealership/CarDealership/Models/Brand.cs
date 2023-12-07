using System.Collections.Generic;

namespace CarDealership.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public List<Model> Models { get; set; }
        public List<Car> Cars { get; set; }
    }

}
