using FitnessApp.Model;

namespace FitnessApp.Interface
{
    public interface IProgramRepository
    {
        void AddProgram(FitnessProgram program);
        FitnessProgram GetProgram(string programCode);
        void UpdateProgram(FitnessProgram program);

        bool ExistsProgram(string programCode);
    }
}
