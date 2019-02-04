using FMS_M1_175272M.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FMS_M1_175272M.Configuration
{
    public class UserSeed
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserSeed(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async void Seed()
        {
            // Roles
            string[] roleNames = { "Admin", "Staff" };

            foreach(string role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.
                        CreateAsync(new IdentityRole(role));
                }
            }


            // Users
            // Admins
            ApplicationUser[] adminUsers = 
            {
                new ApplicationUser
                {
                    UserName = "maryt",
                    StaffName = "Mary Tan",
                    Email = "marytan@fms.com",
                    PhoneNumber = "61153589"
                }
            };

            foreach(ApplicationUser user in adminUsers)
            {
                if(await userManager.
                    FindByNameAsync(user.UserName) == null)
                {
                    IdentityResult result = await userManager.
                        CreateAsync(user, "Pa$$w0rd");
                    if(result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }

            // Staffs
            ApplicationUser[] staffUsers =
            {
                new ApplicationUser
                {
                    UserName = "peterc",
                    StaffName = "Peter Chew",
                    Email = "peterchew@fms.com",
                    PhoneNumber = "61158935"
                },

                new ApplicationUser
                {
                    UserName = "davidp",
                    StaffName = "David Pau",
                    Email = "davidpau@fms.com",
                    PhoneNumber = "61158798"
                }
            };

            foreach(ApplicationUser user in staffUsers)
            {
                if(await userManager.
                    FindByNameAsync(user.UserName) == null)
                {
                    IdentityResult result = await userManager.
                        CreateAsync(user, "Pa$$w0rd");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Staff");
                    }
                }
            }
        }
    }
}
