using Microsoft.AspNetCore.Identity;

namespace TodoListApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }
    }
}