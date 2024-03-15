using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models.Enums
{
    public enum EngineType
    {
        [Display(Name = "Бензин")]
        Petrol,

        [Display(Name = "Дизел")]
        Diesel,

        [Display(Name = "Хибрид")]
        Hybrid,

        [Display(Name = "Електрически")]
        Electric
    }
}
