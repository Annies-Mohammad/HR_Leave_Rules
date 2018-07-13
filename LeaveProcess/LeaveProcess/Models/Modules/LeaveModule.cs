using Autofac;
using LeaveProcess.Models.Implementation;
using LeaveProcess.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveProcess.Models.Modules
{
       public class LeaveModule : Module
    {
        #region Protected methods
        protected override void Load(ContainerBuilder builder)
        {
            //main 
            builder.RegisterType<LeaveOperations>().As<ILeaveOperations>();
           // builder.RegisterType<LeaveRepository>().As<ILeaveRepository>();

            base.Load(builder);
        }
        #endregion
    }
}
