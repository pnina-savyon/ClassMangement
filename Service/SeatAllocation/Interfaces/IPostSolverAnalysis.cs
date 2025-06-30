using Google.OrTools.Sat;
using Service.SeatAllocation.Logic.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Interfaces
{
    public interface IPostSolverAnalysis
    {
        Task APICalculatePriority(StudentContext context, CpSolver solver);
        Task UpdateStudentAndChairs();
    }
}
