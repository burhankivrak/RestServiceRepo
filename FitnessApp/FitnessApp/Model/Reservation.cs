using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("reservation", Schema = "dbo")]
    public class Reservation
    {
        [Key]
        [Column("reservation_id")]
        public int Id { get; set; }
        [Column("member_id")]
        public int MemberId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }

        [NotMapped] // We slaan deze niet op in de database, het zijn afgeleide velden.
        public string Voornaam => Member?.Voornaam;

        [NotMapped]
        public string Achternaam => Member?.Achternaam;

        [NotMapped]
        public string Emailadres => Member?.Emailadres;

        [ForeignKey("MemberId")]
        public Members Member { get; set; }

        public Reservation()
        {
        }
        public Reservation(int id, int memberId, DateTime date)
        {
            Id = id;
            MemberId = memberId;
            Date = date;
        }
    }
}
