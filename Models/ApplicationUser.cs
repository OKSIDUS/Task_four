using Microsoft.AspNetCore.Identity;
using Task_four.Enums;

namespace Task_four.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {  get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime LastLogin { get; set; } = DateTime.Now;

        public UserStatus UserStatus { get; set; } = 0;
    }
}
