using Microsoft.AspNetCore.Mvc;
using RoadTo.NetDeveloperCarier.Data;
using RoadTo.NetDeveloperCarier.Data.Entities;
using RoadTo.NetDeveloperCarier.Services;

namespace RoadTo.NetDeveloperCarier.Controllers
{

    public class PlansController : Controller
    {
        private readonly IPlansService _planService;

        public PlansController(IPlansService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_planService.GetAllPlans());
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
                _planService.CreatePlan(plan);
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.DifficultyLevels = Enum.GetValues(typeof(DifficultyLevel));
            return View(_planService.GetPlanById(id));
        }

        [HttpPost]
        public IActionResult Edit(Plan plan)
        {
            if (ModelState.IsValid)
            {
                ViewBag.DifficultyLevels = Enum.GetValues(typeof(DifficultyLevel));
                _planService.SaveChanges(plan);
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(_planService.GetPlanById(id));
        }

        public IActionResult Delete(int id)
        {
            _planService.DeletePlan(id);
            return RedirectToAction("Index");
        }
    }
}
