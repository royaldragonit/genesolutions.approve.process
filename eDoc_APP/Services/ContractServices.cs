using AutoMapper;
using GS.eDocument.Models.ContractModels;
using GS.eDocument.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eDoc_APP.Services
{
    public class ContractServices : BaseServices, IContractServices
    {
        public ContractServices(IMapper mapper, eDocumentContext db) : base(mapper, db)
        {

        }

        public async Task<bool> Create(CreateContractModel create)
        {

            throw new NotImplementedException();
        }

        public async Task<List<SelectListItem>> SelectListApproveProcess()
        {
            var data = await _db.ApproveProcesss.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ApproveProcessId.ToString()
            }).ToListAsync();
            return data;
        }
    }
    public interface IContractServices
    {
        Task<bool> Create(CreateContractModel create);
        Task<List<SelectListItem>> SelectListApproveProcess();
    }
}