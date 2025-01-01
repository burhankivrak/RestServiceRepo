using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;

namespace FitnessApp.Manager
{
    public class TrainingsessionRepository : ITrainingsessionRepository
    {
        private FitnessContext context;

        public TrainingsessionRepository()
        {
            this.context = new FitnessContext();
        }

        public List<RunningSessionDetail> GetRunningSessionDetails(int runningSessionId)
        {
            return context.RunningSessionDetail
                           //.Include(rs => rs.RunningSession)
                           //.ThenInclude(rs => rs.Member)
                           .Where(rsd => rsd.RunningSessionId == runningSessionId)
                           .ToList();
        }

        public List<object> GetSessionsForMonthAndYear(string type, int memberId, int month, int year)
        {
           
            var runningSessions = context.RunningSession
                .Where(rs => rs.MemberId == memberId && rs.Date.Month == month && rs.Date.Year == year)
                .OrderBy(rs => rs.Date)
                .ToList();

            var cyclingSessions = context.CyclingSession
                .Where(cs => cs.MemberId == memberId && cs.Date.Month == month && cs.Date.Year == year)
                .OrderBy(cs => cs.Date)
                .ToList();

            var sessions = new List<object>();
            if (string.IsNullOrWhiteSpace(type))
            {
                sessions.AddRange(runningSessions);
                sessions.AddRange(cyclingSessions);
            }
            else if (type.ToLower() == "running")
            {
                sessions.AddRange(runningSessions);
            }
            else if (type.ToLower() == "cycling")
            {
                sessions.AddRange(cyclingSessions);
            }
            else
            {
                throw new Exception("Invalid training type specified.");
            }

            foreach (var session in cyclingSessions)
            {
                session.CalculateTrainingImpact();
            }

            var sortedSessions = sessions
                .OrderBy(session => 
                session is RunningSession ? 
                ((RunningSession)session).Date : 
                ((CyclingSession)session).Date)
                .ToList();

            return sortedSessions;
        }

        public object GetSessionStatsForMember(string type, int memberId)
        {
            List<object> sessions = new List<object>();

            var runningSessions = context.RunningSession
                .Where(rs => rs.MemberId == memberId)
                .ToList();

            var cyclingSessions = context.CyclingSession
                .Where(cs => cs.MemberId == memberId)
                .ToList();

            if (string.IsNullOrWhiteSpace(type))
            {
                sessions.AddRange(runningSessions);
                sessions.AddRange(cyclingSessions);
            }
            else if (type.ToLower() == "running")
            {
                sessions.AddRange(runningSessions);
            }
            else if (type.ToLower() == "cycling")
            {
                sessions.AddRange(cyclingSessions);
            }
            else
            {
                throw new Exception("Invalid training type specified.");
            }

            var totalSessions = sessions.Count;
            var totalDurationInMinutes = sessions.Sum(session =>
                session is RunningSession rs ? rs.Duration : (session is CyclingSession cs ? cs.Duration : 0));

            var maxDurationSession = sessions.Max(session =>
                session is RunningSession rs ? rs.Duration : (session is CyclingSession cs ? cs.Duration : 0));

            var minDurationSession = sessions.Min(session =>
                session is RunningSession rs ? rs.Duration : (session is CyclingSession cs ? cs.Duration : 0));

            var averageDuration = totalSessions > 0 ? totalDurationInMinutes / totalSessions : 0;

            return new
            {
                TotalSessions = totalSessions,
                TotalDurationInHours = totalDurationInMinutes / 60.0,
                MaxSessionDuration = maxDurationSession,
                MinSessionDuration = minDurationSession,
                AvgSessionDuration = averageDuration
            };
        }

        public Dictionary<int, int> GetSessionCountPerMonthForYear(string type, int memberId, int year)
        {
            var runningSessions = context.RunningSession
                .Where(rs => rs.MemberId == memberId && rs.Date.Year == year)
                .ToList();

            var cyclingSessions = context.CyclingSession
                .Where(cs => cs.MemberId == memberId && cs.Date.Year == year)
                .ToList();

            List<object> sessions = new List<object>();

            if (string.IsNullOrWhiteSpace(type))
            {
                sessions.AddRange(runningSessions);
                sessions.AddRange(cyclingSessions);
            }
            else if (type.ToLower() == "running")
            {
                sessions.AddRange(runningSessions);
            }
            else if (type.ToLower() == "cycling")
            {
                sessions.AddRange(cyclingSessions);
            }
            else
            {
                throw new Exception("Invalid training type specified.");
            }

            var sessionCountPerMonth = sessions
                .GroupBy(s => s is RunningSession rs ? rs.Date.Month : (s is CyclingSession cs ? cs.Date.Month : 0))
                .ToDictionary(group => group.Key, group => group.Count());

            for (int month = 1; month <= 12; month++)
            {
                if (!sessionCountPerMonth.ContainsKey(month))
                {
                    sessionCountPerMonth[month] = 0;
                }
            }

            return sessionCountPerMonth;
        }

        public List<object> GetSessionCountPerMonthForYearWithType(int memberId, int year)
        {
            var runningSessions = context.RunningSession
                .Where(rs => rs.MemberId == memberId && rs.Date.Year == year)
                .ToList();

            var cyclingSessions = context.CyclingSession
                .Where(cs => cs.MemberId == memberId && cs.Date.Year == year)
                .ToList();

            var result = new List<object>();

            for (int month = 1; month <= 12; month++)
            {
                int runningCount = runningSessions.Count(rs => rs.Date.Month == month);
                int cyclingCount = cyclingSessions.Count(cs => cs.Date.Month == month);

                int enduranceCount = cyclingSessions.Count(cs => cs.Date.Month == month && cs.Trainingtype == "endurance");
                int funCount = cyclingSessions.Count(cs => cs.Date.Month == month && cs.Trainingtype == "fun");
                int intervalCount = cyclingSessions.Count(cs => cs.Date.Month == month && cs.Trainingtype == "interval");
                int recoveryCount = cyclingSessions.Count(cs => cs.Date.Month == month && cs.Trainingtype == "recovery");

                result.Add(new
                {
                    Month = month,
                    RunningCount = runningCount,
                    CyclingCoumt = cyclingCount,
                    Endurance = enduranceCount,
                    Fun = funCount,
                    Interval = intervalCount,
                    Recovery = recoveryCount
                });
            }

            return result;
        }

    }
}
