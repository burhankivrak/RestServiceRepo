using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("time_slot", Schema = "dbo")]
    public class Timeslot
    {
        [Key]
        [Column("time_slot_id")]
        public int Id { get; set; }
        [Column("start_time")]
        public int StartTime { get; set; }

        [Column("end_time")]
        public int EndTime { get; set; }
        [Column("part_of_day")]
        public string PartOfDay { get; set; }

        public Timeslot()
        {
        }

        public Timeslot(int id, int startTime, int endTime, string partOfDay)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            PartOfDay = partOfDay;
        }
    }
}
