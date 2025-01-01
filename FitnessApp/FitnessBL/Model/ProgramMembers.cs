using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("programmembers", Schema = "dbo")]
    [PrimaryKey(nameof(ProgramCode), nameof(MemberId))]
    public class ProgramMembers
    {
        [Key]
        [Column("programCode", Order = 0)]
        public string ProgramCode { get; set; }
        [Key]
        [Column("member_id", Order = 1)]
        public int MemberId { get; set; }

        [ForeignKey("ProgramCode")]
        public FitnessProgram Program { get; set; }

        [ForeignKey("MemberId")]
        public Members Member { get; set; }
    }
}
