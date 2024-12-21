using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("equipment", Schema = "dbo")]
    public class Equipment
    {
        [Key]
        [Column("equipment_id")]
        public int Id { get; set; }
        [Column("device_type")]
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
