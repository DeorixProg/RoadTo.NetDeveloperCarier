using Microsoft.EntityFrameworkCore;
using RoadTo.NetDeveloperCarier.Data.Entities;

namespace RoadTo.NetDeveloperCarier.Data
{
    public class PlansDBContext : DbContext
    {
        public PlansDBContext(DbContextOptions<PlansDBContext> options): base(options)
        {
        }
        public DbSet<Plan> Plans { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PetPlans;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionstring);
                
        }
    } 
}
    

