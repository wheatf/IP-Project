using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace FMS_M1_175272M.Models.ViewModels.FacilityType
{
    public class FacilityTypeCollectionVM
    {
        public FacilityTypeCollectionVM() { }

        public FacilityTypeCollectionVM(ICollection<Models.FacilityType> facilityTypes)
        {
            FacilityTypes = facilityTypes;
        }

        public ICollection<Models.FacilityType> FacilityTypes { get; set; }
    }
}
