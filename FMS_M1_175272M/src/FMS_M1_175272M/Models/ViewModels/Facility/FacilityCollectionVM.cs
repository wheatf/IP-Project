using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.Facility
{
    public class FacilityCollectionVM
    {
        public FacilityCollectionVM() { }

        public FacilityCollectionVM(ICollection<Models.Facility> facilities)
        {
            Facilities = facilities;
        }

        public ICollection<Models.Facility> Facilities { get; set; }
    }
}
