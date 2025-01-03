using System.Diagnostics.Metrics;
using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                throw new MemberException("Member already exists");

            _context.Members.Add(member);
            _context.SaveChanges();
        }

        public Members GetMember(int id)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
                throw new MemberException("Member doesn't exist");

            return member;
        }

        public Members? GetMemberByEmailAndBirthday(string email, DateTime geboortedatum)
        {
            var member = _context.Members
                   .FirstOrDefault(m => m.Emailadres.ToLower() == email.ToLower()
                                     && m.Geboortedatum.Date == geboortedatum.Date);

            if (member == null)
                throw new MemberException("No member found with given email and birthday.");

            return member;
        }



        public void UpdateMember(Members member)
        {
            var existingMember = _context.Members.FirstOrDefault(m => m.Id == member.Id);

            if (existingMember == null)
                throw new MemberException("Member doesn't exist");

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
            
            var programMembers = _context.ProgramMembers
                                 .Include(pm => pm.Program)
                                 .Where(pm => pm.MemberId == memberId)
                                 .Select(pm => pm.Program) 
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
                throw new MemberException("Invalid training type specified.");
            }
            return sessions;
        }
    }
}
