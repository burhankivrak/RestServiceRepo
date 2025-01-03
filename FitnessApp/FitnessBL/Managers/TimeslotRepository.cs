using FitnessApp.Data;
using FitnessApp.Model;
using FitnessBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessBL.Managers
{
    public class TimeslotRepository : ITimeslotRepository
    {
        private readonly FitnessContext _context;

        public TimeslotRepository(FitnessContext context)
        {
            _context = context;
        }

        public Timeslot Get(int startTime, int endTime)
        {
            return _context.Timeslot.FirstOrDefault(t => t.StartTime == startTime && t.EndTime == endTime);
        }

        public IEnumerable<Timeslot> GetAll()
        {
            return _context.Timeslot.ToList();
        }


    }
}
