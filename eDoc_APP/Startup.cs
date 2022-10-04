using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using eDoc_APP.Services;
using eDoc_APP.Utilities;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

namespace eDoc_APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureServices();
            ConfigureAuth(app);
        }
        /// <summary>
        /// Config DI
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices()
        {
            var services = new ServiceCollection();
            //====================================================
            // Create the DB context for the IDENTITY database
            //====================================================
            // Add a database context - this can be instantiated with no parameters
            services.AddTransient(typeof(eDocumentContext));
            //services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddTransient<IApproveProcessServices, ApproveProcessServices>();
            //services.AddTransient(typeof(IApproveProcessServices), p=>new ApproveProcessServices());
            Extension.ServiceProvider = services.BuildServiceProvider();
        }
    }
}
