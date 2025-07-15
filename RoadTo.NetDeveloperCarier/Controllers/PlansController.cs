using Microsoft.AspNetCore.Mvc;
using RoadTo.NetDeveloperCarier.Data;
using RoadTo.NetDeveloperCarier.Data.Entities;

namespace RoadTo.NetDeveloperCarier.Controllers
{

    public class PlansController : Controller
    {
        private PlansDBContext _context = new();
        public IActionResult Index()
        {
            var plans = _context.Plans.ToList();
            return View(plans);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DifficultyLevels = Enum.GetValues(typeof(DifficultyLevel));
            return View();
        }
        [HttpPost]
        public IActionResult Create(Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.CreatedAt = DateTime.Now;
                plan.IsCompleted = false;
                _context.Plans.Add(plan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plan);
        }

    }
}
