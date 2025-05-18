using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lumea_copiilor_2.Data;
using lumea_copiilor_2.Models;
using Microsoft.AspNetCore.Authorization;

namespace lumea_copiilor_2.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
       
       
        public AdminUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminUsers
        public async Task<IActionResult> Index()
        {
            var adminUsers = await _context.AdminUsers.ToListAsync();
            return View("~/Views/Admin/AdminUsers/Index.cshtml", adminUsers);
        }

        // GET: AdminUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (adminUser == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/AdminUsers/Details.cshtml", adminUser);
        }

        // GET: AdminUsers/Create
        public IActionResult Create()
        {
            return View("~/Views/Admin/AdminUsers/Create.cshtml");
        }

        // POST: AdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,Username,PasswordHash,Email,FullName,CreatedAt,LastLogin")] AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/AdminUsers/Create.cshtml", adminUser);
        }

        // GET: AdminUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/AdminUsers/Edit.cshtml", adminUser);
        }

        // POST: AdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,Username,PasswordHash,Email,FullName,CreatedAt,LastLogin")] AdminUser adminUser)
        {
            if (id != adminUser.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminUserExists(adminUser.AdminId))
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
            return View("~/Views/Admin/AdminUsers/Edit.cshtml", adminUser);
        }

        // GET: AdminUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminUser = await _context.AdminUsers
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (adminUser == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/AdminUsers/Delete.cshtml", adminUser);
        }

        // POST: AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser != null)
            {
                _context.AdminUsers.Remove(adminUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminUserExists(int id)
        {
            return _context.AdminUsers.Any(e => e.AdminId == id);
        }
    }
}