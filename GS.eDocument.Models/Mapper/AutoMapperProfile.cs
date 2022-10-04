using AutoMapper;
using GS.eDocument.Models.ApproveProcessModels;
using GS.eDocument.Models.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.eDocument.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Step, StepModel>()
                .ForMember(x => x.RollBack, y => y.MapFrom(s => s.RollBackToStep))
                .ForAllOtherMembers(opts => opts.Ignore());
        }
        public static MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.ValidateInlineMaps = false;
                cfg.CreateMap<Step, StepModel>()
                .ForMember(x => x.RollBack, y => y.MapFrom(s => s.RollBackToStep));
            });

            return mapperConfiguration;
        }
    }
}
