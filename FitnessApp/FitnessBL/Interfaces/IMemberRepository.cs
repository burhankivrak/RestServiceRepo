using System.Diagnostics.Metrics;
using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface IMemberRepository
    {
        void AddMember(Members member);
        Members GetMember(int id);
        Members? GetMemberByEmailAndBirthday(string email, DateTime geboortedatum);
        void UpdateMember(Members member);
        bool ExistsMember(int id);
        IEnumerable<Reservation> GetReservationsForMember(int memberId);
        IEnumerable<FitnessProgram> GetProgramMembersForMember(int memberId);
        IEnumerable<object> GetTrainingsessionsForMember(string type, int memberId);

    }
}
