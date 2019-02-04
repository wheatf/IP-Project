using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.Booking
{
    public class SearchVM
    {
        public SearchVM() { }

        public SearchVM(ICollection<Models.Booking> bookings)
        {
            Bookings = bookings;
        }

        public ICollection<Models.Booking> Bookings { get; set; }

        public string SearchFor { get; set; } = "";

        public bool ClearSearch { get; set; }
    }
}
