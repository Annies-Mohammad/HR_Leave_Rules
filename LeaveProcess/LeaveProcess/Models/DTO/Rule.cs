using LeaveProcess.Entity_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaveProcess.Models.DTO
{
   public class Rule :IEntityBase
    {
        #region Public properties
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Rule description should not be empty")]
        public string RuleDescription { get; set; }

        [Required]
        public int ClientId { get; set; }
       

        #endregion
    }
}
