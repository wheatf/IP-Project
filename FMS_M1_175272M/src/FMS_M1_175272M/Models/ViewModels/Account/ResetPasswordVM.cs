using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models.ViewModels.Account
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "User Name/Email is required!")]
        [Display(Name = "User Name or Email")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm Password doesn't match Password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
