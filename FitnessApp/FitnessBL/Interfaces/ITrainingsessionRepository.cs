using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface ITrainingsessionRepository
    {
        IEnumerable<RunningSessionDetail> GetRunningSessionDetails(int runningSessionId);
        IEnumerable<object> GetSessionsForMonthAndYear(string type, int memberId, int month, int year);
        object GetSessionStatsForMember(string type, int memberId);

        Dictionary<int, int> GetSessionCountPerMonthForYear(string type, int memberId, int year);
        IEnumerable<object> GetSessionCountPerMonthForYearWithType(int memberId, int year);
        IEnumerable<object> GetTrainingImpactPerMonthForYear(int memberId, int year);
    }
}
