using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadTo.NetDeveloperCarier.Data;
using RoadTo.NetDeveloperCarier.Data.Entities;
using RoadTo.NetDeveloperCarier.Services;

namespace RoadTo.NetDeveloperCarier.Controllers
{
    [Authorize]
    public class PlansController : Controller
    {
        private readonly IPlansService _planService;
        private readonly UserManager<IdentityUser> _userManager;

        public PlansController(IPlansService planService, UserManager<IdentityUser> UserManager)
        {
            _planService = planService;
            _userManager = UserManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                return View(_planService.GetAllPlans());
            }

            var userId = _userManager.GetUserId(User);
            return View(_planService.GetAllPlans(userId));
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
            plan.UserId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                ViewBag.DifficultyLevels = Enum.GetValues(typeof(DifficultyLevel));
                return View(plan);
            }
            _planService.CreatePlan(plan);
            return RedirectToAction("Index");

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
        [HttpPost]
        public IActionResult IsPlanCompleted(int Id)
        {
            _planService.IsPlanCompleted(Id);
            return RedirectToAction("Index");
        }
    }
}
