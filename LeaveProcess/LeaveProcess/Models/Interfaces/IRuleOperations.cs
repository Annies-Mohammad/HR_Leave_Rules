using LeaveProcess.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveProcess.Models.Interfaces
{
    public interface IRuleOperations
    {
        IEnumerable<Rule> GetRules();
        bool AddRule(Rule rule);
        bool RemoveRule(Rule rule);
    }
}
