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
        [Column("equipment_id")]
        public int EquipmentId { get; set; }
        [Column("time_slot_id")]
        public int TimeSlotId { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("member_id")]
        public int MemberId { get; set; }


        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }

        [ForeignKey("TimeSlotId")]
        public Timeslot Timeslot { get; set; }

        [ForeignKey("MemberId")]
        public Members Member { get; set; }
        public Reservation()
        {
        }

        public Reservation(int equipmentId, int timeSlotId, DateTime date, int memberId)
        {
            EquipmentId = equipmentId;
            TimeSlotId = timeSlotId;
            Date = date;
            MemberId = memberId;
        }
    }
}
