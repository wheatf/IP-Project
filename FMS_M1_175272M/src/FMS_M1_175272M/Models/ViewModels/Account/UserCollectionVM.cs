using FMS_M1_175272M.Models.Account;
using System.Collections.Generic;

namespace FMS_M1_175272M.Models.ViewModels.Account
{
    public class UserCollectionVM
    {
        public UserCollectionVM() { }

        public UserCollectionVM(ICollection<ApplicationUser> users)
        {
            Users = users;
        }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
