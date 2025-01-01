using FitnessApp.Model;
using FitnessDL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FitnessApp.Data
{
    public class FitnessContext : DbContext
    {
        public DbSet<Members> Members { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<FitnessProgram> Program { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<ReservationTimeslot> ReservationTimeslot { get; set; }
        public DbSet<Timeslot> Timeslot { get; set; }
        public DbSet<ProgramMembers> ProgramMembers { get; set; }
        public DbSet<CyclingSession> CyclingSession { get; set; }
        public DbSet<RunningSession> RunningSession { get; set; }
        public DbSet<RunningSessionDetail> RunningSessionDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definieer een ValueConverter om een List<string> te converteren naar een string en vice versa
            var interessesConverter = new ValueConverter<List<string>, string>(
                v => string.Join(",", v),  // Converteer List<string> naar een komma-gescheiden string
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()  // Converteer een komma-gescheiden string terug naar een List<string>
            );

            // Pas de ValueConverter toe op de "Interesses" eigenschap in de "Members" entiteit
            modelBuilder.Entity<Members>()
                .Property(m => m.Interesses)
                .HasConversion(interessesConverter);

            modelBuilder.Entity<Members>()
            .Property(m => m.TypeKlant)
            .HasConversion(new EnumToStringConverter<KlantType>());

            modelBuilder.Entity<Equipment>()
            .Property(e => e.Status)
            .HasConversion(new EnumToStringConverter<Status>());
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
