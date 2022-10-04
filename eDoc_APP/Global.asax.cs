using AutoMapper;
using eDoc_APP.Utilities;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eDoc_APP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DepedencyInjection.RegisterComponents();
            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                HandleError.Error(exception.Message,exception);
            }
        }
    }
}
