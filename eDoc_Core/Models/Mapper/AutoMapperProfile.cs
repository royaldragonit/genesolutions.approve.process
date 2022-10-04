using AutoMapper;
using eDoc_Core.Models.ApproveProcessModels;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Entities.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
        }
        public static MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Document, vDocument>();
                cfg.CreateMap<ApproveProcessRequestModel, ApproveProcess>()
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.ApproveProcessId, y => y.Ignore())
                .ForMember(x => x.IsActive, y => y.Ignore())
                .ForMember(x => x.TypeDocuments, y => y.Ignore())
                .ForMember(x => x.CreateOn, y => y.Ignore())
                .ForMember(x => x.Steps, y => y.Ignore());
            });

            return mapperConfiguration;
        }
    }
}
