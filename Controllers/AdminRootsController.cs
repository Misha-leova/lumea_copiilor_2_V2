using Microsoft.AspNetCore.Mvc;

namespace lumea_copiilor_2.Controllers
{
    public class AdminRootsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
