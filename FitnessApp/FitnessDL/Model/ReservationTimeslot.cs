using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FitnessApp.Model
{
    [Table("ReservationTimeslot", Schema = "dbo")]
    public class ReservationTimeslot
    {
        [Key]
        [Column("reservation_time_slot_id")]
        public int ReservationTimeslotId { get; set; }
        [Column("reservation_id")]
        public int ReservationId { get; set; }
        [Column("time_slot_id")]
        public int TimeslotId { get; set; }
        [Column("equipment_id")]
        public int EquipmentId { get; set; }

        [JsonIgnore]
        [ForeignKey("ReservationId")]
        public Reservation? Reservation { get; set; }
        [JsonIgnore]
        [ForeignKey("EquipmentId")]
        public Equipment? Equipment { get; set; }
        [JsonIgnore]
        [ForeignKey("TimeslotId")]
        public Timeslot? Timeslot { get; set; }

        public ReservationTimeslot() { }
        public ReservationTimeslot(int reservationTimeslotId, int reservationId, int timeslotId, int equipmentId) 
        { 
            ReservationTimeslotId = reservationTimeslotId;
            ReservationId = reservationId;
            TimeslotId = timeslotId;
            EquipmentId = equipmentId;
        }

        
    }
}
