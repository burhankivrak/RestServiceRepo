using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Model
{
    public class Members
    {
        [Key]
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Emailadres { get; set; }
        public string Verblijfsplaats { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string TypeKlant { get; set; }
        public List<string> Interesses { get; set; }

        public Members()
        {
        }
        public Members(int Id, string voornaam, string achternaam, string emailadres, string verblijfsplaats, DateTime geboortedatum, string typeKlant)
        {
            this.Id = Id;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Emailadres = emailadres;
            Verblijfsplaats = verblijfsplaats;
            Geboortedatum = geboortedatum;
            TypeKlant = typeKlant;
            Interesses = new List<string>(); 
        }

        public Members(int Id, string voornaam, string achternaam, string emailadres, string verblijfsplaats, DateTime geboortedatum, string typeKlant, List<string> interesses)
        {
            this.Id = Id;
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
