using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MIS421_FinalProject.Data;

namespace MIS421_FinalProject
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (!_db.Roles.Any(r => r.Name == SD.Admin))
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            }
            if (!_db.Roles.Any(r => r.Name == SD.Manager))
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Manager)).GetAwaiter().GetResult();
            }

            var userFromDB = _userManager.FindByEmailAsync("admin@gmail.com");
            if (userFromDB.Result == null)
            {
                //Disable this account in the database later on
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    Name = "Matthew Hudnall"

                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUser.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
                _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();
            }  
        }
    }
}
