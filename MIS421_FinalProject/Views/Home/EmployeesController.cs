using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MIS421_FinalProject.Data;
using MIS421_FinalProject.Models;

namespace MIS421_FinalProject.Views.Home
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }


        [Authorize(Roles = "Admin, Manager")]
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.empID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [Authorize(Roles = "Admin, Manager")]
        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("empID,empFName,empLName,empBDate,empSSN,empGender,empMaritalStatus,ProfilePic,empPhone,empEmail,empAddress,empCity,empCountry,empState,empZip,empHireDate,empEndDate,empActive,empSalary,deptID")] Employee employee, IFormFile ProfilePic)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePic != null && ProfilePic.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await ProfilePic.CopyToAsync(memoryStream);
                    employee.ProfilePic = memoryStream.ToArray();
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [Authorize]
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FirstOrDefaultAsync(m => m.empID == id);
            if (employee == null)
            {
                return NotFound();
            }
            if (((User.IsInRole(SD.Admin)) || (User.IsInRole(SD.Manager))) || (email == employee.empEmail))
            {
                return View (employee);
            }
            else
            {
                return Forbid();
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("empID,empFName,empLName,empBDate,empSSN,empGender,empMaritalStatus,ProfilePic,empPhone,empEmail,empAddress,empCity,empCountry,empState,empZip,empHireDate,empEndDate,empActive,empSalary,deptID")] Employee employee, IFormFile ProfilePic)
        {
            if (id != employee.empID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ProfilePic != null && ProfilePic.Length > 0)
                    {
                        var memoryStream = new MemoryStream();
                        await ProfilePic.CopyToAsync(memoryStream);
                        employee.ProfilePic = memoryStream.ToArray();
                    }
                    else
                    {
                        Employee existingpic = _context.Employee.AsNoTracking().FirstOrDefault(m => m.empID == id);
                        if (existingpic != null)
                        {
                            employee.ProfilePic = existingpic.ProfilePic;
                        }
                    }

                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.empID))
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
            return View(employee);
        }

        [Authorize(Roles = "Admin, Manager")]
        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.empID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.empID == id);
        }
    }
}
