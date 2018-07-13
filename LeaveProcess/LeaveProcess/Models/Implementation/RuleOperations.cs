using LeaveProcess.Entity_DAL;
using LeaveProcess.Models.DTO;
using LeaveProcess.Models.Interfaces;
using System.Collections.Generic;
 

namespace RuleProcess.Models.Implementation
{
    public class RuleOperations : IRuleOperations
    {
        #region Private fields

        IRuleRepository _ruleRepository;

        #endregion

        #region Constructor

        public RuleOperations(IRuleRepository ruleRepository)
        {

            _ruleRepository = ruleRepository;
        }

        #endregion

        #region Implementing IRuleOperations
     

        public IEnumerable<Rule> GetRules()
        {
            return _ruleRepository.GetAll();
        }

        public bool AddRule(Rule rule)
        {
            _ruleRepository.Add(rule);
            return true;
        }

        public bool RemoveRule(Rule rule)
        {
            _ruleRepository.Remove(rule, rule.ClientId);
            return true;
        }

        #endregion

    }
}
