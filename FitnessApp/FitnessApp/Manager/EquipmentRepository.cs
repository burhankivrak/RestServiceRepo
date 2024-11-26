using FitnessApp.Interface;
using FitnessApp.Model;

namespace FitnessApp.Manager
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private Dictionary<int, Equipment> data = new Dictionary<int, Equipment>();

        public EquipmentRepository()
        {
            data.Add(1, new Equipment(1, "Loopband"));
            data.Add(2, new Equipment(2, "Loopband"));
            data.Add(3, new Equipment(3, "Cycling"));
            data.Add(4, new Equipment(4, "Cycling"));
        }

        public void AddEquipment(Equipment e)
        {
            if (!data.ContainsKey(e.Id))
                data.Add(e.Id, e);
            else
                throw new Exception("equipment already added");
        }

        public Equipment GetEquipment(int id)
        {
            if (data.ContainsKey(id))
                return data[id];
            else
                throw new Exception("equipment doesn't exist");
        }
    }
}
