using lumea_copiilor_2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lumea_copiilor_2.Controllers
{
    [Route("")] // Base route for the controller
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

        [Route("cont")] // Handles "/user/cart"
        public IActionResult cont()
        {
            return View();
        }

        [Route("market")]
        public async Task<IActionResult> Market()
        {
            // Fetch all products from the database
            var products = await _context.Products.ToListAsync();

            // Pass the products to the view
            return View(products);
        }

        [Route("news")]
public IActionResult News()
{
    // Fetch only PUBLISHED news items from the database, ordered by most recent first
    var publishedNews = _context.News
        .Where(n => n.IsPublished)  // Filter only published news
        .OrderByDescending(n => n.PublishedAt ?? n.CreatedAt) // Use PublishedAt if available, otherwise CreatedAt
        .ToList();

    return View(publishedNews);
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