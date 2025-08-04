using RoadTo.NetDeveloperCarier.Data;
using RoadTo.NetDeveloperCarier.Data.Entities;
using System.Numerics;

namespace RoadTo.NetDeveloperCarier.Services
{
    public interface IPlansService
    {

        List<Plan> GetAllPlans();
        List<Plan> GetAllPlans(string userId);
        void CreatePlan(Plan plan);
        Plan GetPlanById(int id);
        void SaveChanges(Plan plan);
        void DeletePlan(int id);
        void IsPlanCompleted(int id);
    }
    public class PlansService : IPlansService
    {
        private readonly PlansDBContext _context;
        public PlansService(PlansDBContext context)
        {
            _context = context;
        }
        public List<Plan> GetAllPlans()
        {
            return _context.Plans.ToList();
        }
        public List<Plan> GetAllPlans(string userId)
        {
            return _context.Plans.Where(p => p.UserId == null || p.UserId == userId).ToList();
        }
        public void CreatePlan(Plan plan)
        {
            plan.CreatedAt = DateTime.Now;
            plan.IsCompleted = false;
            _context.Plans.Add(plan);
            _context.SaveChanges();
        }
        public Plan GetPlanById(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null)
                throw new ArgumentNullException(nameof(id), "Plan not found");
            return plan;
        }
        public void SaveChanges(Plan plan)
        {
            var existingPlan = _context.Plans.Find(plan.Id);
            if (existingPlan != null)
            {
                existingPlan.Name = plan.Name;
                existingPlan.ShortDescription = plan.ShortDescription;
                existingPlan.FullDescription = plan.FullDescription;
                existingPlan.DifficultyLevel = plan.DifficultyLevel;
                existingPlan.DeadLine = plan.DeadLine;
                existingPlan.CreatedAt = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(plan), "Plan not found");
            }
        }
        public void DeletePlan(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(id), "Plan not found");
            }
        }
        public void IsPlanCompleted(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan != null)
            {
                plan.IsCompleted = !plan.IsCompleted;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(id), "Plan not found");
            }
        }
    }
}
