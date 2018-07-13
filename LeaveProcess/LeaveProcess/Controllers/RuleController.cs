using LeaveProcess.Models.DTO;
using LeaveProcess.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuleProcess.Controllers
{
    public class RuleController : Controller
    {
        #region Private fields

        /// <summary>
        /// Rule Operations instance
        /// </summary>        
        readonly IRuleOperations _RulesOperation;
        #endregion

        #region Construction
        public RuleController(IRuleOperations RulesOperation)
        {
            _RulesOperation = RulesOperation;
        }

        #endregion     

        #region Rule services
        // GET api/values
        [HttpGet("api/Rule/{id}" ,Name ="GetRuleByID")]
        public IActionResult GetRules(int id)
        {
            try
            {
                IEnumerable<Rule> RulesList = _RulesOperation.GetRules();

                if (RulesList.Count() == 0)
                {
                    return Json("No Rules found with id"+ id);
                }

                return Json(RulesList.Select(x=>x.Id=id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddRule([FromBody]Rule info)
        {
            try
            {
                if (info == null)
                {
                    return BadRequest("Rule details are null!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (info.ClientId <= 0 || info.ClientId > int.MaxValue)
                {
                    return BadRequest("Invalid Client number.");
                }
                if (_RulesOperation.AddRule(info))
                {
                    return Json("Rule created successfully");
                }

                return BadRequest("Rule creation failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult RemoveRule([FromBody]Rule info)
        {
            try
            {
                if (info == null)
                {
                    return BadRequest("Rule details are null!");
                }
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (info.ClientId <= 0 || info.ClientId > int.MaxValue)
                {
                    return BadRequest("Invalid Client number.");
                }

                if (_RulesOperation.RemoveRule(info))
                {
                    return Json("Rule Removed successfully");
                }

                return BadRequest("Rule Removed failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
