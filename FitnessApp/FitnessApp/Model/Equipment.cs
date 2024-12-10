using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Model
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Equipment()
        {
        }

        public Equipment(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
