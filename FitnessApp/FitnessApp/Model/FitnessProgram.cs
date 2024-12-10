using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("program", Schema = "dbo")]
    public class FitnessProgram
    {
        [Key]
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public DateTime StartDate { get; set; }
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
