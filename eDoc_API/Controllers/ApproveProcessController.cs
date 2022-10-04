using eDoc_Core.Commons.Const;
using eDoc_Core.Core;
using eDoc_Core.Models.ApproveProcessModels;
using eDoc_Core.Models.CustomModels;
using eDoc_Core.Models.Entities;
using eDoc_Core.Models.Entities.Views;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace eDoc_API.Controllers
{
    public class ApproveProcessController : Controller
    {
        private readonly eDocumentContext _db;
        public ApproveProcessController()
        {
            _db = new eDocumentContext();
        }
        // GET: ApproveProcess
        public async Task<List<vApproveProcess>> Index()
        {
            var data = await _db.Views<vApproveProcess>().ToListAsync();
            return data;
        }

        [HttpPost]
        public async Task<ResultCustomModel<bool>> CreateApproveProcess([FromBody] ApproveProcessRequestModel request)
        {
            request.Email = User.GetClaimByType("preferred_username");
            ApproveProcess approve = new ApproveProcess();
            approve.Email = request.Email;
            approve.Description = request.Description;
            approve.Name = request.Name;
            _db.ApproveProcesss.Add(approve);
            request.Steps.ForEach(s =>
            {
                var step = new Step();
                foreach (string item in s.ApproveWith ?? new List<string>())
                {
                    Approve stepDetail = new Approve();
                    stepDetail.IsApprove = false;
                    stepDetail.Step = step;
                    stepDetail.Email = item;
                    _db.Approves.Add(stepDetail);
                }
                step.RollBackToStep = s.RollBack;
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
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }
        public async Task<ApproveProcess> Detail(int id)
        {
            var data = await _db.ApproveProcesss
                               .Include(x => x.Steps.Select(y => y.Approves))
                               .FirstOrDefaultAsync(x => x.ApproveProcessId == id);
            return data;
        }
    }
}