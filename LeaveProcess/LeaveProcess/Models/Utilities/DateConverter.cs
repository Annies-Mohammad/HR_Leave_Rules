using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveProcess.Models.Utilities
{
   public class DateConverter : IsoDateTimeConverter
    {
        #region Public constants

        public const string DateFormat = "dd-MM-yyyy";

        #endregion

        #region Constructor
        public DateConverter(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
        #endregion
    
    }
}
