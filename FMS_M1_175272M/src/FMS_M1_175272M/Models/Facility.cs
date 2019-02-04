using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS_M1_175272M.Models
{
    public partial class Facility
    {
        public Facility()
        {
            Booking = new HashSet<Booking>();
        }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is required!")]
        [StringLength(10, ErrorMessage = "Location must be less than 10 characters!")]
        public string Location { get; set; }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "Level is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Level must be 1 or above!")]
        public int Level { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required!")]
        public int TypeId { get; set; }

        [Display(Name = "Abr")]
        [Required(ErrorMessage = "Aberration is required!")]
        [StringLength(5, ErrorMessage = "Aberration must be less than 5 characters!")]
        public string Abr { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Display(Name = "Capacity")]
        [Required(ErrorMessage = "Capacity is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be 1 or above!")]
        public int Capacity { get; set; }

        [Display(Name = "Details")]
        [Required(ErrorMessage = "Details is required!")]
        public string Details { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
        public virtual FacilityType Type { get; set; }
    }
}
