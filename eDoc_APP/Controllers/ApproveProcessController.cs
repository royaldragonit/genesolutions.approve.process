using eDoc_APP.Services;
using eDoc_APP.Utilities;
using eDoc_Core.Commons.Const;
using eDoc_Core.Core;
using eDoc_Core.Models.ApproveProcessModels;
using eDoc_Core.Models.CustomModels;
using eDoc_Core.Models.DocumentModels;
using eDoc_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace eDoc_APP.Controllers
{
    public class ApproveProcessController : BaseController
    {
        private readonly IApproveProcessServices _approveProcessServices;
        private readonly IDocumentServices _documentServices;
        public ApproveProcessController(eDocumentContext db, IApproveProcessServices approveProcessServices, IDocumentServices documentServices) : base(db)
        {
            _approveProcessServices = approveProcessServices;
            _documentServices = documentServices;   
        }
        // GET: ApproveProcess
        public async Task<ActionResult> Index()
        {
            var model = await _approveProcessServices.List();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateApproveProcess([FromBody] ApproveProcessRequestModel request)
        {
            request.Email = GetEmail();
            var data = await _approveProcessServices.Create(request);
            return Json(data);
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> Detail(int id)
        {
            var model = await _approveProcessServices.Detail(id);
            return View(model);
        }
    }
}