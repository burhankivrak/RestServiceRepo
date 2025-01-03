using FitnessApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessBL.Interfaces
{
    public interface ITimeslotRepository
    {
        IEnumerable<Timeslot> GetAll();
        Timeslot Get(int startTime, int endTime);
    }
}
