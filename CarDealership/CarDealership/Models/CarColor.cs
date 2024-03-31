using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class CarColor
    {
        [Key]
        public int CarColorId { get; set; }
        [Required(ErrorMessage = "Моля изберете Име!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Моля въведете Цвят!")]
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "Моля въведете валиден HEX цвят!")]
        public string Value {  get; set; }
    }
}