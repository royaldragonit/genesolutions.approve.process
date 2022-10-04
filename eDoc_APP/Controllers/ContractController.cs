using eDoc_APP.Commons.Const;
using eDoc_APP.Services;
using eDoc_APP.Utilities;
using GS.eDocument.Models.ContractModels;
using GS.eDocument.Models.CustomModels;
using GS.eDocument.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpGetAttribute;

namespace eDoc_APP.Controllers
{
    public class ContractController : BaseController
    {
        private readonly IContractServices _contractServices;
        public ContractController(eDocumentContext db, IContractServices contractServices) : base(db)
        {
            _contractServices = contractServices;
        }

        // GET: Contract
        public ActionResult Index()
        {
            return View();
        }
        // GET: Contract
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var model = await _contractServices.SelectListApproveProcess();
            return View(model);
        }
        // GET: Contract
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateContractModel create)
        {
            bool isDocxFile = HttpContext.Request.Files.CheckIsDocxFileOrEmptyFile();
            if (!isDocxFile) return Json(new ResultCustomModel<bool>
            {
                Success = false,
                Message = MessageConst.InvalidFile,
                Code = 400
            });
            create.Email = User.GetClaimByType("preferred_username");
            bool isSuccess = await _contractServices.Create(create);
            return Json(new ResultCustomModel<bool>
            {
                Success = isSuccess,
                Message = isSuccess ? MessageConst.Success : MessageConst.Failed,
                Code = isSuccess ? 200 : 400
            });
        }
        // GET: Detail
        public ActionResult Detail(int id)
        {
            return View();
        }
    }
}