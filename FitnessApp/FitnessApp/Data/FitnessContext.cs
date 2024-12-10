using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Data
{
    public class FitnessContext : DbContext
    {
        public DbSet<Members> Members { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<FitnessProgram> Program { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Server explorer daar uw string ophalen
            optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=Books2024;Integrated Security=True;Trust Server Certificate=True"); 
            //View > other windows > package manager console
            // add-migration initCreate
            // update-database
        }
    }
}
