using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models.ViewModels.Account
{
    public class AdminEditVM
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required!")]
        public string UserName { get; set; }

        [Display(Name = "Staff Name")]
        [Required(ErrorMessage = "Staff Name is required!")]
        public string StaffName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone number must be exactly 8-digit long!")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
