﻿using FitnessDL;
using FitnessDL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Geboortedatum { get; set; }
        [Column("membertype")]
        public KlantType TypeKlant { get; set; } = KlantType.Bronze;
        [Column("interests")]
        public List<string>? Interesses { get; set; } 
        public Members()
        {
            Interesses = null;  
        }

        public Members(int id, string voornaam, string achternaam, string emailadres, string verblijfsplaats, DateTime geboortedatum, KlantType typeKlant, List<string> interesses)
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
