using AutoMapper;
using eDoc_Core.Commons.Const;
using eDoc_APP.Utilities;
using eDoc_Core.Models.ApproveProcessModels;
using eDoc_Core.Core;
using eDoc_Core.Models.CustomModels;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Entities.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eDoc_APP.Services
{
    public class ApproveProcessServices : BaseServices, IApproveProcessServices
    {
        public ApproveProcessServices(IMapper mapper, eDocumentContext db) : base(mapper, db)
        {

        }

        public async Task<ResultCustomModel<bool>> Create(ApproveProcessRequestModel request)
        {
            ApproveProcess approve =_mapper.Map<ApproveProcess>(request);
            _db.ApproveProcesss.Add(approve);
            int stepIndex = 1;
            request.Steps.ForEach(s =>
            {
                var step = new Step();
                step.StepIndex = stepIndex++;
                foreach (string item in s.ApproveWith ?? new List<string>())
                {
                    Approve stepDetail = new Approve();
                    stepDetail.IsApprove = false;
                    stepDetail.Step = step;
                    stepDetail.Email = item;
                    _db.Approves.Add(stepDetail);
                }
                step.RollBackToStep = s.RollBack;
                step.IsAllAccept = s.IsAllAccept;
                step.ApproveProcess = approve;
                _db.Steps.Add(step);
            });
            //Số lượng Row được thêm vào db
            int save = await _db.SaveChangesAsync();
            return new ResultCustomModel<bool>
            {
                Success = save > 0,
                Code = save > 0 ? 200 : 400,
                Message = save > 0 ? MessageConst.Success : MessageConst.Failed
            };
        }

        public async Task<List<vApproveProcess>> List()
        {
            var data = await _db.Views<vApproveProcess>().ToListAsync();
            return data;
        }
        public async Task<ApproveProcess> Detail(int approveProcessId)
        {
            var data = await _db.ApproveProcesss
                                .Include(x=>x.Steps.Select(y=>y.Approves))
                                .FirstOrDefaultAsync(x=>x.ApproveProcessId==approveProcessId);
            return data;
        }
    }
    public interface IApproveProcessServices
    {
        Task<ResultCustomModel<bool>> Create(ApproveProcessRequestModel request);
        Task<ApproveProcess> Detail(int approveProcessId);
        Task<List<vApproveProcess>> List();
    }
}