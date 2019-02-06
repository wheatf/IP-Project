using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Staff Name")]
        [Required(ErrorMessage = "Staff Name is required!")]
        public string StaffName { get; set; }
    }
}
