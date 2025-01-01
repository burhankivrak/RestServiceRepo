using System.Diagnostics.Metrics;
using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Manager
{
    public class MemberRepository : IMemberRepository
    {
        private readonly FitnessContext _context;

        public MemberRepository(FitnessContext context)
        {
            _context = context;
        }

        public void AddMember(Members member)
        {
            if (ExistsMember(member.Id))
                throw new Exception("Member already exists");

            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public Members GetMember(int id)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
                throw new Exception("Member doesn't exist");

            return member;
        }

        public void UpdateMember(Members member)
        {
            var existingMember = _context.Members.FirstOrDefault(m => m.Id == member.Id);

            if (existingMember == null)
                throw new Exception("Member doesn't exist");

            _context.Entry(existingMember).CurrentValues.SetValues(member);
            _context.SaveChanges();
        }

        public bool ExistsMember(int id)
        {
            return _context.Members.Any(m => m.Id == id);
        }

        public IEnumerable<Reservation> GetReservationsForMember(int memberId)
        {
            var reservations = _context.Reservation
                                      .Include(r => r.Member) 
                                      .Where(r => r.MemberId == memberId)
                                      .ToList();

            return reservations;
        }

        public IEnumerable<FitnessProgram> GetProgramMembersForMember(int memberId)
        {
            //var programMembers = context.ProgramMembers.Where(pm =>  pm.MemberId == memberId).ToList();
            var programMembers = _context.ProgramMembers
                                 .Include(pm => pm.Program) // Laad het gerelateerde programma
                                 .Where(pm => pm.MemberId == memberId)
                                 .Select(pm => pm.Program) // Selecteer alleen het programma
                                 .ToList();

            return programMembers;
        }

        public IEnumerable<object> GetTrainingsessionsForMember(string type, int memberId)
        {
            var runningsessions = _context.RunningSession.Where(rs => rs.MemberId == memberId).ToList();
            var cyclingsessions = _context.CyclingSession.Where(cs =>  cs.MemberId == memberId).ToList();

            List<object> sessions = new List<object>();

            if (string.IsNullOrWhiteSpace(type))
            {
                sessions.AddRange(runningsessions);
                sessions.AddRange(cyclingsessions);
            }
            else if (type.ToLower() == "running")
            {
                sessions.AddRange(runningsessions);
            }
            else if (type.ToLower() == "cycling")
            {
                sessions.AddRange(cyclingsessions);
            }
            else
            {
                throw new Exception("Invalid training type specified.");
            }
            return sessions;
        }
    }
}
