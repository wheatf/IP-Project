using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter an username!")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
