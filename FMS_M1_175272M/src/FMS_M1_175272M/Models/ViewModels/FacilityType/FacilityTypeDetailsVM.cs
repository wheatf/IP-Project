using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_M1_175272M.Models.ViewModels.FacilityType
{
    public class FacilityTypeDetailsVM
    {
        public FacilityTypeDetailsVM() { }

        public FacilityTypeDetailsVM(Models.FacilityType facilityType)
        {
            FacilityType = facilityType;
        }

        public Models.FacilityType FacilityType { get; set; }
    }
}
