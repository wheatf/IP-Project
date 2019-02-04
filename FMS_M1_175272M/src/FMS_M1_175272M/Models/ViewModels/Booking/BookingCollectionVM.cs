using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.Booking
{
    public class BookingCollectionVM
    {
        public BookingCollectionVM() { }

        public BookingCollectionVM(ICollection<Models.Booking> bookings)
        {
            Bookings = bookings;
        }

        public ICollection<Models.Booking> Bookings { get; set; }
    }
}
