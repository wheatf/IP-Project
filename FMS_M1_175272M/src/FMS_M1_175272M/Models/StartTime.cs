using System;
using System.Collections.Generic;

namespace FMS_M1_175272M.Models
{
    public partial class StartTime
    {
        public StartTime()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string StartTime1 { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
