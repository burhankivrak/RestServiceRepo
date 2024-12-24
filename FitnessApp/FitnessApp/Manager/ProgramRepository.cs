using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessApp.Manager
{
    public class ProgramRepository : IProgramRepository
    {
        private FitnessContext context;
        public ProgramRepository()
        {
            this.context = new FitnessContext();
        }

        public void AddProgram(FitnessProgram program)
        {
            if (ExistsProgram(program.ProgramCode))
                throw new Exception("Program already exists");

            context.Program.Add(program);  
            context.SaveChanges();
        }   

        public bool ExistsProgram(string programCode)
        {
            return context.Program.Any(p => p.ProgramCode == programCode);
        }

        public FitnessProgram GetProgram(string programCode)
        {
            var program = context.Program.FirstOrDefault(p => p.ProgramCode == programCode);

            if (program == null)
                throw new Exception("Program doesn't exist");

            return program;
        }

        public void UpdateProgram(FitnessProgram program)
        {
            var existingProgram = context.Program.FirstOrDefault(p => p.ProgramCode == program.ProgramCode);

            if (existingProgram == null)
                throw new Exception("Program doesn't exist");

            //existingProgram.Name = program.Name;
            //existingProgram.Target = program.Target;
            //existingProgram.StartDate = program.StartDate;
            //existingProgram.MaxMembers = program.MaxMembers;
            context.Entry(existingProgram).CurrentValues.SetValues(program);
            context.SaveChanges();
        }
    }
}
