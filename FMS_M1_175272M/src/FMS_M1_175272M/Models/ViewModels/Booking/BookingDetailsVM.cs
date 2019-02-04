using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.Booking
{
    public class BookingDetailsVM
    {
        public BookingDetailsVM() { }

        public BookingDetailsVM(Models.Booking booking)
        {
            Booking = booking;
        }

        public BookingDetailsVM(Models.Booking booking,
                                ICollection<SelectListItem> facilities,
                                ICollection<SelectListItem> startTimes,
                                ICollection<SelectListItem> endTimes)
        {
            Booking = booking;
            Facilities = facilities;
            StartTimes = startTimes;
            EndTimes = endTimes;
        }

        public Models.Booking Booking { get; set; }

        public ICollection<SelectListItem> Facilities { get; set; }

        public ICollection<SelectListItem> StartTimes { get; set; }

        public ICollection<SelectListItem> EndTimes { get; set; }
    }
}
