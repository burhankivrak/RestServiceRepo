using System.Diagnostics.Metrics;
using FitnessApp.Interface;
using FitnessApp.Model;

namespace FitnessApp.Manager
{
    public class MemberRepository : IMemberRepository
    {
        private Dictionary<int, Members> data = new Dictionary<int, Members>();


        public MemberRepository()
        {
            data.Add(1, new Members(1, "Burhan", "Kivrak", "burhankivrak@gmail.com", "Mariakerke", new DateTime(2001, 3, 21), "Gold"));
        }

        public void AddMember(Members member)
        {
            if (!data.ContainsKey(member.Id))
                data.Add(member.Id, member);
            else
                throw new Exception("member already added");
        }

        public Members GetMember(int id)
        {
            if (data.ContainsKey(id))
                return data[id];
            else
                throw new Exception("member doesn't exist");
        }

        public void UpdateMember(Members member)
        {
            if (data.ContainsKey(member.Id))
                data[member.Id] = member;
            else
                throw new Exception("member doesn't exist");
        }

        public bool ExistsMember(int id)
        {
            if (data.ContainsKey(id)) return true;
            else return false;
        }
    }
}
