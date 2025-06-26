using Repository.Entities;
using Service.SeatAllocation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Solver
{
    public class SeatingAllocationInputValidator :ISeatingAllocationInputValidator
    {
        public bool IsValidInput(List<Student> students, List<Chair> chairs, Class schoolClass)
        {
            if (students == null || students.Count == 0)
                return false;

            if (chairs == null || chairs.Count == 0)
                return false;

            if (schoolClass == null)
                return false;

            if (schoolClass.Id <= 0)
                return false;

            if (schoolClass.Students == null || schoolClass.Students.Count == 0)
                return false;

            if (schoolClass.Chairs == null || schoolClass.Chairs.Count == 0)
                return false;

            foreach (var student in students)
            {
                if (student == null)
                    return false;

                if (student.ClassId != schoolClass.Id)
                    return false;

                if (student.StatusSocial == null)
                    return false;

                //תוכן שגוי לא צריך לבדוק מראש.
            }

            foreach (var chair in chairs)
            {
                if (chair == null)
                    return false;

                if (chair.Id <= 0)
                    return false;

                if (chair.ClassId != schoolClass.Id)
                    return false;
            }

            if (chairs.Count < students.Count)
                return false;

            return true;
        }

    }
}
