using LeaveProcess.Entity_DAL;
using LeaveProcess.Models.DTO;
using LeaveProcess.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveProcess.Models.Implementation
{
    public class LeaveOperations : ILeaveOperations
    {
        #region Private fields

        ILeaveRepository _leaveRepository;      

        #endregion


        #region Constructor

        public LeaveOperations(ILeaveRepository LeaveRepository, IRuleOperations ruleOperations)
        {
            _leaveRepository = LeaveRepository;           
        }

        #endregion

        #region Implementing ILeaveOperations

        /// <summary>
        /// Adds a new Leave
        /// </summary>
        /// <param name="Leave">Leave details object</param>
        /// <returns>Returns true</returns>
        public bool AddLeave(Leave Leave)
        {
            _leaveRepository.Add(Leave);
            return true;
        }

        /// <summary>
        /// Remove Leave
        /// </summary>
        /// <param name="Leave">Leave details object</param>
        /// <returns>Returns true</returns>
        public bool RemoveLeave(Leave leave)
        {
            _leaveRepository.Remove(leave);
            return true;
        }

        /// <summary>
        /// Retrieves the list of Leaves
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Leave> GetLeaves()
        {
            return _leaveRepository.GetAll();
        }

        #endregion
    }
}
