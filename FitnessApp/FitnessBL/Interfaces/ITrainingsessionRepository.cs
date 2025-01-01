using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface ITrainingsessionRepository
    {
        List<RunningSessionDetail> GetRunningSessionDetails(int runningSessionId);
        List<object> GetSessionsForMonthAndYear(string type, int memberId, int month, int year);
        public object GetSessionStatsForMember(string type, int memberId);

        public Dictionary<int, int> GetSessionCountPerMonthForYear(string type, int memberId, int year);
        public List<object> GetSessionCountPerMonthForYearWithType(int memberId, int year);

    }
}
