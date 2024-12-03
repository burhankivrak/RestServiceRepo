namespace FitnessApp.Model
{
    public class Timeslot
    {
        public int Id { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string PartOfDay { get; set; }

        public Timeslot()
        {
        }

        public Timeslot(int id, int startTime, int endTime, string partOfDay)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            PartOfDay = partOfDay;
        }
    }
}
