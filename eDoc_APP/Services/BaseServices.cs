using AutoMapper;
using eDoc_Core.Models.ApproveProcessModels;
using eDoc_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eDoc_APP.Services
{
    /// <summary>
    /// Lớp Abtract để kế thừa toàn bộ các Services sau này
    /// </summary>
    public abstract class BaseServices
    {
        public readonly IMapper _mapper;
        public readonly eDocumentContext _db;
        public BaseServices(IMapper mapper, eDocumentContext db)
        {
            _mapper = mapper;
            _db = db;
        }
    }
}