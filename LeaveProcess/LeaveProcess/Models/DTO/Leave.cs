using LeaveProcess.Entity_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaveProcess.Models.DTO
{
   public class Leave : IEntityBase
    {
        #region Public properties
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName should not be empty")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName should not be empty")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Leave date cannot be empty")]
        public DateTime LeaveDate{ get; set; }
        #endregion
    }
}
