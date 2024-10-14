using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository.Identity.DataSeed
{
    public class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "ayanada780@gmail.com",
                    DisplayName = "Aya Nada",
                    UserName = "ayanada",
                    PhoneNumber = "01128511504",
                    Address = new Address()
                    {
                        FName = "Aya",
                        LName = "Nada",
                        City = "Alex",
                        Street = "Street",
                        Country = "Egypt"
                    }
                };

                await _userManager.CreateAsync(user, password: "P@ssw0rd");
            }
        }
    }
}
