using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface IEquipmentRepository
    {
        void AddEquipment(Equipment e);
        Equipment GetEquipment(int id);
        void UpdateEquipmentStatus(int id, string status);
    }
}
