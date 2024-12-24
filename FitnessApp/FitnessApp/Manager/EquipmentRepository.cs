using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;

namespace FitnessApp.Manager
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private FitnessContext context;

        public EquipmentRepository()
        {
            this.context = new FitnessContext();
        }

        public void AddEquipment(Equipment e)
        {
            if (ExistsEquipment(e.Id))
                throw new Exception("Equipment already exists");

            context.Equipment.Add(e);
            context.SaveChanges();
        }

        public Equipment GetEquipment(int id)
        {
            var e = context.Equipment.FirstOrDefault(e => e.Id == id);

            if (e == null)
                throw new Exception("Equipment doesn't exist");

            return e;
        }

        public bool ExistsEquipment(int id)
        {
            return context.Equipment.Any(e => e.Id == id);
        }

        public void UpdateEquipmentStatus(int id, string status)
        {
            var equipment = context.Equipment.FirstOrDefault(e => e.Id == id);
            if (equipment == null)
                throw new Exception($"Equipment with ID {id} doesn't exist");

            equipment.Status = status;
            context.SaveChanges();

            //Indien in onderhoud, alle reservaties met de gegeven equipmentid worden verwijderd
            if (status.Equals("UnderMaintenance", StringComparison.OrdinalIgnoreCase))
            {
                var reservations = context.Reservation.Where(r => r.EquipmentId == id).ToList();

                if (reservations.Any())
                {
                    context.Reservation.RemoveRange(reservations);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"No reservations found for equipment with ID {id}.");
                }
            }
        }

    }
}
