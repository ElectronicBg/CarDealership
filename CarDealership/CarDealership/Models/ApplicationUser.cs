using Microsoft.AspNetCore.Identity;

namespace Cinema.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
