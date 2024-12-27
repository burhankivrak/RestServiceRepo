using System.Diagnostics.Metrics;
using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Manager
{
    public class MemberRepository : IMemberRepository
    {
        private FitnessContext context;

        public MemberRepository()
        {
            this.context = new FitnessContext();
        }

        public void AddMember(Members member)
        {
            if (ExistsMember(member.Id))
                throw new Exception("Member already exists");
            
            context.Members.Add(member);
            context.SaveChanges();
        }

        public Members GetMember(int id)
        {
            var member = context.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
                throw new Exception("Member doesn't exist");

            return member;
        }

        public void UpdateMember(Members member)
        {
            var existingMember = context.Members.FirstOrDefault(m => m.Id == member.Id);

            if (existingMember == null)
                throw new Exception("Member doesn't exist");

            context.Entry(existingMember).CurrentValues.SetValues(member);
            context.SaveChanges();
        }

        public bool ExistsMember(int id)
        {
            return context.Members.Any(m => m.Id == id);
        }

        public IEnumerable<Reservation> GetReservationsForMember(int memberId)
        {
            var reservations = context.Reservation
                                      .Include(r => r.Member) 
                                      .Where(r => r.MemberId == memberId)
                                      .ToList();

            return reservations;
        }

        public IEnumerable<FitnessProgram> GetProgramMembersForMember(int memberId)
        {
            //var programMembers = context.ProgramMembers.Where(pm =>  pm.MemberId == memberId).ToList();
            var programMembers = context.ProgramMembers
                                 .Include(pm => pm.Program) // Laad het gerelateerde programma
                                 .Where(pm => pm.MemberId == memberId)
                                 .Select(pm => pm.Program) // Selecteer alleen het programma
                                 .ToList();

            return programMembers;
        }
    }
}
