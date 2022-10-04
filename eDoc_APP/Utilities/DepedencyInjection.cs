using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Mvc5;
using Unity;
using eDoc_APP.Services;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Mapper;
using AutoMapper;

namespace eDoc_APP.Utilities
{
    /// <summary>
    /// Provides the default dependency resolver for the application - based on IDependencyResolver, which hhas just two methods
    /// </summary>
    public class DepedencyInjection 
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<eDocumentContext>();
            container.RegisterType<eDocumentContext>();
            container.RegisterType<IApproveProcessServices, ApproveProcessServices>();
            container.RegisterType<IDocumentServices, DocumentServices>();
            container.RegisterType<IOfficeServices, OfficeServices>();
            #region AutoMapper
            MapperConfiguration config = AutoMapperProfile.Configure(); ;
            //build the mapper
            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);
            #endregion
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}