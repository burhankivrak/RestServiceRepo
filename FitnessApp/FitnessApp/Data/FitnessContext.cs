using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FitnessApp.Data
{
    public class FitnessContext : DbContext
    {
        public DbSet<Members> Members { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<FitnessProgram> Program { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define a ValueConverter to convert List<string> to a string and vice versa
            var interessesConverter = new ValueConverter<List<string>, string>(
                v => string.Join(",", v),  // Convert List<string> to a comma-separated string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  // Convert comma-separated string back to List<string>
            );

            // Apply the ValueConverter to the "Interesses" property in the "Members" entity
            modelBuilder.Entity<Members>()
                .Property(m => m.Interesses)
                .HasConversion(interessesConverter);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Server explorer daar uw string ophalen
            optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=GymTest;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"); 
            //View > other windows > package manager console
            // add-migration initCreate
            // update-database
        }
    }
}
