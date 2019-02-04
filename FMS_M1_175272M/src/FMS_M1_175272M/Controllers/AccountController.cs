using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FMS_M1_175272M.Models.Account;
using FMS_M1_175272M.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS_M1_175272M.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult AccessDenied()
        {
            ViewBag.Title = "Access Denied";
            return View();
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Title = "Register";
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            ViewBag.Title = "Register";

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    StaffName = vm.StaffName,
                    Email = vm.Email,
                    PhoneNumber = vm.PhoneNumber,
                    UserName = vm.UserName
                };

                var result = await userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Staff");
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.
                            AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.Title = "Login";
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string returnUrl = null)
        {
            ViewBag.Title = "Login";

            if (ModelState.IsValid)
            {
                var result = await signInManager.
                    PasswordSignInAsync(vm.UserName,
                                        vm.Password,
                                        vm.RememberMe,
                                        false);
                if (result.Succeeded)
                {
                    if(returnUrl != null)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.
                        AddModelError("", "Invalid Login Attempt.");
                }
            }

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
