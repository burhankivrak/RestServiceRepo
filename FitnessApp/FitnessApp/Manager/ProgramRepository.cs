using FitnessApp.Interface;
using FitnessApp.Model;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessApp.Manager
{
    public class ProgramRepository : IProgramRepository
    {
        private Dictionary<string, FitnessProgram> data = new Dictionary<string, FitnessProgram>();

        
        public ProgramRepository()
        {
            data.Add("res12", new FitnessProgram("res12", "Olympic", "87KG",new DateTime(2024, 12, 12), 20));
        }
        public void AddProgram(FitnessProgram program)
        {
            if (!data.ContainsKey(program.ProgramCode))
                data.Add(program.ProgramCode, program);
            else
                throw new Exception("program already added");
        }

        public bool ExistsProgram(string programCode)
        {
            if (data.ContainsKey(programCode)) return true;
            else return false;
        }

        public FitnessProgram GetProgram(string programCode)
        {
            if (data.ContainsKey(programCode))
                return data[programCode];
            else
                throw new Exception("program doesn't exist");
        }

        public void UpdateProgram(FitnessProgram program)
        {
            if (data.ContainsKey(program.ProgramCode))
                data[program.ProgramCode] = program;
            else
                throw new Exception("program doesn't exist");
        }
    }
}
