using lumea_copiilor_2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lumea_copiilor_2.Controllers
{
    [Route("users")] // Base route for the controller
    public class UserPagesController : Controller
    {
        [Route("")] // Schimbat de la "" la "house"
        public  IActionResult First()
        {
            return View();
        }

        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext via constructor
        public UserPagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("market")]
        public async Task<IActionResult> Market()
        {
            // Fetch all products from the database
            var products = await _context.Products.ToListAsync();

            // Pass the products to the view
            return View(products);
        }

        [Route("news")] // Handles "/user/news"
        public IActionResult News()
        {
            // Fetch news items from the database
            var newsItems = _context.News
                .OrderByDescending(n => n.CreatedAt) // Sort by most recent first
                .ToList();

            // Pass the news items to the view
            return View(newsItems);
        }

        [Route("promotions")] // Handles "/user/promotions"
        public IActionResult Promotions()
        {
            return View();
        }

        [Route("contact")] // Handles "/user/contact"
        public IActionResult Contact()
        {
            return View();
        }

        [Route("cart")] // Handles "/user/cart"
        public IActionResult Cart()
        {
            return View();
        }

        [Route("privacy")] // Handles "/user/privacy"
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("user")] // Route for the user profile page
        public async Task<IActionResult> User()
        {
            // Fetch user information for UserInformationId = 1
            var userInformation = await _context.UserInformation
                .FirstOrDefaultAsync(u => u.UserInformationId == 1);

            if (userInformation == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            // Pass the user information to the view using ViewBag
            ViewBag.UserInformation = userInformation;

            return View();
        }
    }
}