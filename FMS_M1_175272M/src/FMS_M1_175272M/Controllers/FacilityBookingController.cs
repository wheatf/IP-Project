using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS_M1_175272M.Models;
using FMS_M1_175272M.Models.ViewModels.Booking;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FMS_M1_175272M.Models.Account;
using Microsoft.AspNetCore.Authorization;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS_M1_175272M.Controllers
{
    [Authorize]
    public class FacilityBookingController : Controller
    {
        private readonly FacilityDBContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public FacilityBookingController(FacilityDBContext context,
                                        UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.Title = "View Booking";

            var user = await userManager.GetUserAsync(User);

            ViewBag.LocationSort = string.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
            ViewBag.BookingDate = sortOrder == "bookingDate_asc" ? "bookingDate_desc" : "bookingDate_asc";

            var bookings = context.
                            Booking.
                            Include(booking => booking.Facility).
                            Include(booking => booking.StartTime).
                            Include(booking => booking.EndTime).
                            Where(booking => booking.StaffName == user.StaffName).
                            AsNoTracking();
            switch (sortOrder)
            {
                case "location_desc":
                    bookings = bookings.OrderByDescending(booking => booking.Facility.Location);
                    break;

                case "bookingDate_asc":
                    bookings = bookings.OrderBy(booking => booking.BookingDate);
                    break;

                case "bookingDate_desc":
                    bookings = bookings.OrderByDescending(booking => booking.BookingDate);
                    break;

                default:
                    bookings = bookings.OrderBy(booking => booking.Facility.Location);
                    break;
            }

            return View(new BookingCollectionVM(bookings.ToList()));
        }

        [HttpGet]
        public IActionResult Booking()
        {
            ViewBag.Title = "Make Booking";

            BookingDetailsVM vm = new BookingDetailsVM(new Booking(),
                                                        getFacilities(),
                                                        getStartTimes(),
                                                        getEndTimes());

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Booking(BookingDetailsVM vm)
        {
            ViewBag.Title = "Make Booking";

            if (vm.Booking.StartTimeId > vm.Booking.EndTimeId)
            {
                ModelState.AddModelError("", "Start Time must be before End Time!");

                vm.Facilities = getFacilities();
                vm.StartTimes = getStartTimes();
                vm.EndTimes = getEndTimes();
                return View(vm);
            }

            if (vm.Booking.BookingDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Booking Date cannot be before today!");

                vm.Facilities = getFacilities();
                vm.StartTimes = getStartTimes();
                vm.EndTimes = getEndTimes();
                return View(vm);
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                try
                {
                    vm.Booking.StaffName = user.StaffName;

                    context.Booking.Add(vm.Booking);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to connect to database. Try again later.");
                }
            }

            vm.Facilities = getFacilities();
            vm.StartTimes = getStartTimes();
            vm.EndTimes = getEndTimes();

            return View(vm);
        }

        public IActionResult Search(SearchVM vm, string sortOrder)
        {
            ViewBag.Title = "Search Bookings";

            ViewBag.LocationSort = string.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
            ViewBag.BookingDate = sortOrder == "bookingDate_asc" ? "bookingDate_desc" : "bookingDate_asc";

            var bookings = context.
                            Booking.
                            Include(booking => booking.Facility).
                            Include(booking => booking.StartTime).
                            Include(booking => booking.EndTime).
                            AsNoTracking();

            if (vm != null && !vm.ClearSearch)
            {
                if (!string.IsNullOrWhiteSpace(vm.SearchFor))
                {
                    bookings = bookings.
                                Where(booking => booking.StaffName.IndexOf(vm.SearchFor.Trim(),
                                                                            StringComparison.OrdinalIgnoreCase) > -1);
                }
            }
            else
            {
                vm = new SearchVM();
            }

            switch (sortOrder)
            {
                case "location_desc":
                    bookings = bookings.OrderByDescending(booking => booking.Facility.Location);
                    break;

                case "bookingDate_asc":
                    bookings = bookings.OrderBy(booking => booking.BookingDate);
                    break;

                case "bookingDate_desc":
                    bookings = bookings.OrderByDescending(booking => booking.BookingDate);
                    break;

                default:
                    bookings = bookings.OrderBy(booking => booking.Facility.Location);
                    break;
            }

            vm.Bookings = bookings.ToList();

            return View(vm);
        }

        private ICollection<SelectListItem> getFacilities()
        {
            return context.Facility.
                    Select(facility => new SelectListItem()
                    {
                        Value = facility.Id.ToString(),
                        Text = facility.Description
                    })
                    .ToList();
        }

        private ICollection<SelectListItem> getStartTimes()
        {
            return context.StartTime.
                    Select(time => new SelectListItem()
                    {
                        Value = time.Id.ToString(),
                        Text = time.StartTime1
                    })
                    .ToList();
        }

        private ICollection<SelectListItem> getEndTimes()
        {
            return context.EndTime.
                    Select(time => new SelectListItem()
                    {
                        Value = time.Id.ToString(),
                        Text = time.EndTime1
                    })
                    .ToList();
        }
    }
}
