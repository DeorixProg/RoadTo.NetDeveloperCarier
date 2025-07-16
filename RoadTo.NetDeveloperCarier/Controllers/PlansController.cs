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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null)
            {
                return NotFound();
            }
            ViewBag.DifficultyLevels = Enum.GetValues(typeof(DifficultyLevel));
            return View(plan);
        }
        [HttpPost]
        public IActionResult Edit(Plan plan)
        {
            if (ModelState.IsValid)
            {
                var existingPlan = _context.Plans.Find(plan.Id);
                if (existingPlan != null)
                {
                    existingPlan.Name = plan.Name;
                    existingPlan.ShortDescription = plan.ShortDescription;
                    existingPlan.FullDescription = plan.FullDescription;
                    existingPlan.DifficultyLevel = plan.DifficultyLevel;
                    existingPlan.CreatedAt = DateTime.Now;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(plan);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }
        public IActionResult Delete(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
