using System;
using System.Collections.Generic;

namespace FMS_M1_175272M.Models
{
    public partial class FacilityType
    {
        public FacilityType()
        {
            Facility = new HashSet<Facility>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Facility> Facility { get; set; }
    }
}
