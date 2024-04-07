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
        [Required(ErrorMessage = "Моля въведете име на Марката.")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Model")]
        [Required(ErrorMessage = "Моля въведете име на Модела.")]
        public int ModelId { get; set; }
        public Model Model { get; set; }

        [EnumDataType(typeof(EngineType))]
        [Required(ErrorMessage = "Моля въведете тип на Двигателя.")]
        public EngineType EngineType { get; set; }

        [EnumDataType(typeof(TransmissionType))]
        [Required(ErrorMessage = "Моля въведете тип на Трансмисията.")]
        public TransmissionType TransmissionType { get; set; }

        [ForeignKey("CarColor")]
        [Required(ErrorMessage = "Моля въведете Цвят на колата.")]
        public int CarColorId { get; set; }
        public CarColor CarColor { get; set; }

        [EnumDataType(typeof(Enums.Region))]
        [Required(ErrorMessage = "Моля въведете Област.")]
        public Enums.Region Region { get; set; }

        [Range(1901, int.MaxValue, ErrorMessage = "Годината трябва да бъде естествено число, по-голямо от 1900.")]
        [Required(ErrorMessage = "Моля въведете Година на производство.")]
        public int Year { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Пробегът трябва да бъде неотрицателно число.")]
        [Required(ErrorMessage = "Моля въведете Пробег.")]
        public int Mileage { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Мощността трябва да бъде неотрицателно число.")]
        [Required(ErrorMessage = "Моля въведете Мощност.")]
        public int Power { get; set; }

        [EnumDataType(typeof(CarType))]
        [Required(ErrorMessage = "Моля въведете тип на Купето")]
        public CarType CarType { get; set; }

        [EnumDataType(typeof(Condition))]
        [Required(ErrorMessage = "Моля въведете Състояние.")]
        public Condition Condition { get; set; }

        //[Required(ErrorMessage = "Моля въведете линк към Снимка.")]
        public List<Photo> Photos { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Цената трябва да бъде неотрицателно число.")]
        [Required(ErrorMessage = "Моля въведете Цена.")]
        public decimal Price { get; set; }
    }
}
