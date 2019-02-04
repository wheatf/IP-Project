using System;
using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models
{
    public partial class Booking
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }

        [Display(Name = "Facility")]
        [Required(ErrorMessage = "Facility is required!")]
        public int FacilityId { get; set; }

        [Display(Name = "Booking Date")]
        [DataType(DataType.Date, ErrorMessage = "Booking Date must be a date!")]
        [Required(ErrorMessage = "Booking Date is required!")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "Start Time is required!")]
        public int StartTimeId { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "End Time is required!")]
        public int EndTimeId { get; set; }

        [Display(Name = "Purpose")]
        [Required(ErrorMessage = "Purpose is required!")]
        public string Purpose { get; set; }

        public virtual EndTime EndTime { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual StartTime StartTime { get; set; }
    }
}
