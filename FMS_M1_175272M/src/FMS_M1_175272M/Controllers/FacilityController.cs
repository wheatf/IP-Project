using FMS_M1_175272M.Models;
using FMS_M1_175272M.Models.ViewModels.Facility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS_M1_175272M.Controllers
{
    [Authorize]
    public class FacilityController : Controller
    {
        private readonly FacilityDBContext context;

        public FacilityController(FacilityDBContext context)
        {
            this.context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Title = "View Facility";

            FacilityCollectionVM vm = new FacilityCollectionVM(context.
                                            Facility.
                                            Include(facility => facility.Type).
                                            AsNoTracking().
                                            ToList());

            return View(vm);
        }

        // GET: /<controller>/Add
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Facility";

            FacilityDetailsVM vm = new FacilityDetailsVM(new Facility(),
                                                        getFacilityTypes());
            vm.Facility.Level = 1;
            vm.Facility.Capacity = 1;

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(FacilityDetailsVM vm)
        {
            ViewBag.Title = "Add Facility";

            if (ModelState.IsValid)
            {
                try
                {
                    context.Facility.Add(vm.Facility);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to connect to database. Try again later.");
                }
            }

            vm.FacilityTypes = getFacilityTypes();

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Title = "Edit Facility";

            if(id != null)
            {
                var facilityToEdit = await context.
                                            Facility.
                                            //Include(facility => facility.Type).
                                            SingleOrDefaultAsync(facility => facility.Id == id);

                if(facilityToEdit != null)
                {
                    FacilityDetailsVM vm = new FacilityDetailsVM(facilityToEdit, 
                                            getFacilityTypes());

                    return View(vm);
                }

            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(FacilityDetailsVM vm)
        {
            ViewBag.Title = "Edit Facility";

            if (ModelState.IsValid)
            {
                try
                {
                    context.Facility.Update(vm.Facility);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to connect to database. Try again later.");
                }
            }

            vm.FacilityTypes = getFacilityTypes();

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Title = "Delete Facility";

            if(id != null)
            {
                var facilityToDelete = await context.
                                                Facility.
                                                Include(facility => facility.Type).
                                                SingleOrDefaultAsync(facility => facility.Id == id);

                if(facilityToDelete != null)
                {
                    FacilityDetailsVM vm = new FacilityDetailsVM(facilityToDelete);

                    return View(vm);
                }
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Title = "Delete Facility";

            var facilityToDelete = await context.
                                            Facility.
                                            Include(facility => facility.Type).
                                            SingleOrDefaultAsync(facility => facility.Id == id);

            try
            {
                context.Facility.Remove(facilityToDelete);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to connect to database. Try again later.");
            }

            return View(new FacilityDetailsVM(facilityToDelete));
        }

        // /<controller>/Search
        public IActionResult Search(SearchVM vm = null)
        {
            ViewBag.Title = "Search Facility";

            var facilities = context.
                                Facility.
                                Include(facility => facility.Type).
                                AsNoTracking();
                                

            if (vm != null && !vm.ClearSearch)
            {
                if (!string.IsNullOrWhiteSpace(vm.SearchFor))
                {
                    facilities = facilities.
                                    Where(facility => facility.Type.Type.IndexOf(vm.SearchFor.Trim(),
                                                                                StringComparison.OrdinalIgnoreCase) > -1);
                }
            }
            else
            {
                vm = new SearchVM();
            }

            vm.Facilities = facilities.ToList();

            return View(vm);
        }

        // HACK: Repopulate list of facility types. Unknown as to why it keeps dropping after every time a viewmodel gets send back.
        private ICollection<SelectListItem> getFacilityTypes()
        {
            return context.FacilityType.
                    Select(type => new SelectListItem()
                    {
                        Value = type.Id.ToString(),
                        Text = type.Type
                    })
                    .ToList();
        }
    }
}
