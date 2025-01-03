using FitnessApp.Data;
using FitnessApp.Interface;
using FitnessApp.Model;
using FitnessBL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessApp.Manager
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly FitnessContext _context;
        public ProgramRepository(FitnessContext context)
        {
            _context = context;
        }

        public void AddProgram(FitnessProgram program)
        {
            var programCode = GenerateProgramCode(program);

            if (ExistsProgram(program.ProgramCode))
                throw new ProgramException("Program already exists");

            program.ProgramCode = programCode;

            _context.Program.Add(program);
            _context.SaveChanges();
        }   

        public bool ExistsProgram(string programCode)
        {
            return _context.Program.Any(p => p.ProgramCode == programCode);
        }

        public FitnessProgram GetProgram(string programCode)
        {
            var program = _context.Program.FirstOrDefault(p => p.ProgramCode == programCode);

            if (program == null)
                throw new ProgramException("Program doesn't exist");

            return program;
        }

        public void UpdateProgram(FitnessProgram program)
        {
            var existingProgram = _context.Program.FirstOrDefault(p => p.ProgramCode == program.ProgramCode);

            if (existingProgram == null)
                throw new ProgramException("Program doesn't exist");

            _context.Entry(existingProgram).CurrentValues.SetValues(program);
            _context.SaveChanges();
        }

        public string GenerateProgramCode(FitnessProgram program)
        {
            // Stap 1: Haal de eerste 2-3 letters van de naam
            string namePrefix = program.Name.Substring(0, Math.Min(3, program.Name.Length)).ToUpper();

            // Stap 2: Bepaal de letter voor de target
            string targetLetter = program.Target.ToLower() switch
            {
                "beginner" => "b",
                "advanced" => "a",
                "pro" => "p",
                _ => throw new Exception("Invalid target specified")
            };

            // Stap 3: Genereer een willekeurig nummer tussen 1 en 99
            Random random = new Random();
            int randomNumber = random.Next(1, 100); 

            // Stap 4: Combineer alles tot een programCode
            string programCode = $"{namePrefix}{targetLetter}{randomNumber}";

            // Stap 5: Controleer of de code al bestaat en genereer een nieuwe als dat het geval is
            while (ExistsProgram(programCode))
            {
                randomNumber = random.Next(1, 100); 
                programCode = $"{namePrefix}{targetLetter}{randomNumber}";
            }

            return programCode;
        }
    }
}
