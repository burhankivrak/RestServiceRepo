using System.Diagnostics.Metrics;
using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;

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
    }
}
