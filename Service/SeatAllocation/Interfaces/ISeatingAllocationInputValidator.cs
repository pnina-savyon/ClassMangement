﻿using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Interfaces
{
    public interface ISeatingAllocationInputValidator
    {
        bool IsValidInput(List<Student> students, List<Chair> chairs, Class schoolClass);
    }
}
