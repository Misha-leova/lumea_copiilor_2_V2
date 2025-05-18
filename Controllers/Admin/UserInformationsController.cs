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

    public class UserInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserInformations
        public async Task<IActionResult> Index()
        {
            var userInformations = await _context.UserInformation.ToListAsync();
            return View("~/Views/Admin/UserInformations/Index.cshtml", userInformations);
        }

        // GET: UserInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation
                .FirstOrDefaultAsync(m => m.UserInformationId == id);
            if (userInformation == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/UserInformations/Details.cshtml", userInformation);
        }

        // GET: UserInformations/Create
        public IActionResult Create()
        {
            return View("~/Views/Admin/UserInformations/Create.cshtml");
        }

        // POST: UserInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserInformationId,FullName,Email,PhoneNumber,Address,Birthdate,SubscribedToNewsletter,CreatedAt,UpdatedAt,City,Country,PostalCode")] UserInformation userInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/UserInformations/Create.cshtml", userInformation);
        }

        // GET: UserInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation.FindAsync(id);
            if (userInformation == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/UserInformations/Edit.cshtml", userInformation);
        }

        // POST: UserInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserInformationId,FullName,Email,PhoneNumber,Address,Birthdate,SubscribedToNewsletter,CreatedAt,UpdatedAt,City,Country,PostalCode")] UserInformation userInformation)
        {
            if (id != userInformation.UserInformationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInformationExists(userInformation.UserInformationId))
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
            return View("~/Views/Admin/UserInformations/Edit.cshtml", userInformation);
        }

        // GET: UserInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformation
                .FirstOrDefaultAsync(m => m.UserInformationId == id);
            if (userInformation == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/UserInformations/Delete.cshtml", userInformation);
        }

        // POST: UserInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userInformation = await _context.UserInformation.FindAsync(id);
            if (userInformation != null)
            {
                _context.UserInformation.Remove(userInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInformationExists(int id)
        {
            return _context.UserInformation.Any(e => e.UserInformationId == id);
        }
    }
}