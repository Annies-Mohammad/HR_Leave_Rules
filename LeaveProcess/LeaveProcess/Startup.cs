using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LeaveProcess.Entity_DAL;
using LeaveProcess.Models.Modules;
using System;

namespace LeaveProcess
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        #region Construction
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
        #endregion

        #region Public properties
        public IConfiguration Configuration { get; }
        #endregion

        #region Public methods
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<LeaveCOntext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("dbConnectionString")));

            //create ioc container
            var builder = new ContainerBuilder();
            builder.RegisterModule<LeaveModule>();

            builder.Populate(services);
            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        } 
        #endregion
    }
}
