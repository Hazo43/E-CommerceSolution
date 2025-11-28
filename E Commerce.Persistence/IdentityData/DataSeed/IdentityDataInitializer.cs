using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;
        public IdentityDataInitializer( UserManager<ApplicationUser> userManager , 
                                        RoleManager<IdentityRole> roleManager , 
                                        ILogger<IdentityDataInitializer> logger) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // Roles او اي Element لو مفهاش اي
                if (!_roleManager.Roles.Any())
                {
                   await _roleManager.CreateAsync(new IdentityRole("Admin"));
                   await  _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                // User لو معنديش اي
                if( !_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        DisplayName = "Mohamed Tarek",
                        UserName = "MohamedTarek",
                        Email = "MohamedTarek.gmail.com",
                        PhoneNumber = "01212345678",
                    };
                    var User02 = new ApplicationUser()
                    {
                        DisplayName = "Salma Tarek",
                        UserName = "SalmaTarek",
                        Email = "SalmaTarek.gmail.com",
                        PhoneNumber = "01120304050",
                    };
                    // اللي عندنا user ل ال Create بنعمل
                    await _userManager.CreateAsync(User01 , "P@ssw0rd");
                    await _userManager.CreateAsync(User02 , "P@ssw0rd");

                    // اللي عندنا user ل ال Role بنعمل
                    await _userManager.AddToRoleAsync(User01, "Admin"); // Mohamed 
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin"); // Salma 

                }
            }
            catch(Exception ex)
            {
                _logger.LogError($" Error While Seeding Identity Database : Message = {ex.Message}");
            }
        }
    }
}
