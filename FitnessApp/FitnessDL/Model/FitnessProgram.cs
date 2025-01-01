using FitnessDL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FitnessApp.Model
{
    [Table("program", Schema = "dbo")]
    public class FitnessProgram
    {
        [Key]
        [Column("programCode")]
        public string ProgramCode { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("target")]
        public string Target { get; set; }
        [Column("startdate")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartDate { get; set; }
        [Column("max_members")]
        public int MaxMembers { get; set; }

        public FitnessProgram () { }
        public FitnessProgram(string programCode, string name, string target, DateTime startDate, int maxMembers)
        {
            ProgramCode = programCode;
            Name = name;
            Target = target;
            StartDate = startDate;
            MaxMembers = maxMembers;
        }
    }
}
