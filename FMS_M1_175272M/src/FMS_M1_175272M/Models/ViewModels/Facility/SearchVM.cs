using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.Facility
{
    public class SearchVM
    {
        public SearchVM() { }

        public SearchVM(ICollection<Models.Facility> facilities)
        {
            Facilities = facilities;
        }

        public ICollection<Models.Facility> Facilities { get; set; }

        public string SearchFor { get; set; } = "";

        public bool ClearSearch { get; set; }
    }
}
