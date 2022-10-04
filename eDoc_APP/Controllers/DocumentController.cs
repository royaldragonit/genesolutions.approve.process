using eDoc_Core.Commons.Const;
using eDoc_APP.Services;
using eDoc_APP.Utilities;
using eDoc_Core.Models.DocumentModels;
using eDoc_Core.Models.CustomModels;
using eDoc_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using eDoc_Core.Core;
using System.Data.Entity;
using eDoc_Core.Models.ApproveProcessModels;
using System.Drawing;
using eDoc_Core.Models.Entities.StoredProcedures;

namespace eDoc_APP.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly IDocumentServices _documentServices;
        private readonly IOfficeServices _officeServices;
        public DocumentController(eDocumentContext db, IDocumentServices documentServices,IOfficeServices officeServices) : base(db)
        {
            _documentServices = documentServices;
            _officeServices = officeServices;
        }

        public async Task<ActionResult> Index(string filter)
        {
            var model = await _documentServices.List(GetEmail(), filter);
            return View(model);
        }

        public async Task<ActionResult> ActionApprove(ApproveActionModel approveModel)
        {
            approveModel.Email = GetEmail();
            var model = await _documentServices.ActionApprove(approveModel);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Create()
        {
            var model = await _documentServices.SelectListApproveProcess();
            ViewBag.TypeDocument = await _documentServices.SelectListTypeDocument();
            return View(model);
        }
        // POST: CreateDocument
        [HttpPost]
        public async Task<ActionResult> CreateDocument([FromBody] CreateDocumentModel create)
        {
            bool isDocxFile = HttpContext.Request.Files.CheckIsValidFile();
            if (!isDocxFile) return Json(new ResultCustomModel<bool>
            {
                Success = false,
                Message = MessageConst.InvalidFile,
                Code = 400
            });
            create.Files = HttpContext.Request.Files;
            create.Server = Server;
            create.Email = GetEmail();
            bool isSuccess = await _documentServices.Create(create);
            ModelState.AddModelError("CannotCreate", "Gửi thành công");
            if (isSuccess)
                return RedirectToAction("Index");
            ModelState.AddModelError("CannotCreate", "Không thể tạo tài liệu");
            return await Create();
        }
        // GET: Detail
        public async Task<ActionResult> Detail(int id)
        {
            var model = await _documentServices.Detail(id, GetEmail());
            var process = await _documentServices.GetProcessDocument(id);
            ViewBag.Process = process;
            ViewBag.ApproveProcess = await _documentServices.SelectListApproveProcess(id);
            ViewBag.TypeDocument = await _documentServices.SelectListTypeDocument(id);
            ViewBag.ListFileDocuments = await _db.DocumentFiles.Where(x => x.DocumentId == model.DocumentId).ToListAsync();
            return View(model);
        }
    }
}