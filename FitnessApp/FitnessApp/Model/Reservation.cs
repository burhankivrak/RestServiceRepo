namespace FitnessApp.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public Equipment Equipment { get; set; }
        public Timeslot Timeslot { get; set; }
        public DateTime Date { get; set; }
        public Members Member { get; set; }

        public Reservation()
        {
        }

        public Reservation(int id, DateTime date, Equipment equipment, Timeslot timeslot, Members member)
        {
            Id = id;
            Date = date;
            Equipment.Id = equipment.Id;
            Timeslot.Id = timeslot.Id;
            Member.Id = member.Id;
        }
    }
}
