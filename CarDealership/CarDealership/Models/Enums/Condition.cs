using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models.Enums
{
    public enum Condition
    {
        [Display(Name = "Новa")]
        New,

        [Display(Name = "Използванa")]
        Used
    }
}
