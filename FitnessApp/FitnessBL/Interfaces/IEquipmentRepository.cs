using FitnessApp.Model;
using FitnessDL.Enums;
using System.Diagnostics.Metrics;

namespace FitnessApp.Interface
{
    public interface IEquipmentRepository
    {
        void AddEquipment(Equipment e);
        Equipment GetEquipment(int id);
        void UpdateEquipmentStatus(int id, Status status);

        void RemoveEquipment(int id);
    }
}
