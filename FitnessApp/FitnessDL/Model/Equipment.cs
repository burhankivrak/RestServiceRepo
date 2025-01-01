using FitnessDL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [Column("status")]
        public Status Status { get; set; } = Status.available;

        public Equipment()
        {
        }

        public Equipment(int Id, string Name, string Status)
        {
            Id = Id;
            Name = Name;
            Status = Status;
        }
    }
}
