using LeaveProcess.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveProcess.Models.Interfaces
{
   public interface ILeaveOperations
    {
        IEnumerable<Leave> GetLeaves();

        bool AddLeave(Leave leave);
        bool RemoveLeave(Leave leave);

    }
}
