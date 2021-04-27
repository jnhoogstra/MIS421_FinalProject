using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS421_FinalProject;
using MIS421_FinalProject.Data;

namespace MIS421_FinalProject.Views.Home
{
    [Authorize (Roles = SD.Admin)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, 
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        // GET: UserController
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.ToListAsync());

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var createUser = new CreateUserVM();

            createUser.AllRoles = _roleManager.Roles.Select(x => x.Name).ToList();

            return View(createUser);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleUserVM submitUser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = submitUser.AppUser.Email,
                    Email = submitUser.AppUser.Email,
                    EmailConfirmed = true,
                    Name = submitUser.AppUser.Name
                };
                string password = Request.Form["Password"].ToString();
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //Go get the user who was just created. This is needed because the DB generated an ID for the user
                    var userFromDB = await _userManager.FindByEmailAsync(submitUser.AppUser.Email);
                    if (userFromDB == null)
                    {
                        return NotFound();
                    }

                    //Add new roles that were checked
                    var roles = Request.Form["CheckRoles"].ToString().Split(",");
                    foreach (var role in roles)
                    {
                        _userManager.AddToRoleAsync(userFromDB, role).GetAwaiter().GetResult();
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var rollUser = new RoleUserVM();
            rollUser.AppUser = user;

            rollUser.AllRoles = _roleManager.Roles.Select(x => x.Name).ToList();

            var userRoles = await _userManager.GetRolesAsync(user);
            rollUser.UserRoles = userRoles.Select(x => x).ToList();

            return View(rollUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoleUserVM submitUser)
        {
            if (id != submitUser.AppUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbUser = await _context.ApplicationUser.FirstOrDefaultAsync(m => m.Id == id);
                    if (dbUser == null)
                    {
                        return NotFound();
                    }
                    dbUser.Email = submitUser.AppUser.Email;
                    dbUser.Name = submitUser.AppUser.Name;
                    dbUser.State = submitUser.AppUser.State;
                    _context.Update(dbUser);

                    //Handle Roles

                    //Remove all roles
                    await _userManager.RemoveFromRolesAsync(dbUser, await _userManager.GetRolesAsync(dbUser));

                    //Add new roles that were checked
                    var roles = Request.Form["CheckRoles"].ToString().Split(",");
                    foreach(var role in roles)
                    {
                        _userManager.AddToRoleAsync(submitUser.AppUser, role).GetAwaiter().GetResult();
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(submitUser.AppUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(submitUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.ApplicationUser.FindAsync(id);
            _context.ApplicationUser.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
