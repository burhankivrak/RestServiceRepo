using FitnessDL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FitnessApp.Model
{
    [Table("runningsession_main", Schema = "dbo")]
    public class RunningSession
    {
        [Key]
        [Column("runningsession_id")]
        public int Id { get; set; }
        [Column("date")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }
        [Column("member_id")]
        public int MemberId { get; set; }
        [Column("duration")]
        public int Duration { get; set; }
        [Column("avg_speed")]
        public double Avg_speed { get; set; }

        [JsonIgnore]
        [ForeignKey("MemberId")]
        public Members Member { get; set; }
    }
}
