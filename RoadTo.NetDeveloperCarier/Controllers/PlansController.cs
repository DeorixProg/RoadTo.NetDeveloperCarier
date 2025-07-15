using Microsoft.AspNetCore.Mvc;

namespace RoadTo.NetDeveloperCarier.Controllers
{
    public class PlansController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
