using CarDealership.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace CarDealership.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [EnumDataType(typeof(EngineType))]
        public EngineType EngineType { get; set; }

        [EnumDataType(typeof(TransmissionType))]
        public TransmissionType TransmissionType { get; set; }

        [ForeignKey("CarColor")]
        public int CarColorId { get; set; }
        public CarColor CarColor { get; set; }

        [EnumDataType(typeof(Enums.Region))]
        public Enums.Region Region { get; set; }

        [Range(1901, int.MaxValue)]
        public int Year { get; set; }

        public int Mileage { get; set; }
        public int Power { get; set; }

        [EnumDataType(typeof(CarType))]
        public CarType CarType { get; set; }

        [EnumDataType(typeof(Condition))]
        public Condition Condition { get; set; }

        public List<Photo> Photos { get; set; }
        public decimal Price { get; set; }
    }
}
