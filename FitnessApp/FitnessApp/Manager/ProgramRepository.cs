using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessApp.Manager
{
    public class ProgramRepository : IProgramRepository
    {
        //private Dictionary<string, FitnessProgram> data = new Dictionary<string, FitnessProgram>();
        private FitnessContext context;
        public ProgramRepository()
        {
            this.context = new FitnessContext();
        }
        //public ProgramRepository()
        //{
        //    data.Add("res12", new FitnessProgram("res12", "Olympic", "87KG",new DateTime(2024, 12, 12), 20));
        //}
        public void AddProgram(FitnessProgram program)
        {
            //if (!context.Programs.ContainsKey(program.ProgramCode))
            //    context.Programs.Add(program.ProgramCode, program);
            //else
            //    throw new Exception("program already added");
            if (context.Program.Any(p => p.ProgramCode == program.ProgramCode))
                throw new Exception("Program already exists");

            context.Program.Add(program);  
            context.SaveChanges();
        }   

        public bool ExistsProgram(string programCode)
        {
            //if (context.Programs.ContainsKey(programCode)) return true;
            //else return false;
            return context.Program.Any(p => p.ProgramCode == programCode);
        }

        public FitnessProgram GetProgram(string programCode)
        {
            //if (context.Programs.ContainsKey(programCode))
            //    return context.Programs[programCode];
            //else
            //    throw new Exception("program doesn't exist");
            var program = context.Program.FirstOrDefault(p => p.ProgramCode == programCode);

            if (program == null)
                throw new Exception("Program doesn't exist");

            return program;
        }

        public void UpdateProgram(FitnessProgram program)
        {
            //if (context.Programs.ContainsKey(program.ProgramCode))
            //    context.Programs[program.ProgramCode] = program;
            //else
            //    throw new Exception("program doesn't exist");
            var existingProgram = context.Program.FirstOrDefault(p => p.ProgramCode == program.ProgramCode);

            if (existingProgram == null)
                throw new Exception("Program doesn't exist");

            //existingProgram.Name = program.Name;
            //existingProgram.Target = program.Target;
            //existingProgram.StartDate = program.StartDate;
            //existingProgram.MaxMembers = program.MaxMembers;

            context.Program.Update(existingProgram);  // Mark the program as modified
            context.SaveChanges();
        }
    }
}
