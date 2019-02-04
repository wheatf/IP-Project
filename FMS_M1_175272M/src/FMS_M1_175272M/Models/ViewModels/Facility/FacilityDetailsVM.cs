using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS_M1_175272M.Models.ViewModels.Facility
{
    public class FacilityDetailsVM
    {
        public FacilityDetailsVM() { }

        public FacilityDetailsVM(Models.Facility facility)
        {
            Facility = facility;
        }

        public FacilityDetailsVM(Models.Facility facility,
                                ICollection<SelectListItem> facilityTypes) : this(facility)
        {
            FacilityTypes = facilityTypes;
        }

        public Models.Facility Facility { get; set; }

        public ICollection<SelectListItem> FacilityTypes { get; set; }
    }
}
