using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FitnessApp.Model
{
    [Table("runningsession_detail", Schema = "dbo")]
    public class RunningSessionDetail
    {
        [Key]
        [Column("seq_nr")]
        public int Id { get; set; }
        [Column("runningsession_id")]
        public int RunningSessionId { get; set; }
        [Column("interval_time")]
        public int Interval_Time { get; set; }
        [Column("interval_speed")]
        public double Interval_Speed { get; set; }

        [JsonIgnore]
        [ForeignKey("RunningSessionId")]
        public RunningSession RunningSession { get; set; }
    }
}
