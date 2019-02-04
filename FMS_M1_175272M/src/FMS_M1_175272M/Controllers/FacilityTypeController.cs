using FMS_M1_175272M.Models;
using FMS_M1_175272M.Models.ViewModels.FacilityType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FMS_M1_175272M.Controllers
{
    [Authorize]
    public class FacilityTypeController : Controller
    {
        private readonly FacilityDBContext context;

        public FacilityTypeController(FacilityDBContext context)
        {
            this.context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "View Facility Type";

            FacilityTypeCollectionVM vm = new FacilityTypeCollectionVM(context.
                                                            FacilityType.
                                                            AsNoTracking().
                                                            ToList());

            return View(vm);
        }

        // GET: /<controller/Search
        public IActionResult Search(SearchVM vm = null)
        {
            ViewBag.Title = "Search Facility Type";

            var facilityTypes = from ft in context.FacilityType.
                                AsNoTracking()
                                select ft;

            if(vm != null && !vm.ClearSearch)
            {
                if (!string.IsNullOrWhiteSpace(vm.SearchFor))
                {
                    facilityTypes = facilityTypes.
                                        Where(facilityType => facilityType.Type.IndexOf(vm.SearchFor.Trim(),
                                                                                            StringComparison.OrdinalIgnoreCase) > -1);
                }
            }
            else
            {
                vm = new SearchVM();
            }

            vm.FacilityTypes = facilityTypes.ToList();

            return View(vm);
        }
    }
}
