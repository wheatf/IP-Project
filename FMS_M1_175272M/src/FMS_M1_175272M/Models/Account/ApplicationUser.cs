using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FMS_M1_175272M.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        public string StaffName { get; set; }
    }
}
