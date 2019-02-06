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

                    return View("RegisterSuccess");
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


        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewBag.Title = "Reset Password";

            return View(new ResetPasswordVM());
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
        {
            ViewBag.Title = "Reset Password";

            if (ModelState.IsValid)
            {
                ApplicationUser user;

                if(await userManager.FindByEmailAsync(vm.Identification) != null)
                {
                    user = await userManager.FindByEmailAsync(vm.Identification);
                }
                else if(await userManager.Users.FirstOrDefaultAsync(u => u.UserName == vm.Identification) != null)
                {
                    user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == vm.Identification);
                }
                else
                {
                    ModelState.AddModelError("", "User Name/Email cannot be found! Try again.");
                    return View(vm);
                }

                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, resetToken, vm.Password);

                if (result.Succeeded)
                {
                    return View("ResetPasswordSuccess");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowAllUsers()
        {
            ViewBag.Title = "List of Users";

            UserCollectionVM vm = new UserCollectionVM(userManager.Users.
                                                        Include(user => user.Roles).
                                                        AsNoTracking().
                                                        ToList());

            return View(vm);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AdminEdit(string id)
        {
            ViewBag.Title = "Edit User";

            if(id != null)
            {
                var userToEdit = await userManager.FindByIdAsync(id);

                if(userToEdit != null)
                {
                    AdminEditVM vm = new AdminEditVM();
                    vm.Id = userToEdit.Id;
                    vm.UserName = userToEdit.UserName;
                    vm.StaffName = userToEdit.StaffName;
                    vm.Email = userToEdit.Email;
                    vm.PhoneNumber = userToEdit.PhoneNumber;
                    
                    if(await userManager.IsInRoleAsync(userToEdit, "Staff"))
                    {
                        vm.Role = "Staff";
                    }
                    else
                    {
                        vm.Role = "Admin";
                    }

                    return View(vm);
                }
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AdminEdit(AdminEditVM vm)
        {
            ViewBag.Title = "Edit User";

            if (ModelState.IsValid)
            {
                var userToEdit = await userManager.FindByIdAsync(vm.Id);
                userToEdit.UserName = vm.UserName;
                userToEdit.StaffName = vm.StaffName;
                userToEdit.Email = vm.Email;
                userToEdit.PhoneNumber = vm.PhoneNumber;

                var result = await userManager.UpdateAsync(userToEdit);

                if (result.Succeeded)
                {
                    return RedirectToAction("ShowAllUsers", "Account");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            if(id != null)
            {
                var userToMakeAdmin = await userManager.FindByIdAsync(id);

                if(userToMakeAdmin != null && await userManager.IsInRoleAsync(userToMakeAdmin, "Staff"))
                {
                    await userManager.RemoveFromRoleAsync(userToMakeAdmin, "Staff");
                    await userManager.AddToRoleAsync(userToMakeAdmin, "Admin");

                    return RedirectToAction("ShowAllUsers", "Account");
                }
            }

            return NotFound();
        }
    }
}
