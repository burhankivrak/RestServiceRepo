using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("cyclingsession", Schema = "dbo")]
    public class CyclingSession
    {
        [Key]
        [Column("cyclingsession_id")]
        public int Id { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("duration")]
        public int Duration { get; set; }
        [Column("avg_watt")]
        public int Avg_watt { get ; set; }
        [Column("max_watt")]
        public int Max_watt { get; set; }
        [Column("avg_cadence")]
        public int Avg_cadence { get; set; }
        [Column("max_cadence")]
        public int Max_cadence { get; set ; }
        [Column("trainingtype")]
        public string Trainingtype { get; set; }
        [Column("comment")]
        public string? Comment { get; set; }
        [Column("member_id")]
        public int MemberId { get; set; }
        [Column("trainingsimpact")]
        public string Trainingsimpact { get; set; }
        public void CalculateTrainingImpact()
        {
            if (Avg_watt < 150)
            {
                if (Duration <= 90)
                    Trainingsimpact = "low";
                else
                    Trainingsimpact = "medium";
            }
            else if (Avg_watt >= 150 && Avg_watt <= 200)
            {
                Trainingsimpact = "medium";
            }
            else if (Avg_watt > 200)
            {
                Trainingsimpact = "high";
            }
        }


        [ForeignKey("MemberId")]
        public Members Member { get; set; }


    }
}
