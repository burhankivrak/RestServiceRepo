using FitnessApp.Model;
using FitnessDL.Enums;
using System.Diagnostics.Metrics;

namespace FitnessApp.Interface
{
    public interface IEquipmentRepository
    {
        IEnumerable<Equipment> GetAll();
        void AddEquipment(Equipment e);
        Equipment GetEquipment(int id);
        void UpdateEquipmentStatus(int id, Status status);
        bool ExistsEquipment(int id);
        void RemoveEquipment(int id);
    }
}
