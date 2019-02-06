using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please enter your staff name!")]
        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }

        [Required(ErrorMessage = "Please enter your email!")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone number must be exactly 8-digit long!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter an username!")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm Password doesn't match Password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
