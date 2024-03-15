using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models.Enums
{
    public enum CarType
    {
        [Display(Name = "Седан")]
        Sedan,

        [Display(Name = "Хечбек")]
        Hatchback,

        [Display(Name = "Микро")]
        Micro,

        [Display(Name = "SUV")]
        SUV
    }
}
