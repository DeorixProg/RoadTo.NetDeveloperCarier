using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoadTo.NetDeveloperCarier.Services;
using System.Globalization;

namespace RoadTo.NetDeveloperCarier.Controllers
{
    public class StatsController : Controller
    {
        private readonly IPlansService _planService;
        private readonly UserManager<IdentityUser> _userManager;
        public StatsController(IPlansService plansService, UserManager<IdentityUser> UserManager)
        {
            _planService = plansService;
            _userManager = UserManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                var adminPlans = _planService.GetAllPlans();
                return View(adminPlans);
            }
            var userId = _userManager.GetUserId(User);
            return View(_planService.GetAllPlans(userId));
        }
    }
}
