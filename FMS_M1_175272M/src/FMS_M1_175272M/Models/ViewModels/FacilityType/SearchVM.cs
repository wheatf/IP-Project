using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.FacilityType
{
    public class SearchVM
    {
        public SearchVM() { }

        public SearchVM(ICollection<Models.FacilityType> facilityTypes)
        {
            FacilityTypes = facilityTypes;
        }

        public ICollection<Models.FacilityType> FacilityTypes { get; set; }

        public string SearchFor { get; set; } = "";

        public bool ClearSearch { get; set; }
    }
}
