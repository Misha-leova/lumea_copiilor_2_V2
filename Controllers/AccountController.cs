using Microsoft.AspNetCore.Mvc;
using lumea_copiilor_2.Models;
using lumea_copiilor_2.Data;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

namespace lumea_copiilor_2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the admin user by username
                var adminUser = _context.AdminUsers
                    .FirstOrDefault(u => u.Username == model.Username);

                if (adminUser != null && BCrypt.Net.BCrypt.Verify(model.Password, adminUser.PasswordHash))
                {
                    // Create claims for the authenticated user
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, adminUser.Username),
                        new Claim(ClaimTypes.Role, "Admin") // Assign the "Admin" role
                    };

                    // Create a claims identity and sign in the user
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // Redirect to the Admin dashboard (Admin/Index)
                    return RedirectToAction("Index", "AdminUsers");
                }

                // If no user is found or password is incorrect, show an error
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If the model state is invalid, return to the login page with errors
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("First", "UserPages");
        }
    }
}