using LeaveProcess.Models.DTO;
using LeaveProcess.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LeaveProcess.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Leave")]
    public class LeaveController : Controller
    {
        #region Private fields

        /// <summary>
        /// Leave Operations instance
        /// </summary>        
        readonly ILeaveOperations _LeavesOperation;
        #endregion

        #region Construction
        public LeaveController(ILeaveOperations LeavesOperation)
        {
            _LeavesOperation = LeavesOperation;
        }

        #endregion     

        #region Leave services
        // GET api/values
        [HttpGet]
        public IActionResult GetLeaves()
        {
            try
            {
                IEnumerable<Leave> LeavesList = _LeavesOperation.GetLeaves();

                if (LeavesList.Count() == 0)
                {
                    return Json("No Leaves found");
                }

                return Json(LeavesList.OrderBy(x => x.LeaveDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddLeave([FromBody]Leave info)
        {
            try
            {
                if (info == null)
                {
                    return BadRequest("Leave details are null!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_LeavesOperation.AddLeave(info))
                {
                    return Json("Leave created successfully");
                }

                return BadRequest("Leave creation failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult RemoveLeave([FromBody]Leave info)
        {
            try
            {
                if (info == null)
                {
                    return BadRequest("Leave details are null!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_LeavesOperation.RemoveLeave(info))
                {
                    return Json("Leave Removed successfully");
                }

                return BadRequest("Leave Removed failed!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
