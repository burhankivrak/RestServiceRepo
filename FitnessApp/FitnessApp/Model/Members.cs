using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessApp.Model
{
    [Table("members", Schema = "dbo")]
    public class Members
    {
        [Key]
        [Column("member_id")]
        public int Id { get; set; }
        [Column("first_name")]
        public string Voornaam { get; set; }
        [Column("last_name")]
        public string Achternaam { get; set; }
        [Column("email")]
        public string Emailadres { get; set; }
        [Column("address")]
        public string Verblijfsplaats { get; set; }
        [Column("birthday")]
        public DateTime Geboortedatum { get; set; }
        [Column("membertype")]
        public string TypeKlant { get; set; }
        [Column("interests")]
        public List<string>? Interesses { get; set; } // Nullable gemaakt
        public Members()
        {
            Interesses = null;  // Optioneel, kan ook niet worden geïnitialiseerd
        }
        //public Members(int Id, string voornaam, string achternaam, string emailadres, string verblijfsplaats, DateTime geboortedatum, string typeKlant)
        //{
        //    this.Id = Id;
        //    Voornaam = voornaam;
        //    Achternaam = achternaam;
        //    Emailadres = emailadres;
        //    Verblijfsplaats = verblijfsplaats;
        //    Geboortedatum = geboortedatum;
        //    TypeKlant = typeKlant;
        //    Interesses = new List<string>(); 
        //}

        public Members(int id, string voornaam, string achternaam, string emailadres, string verblijfsplaats, DateTime geboortedatum, string typeKlant, List<string> interesses)
        {
            Id = id;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Emailadres = emailadres;
            Verblijfsplaats = verblijfsplaats;
            Geboortedatum = geboortedatum;
            TypeKlant = typeKlant;
            Interesses = interesses;
        }
    }
}
