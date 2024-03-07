using Microsoft.AspNetCore.Identity;

namespace CarDealership.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
