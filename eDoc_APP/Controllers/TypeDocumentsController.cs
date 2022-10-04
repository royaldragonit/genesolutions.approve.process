using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eDoc_APP.Services;
using eDoc_Core.Models.Entities;

namespace eDoc_APP.Controllers
{
    public class TypeDocumentsController : BaseController
    {
        private readonly eDocumentContext _db ;
        private readonly IDocumentServices _documentServices;

        public TypeDocumentsController(eDocumentContext db, IDocumentServices documentServices) : base(db)
        {
            _documentServices = documentServices;
            _db = db;
        }

        // GET: TypeDocuments
        public ActionResult Index()
        {
            return View(_db.TypeDocuments.ToList());
        }

        // GET: TypeDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeDocument typeDocument = _db.TypeDocuments.Find(id);
            if (typeDocument == null)
            {
                return HttpNotFound();
            }
            return View(typeDocument);
        }

        // GET: TypeDocuments/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ApproveProcess = await _documentServices.SelectListApproveProcess();
            return View();
        }

        // POST: TypeDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "TypeDocumentId,Name,ApproveProcessId")] TypeDocument typeDocument)
        {
            ViewBag.ApproveProcess = await _documentServices.SelectListApproveProcess();
            if (ModelState.IsValid)
            {
                typeDocument.CreateOn = DateTime.Now;
                typeDocument.IsActive = true;
                _db.TypeDocuments.Add(typeDocument);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeDocument);
        }

        // GET: TypeDocuments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeDocument typeDocument = _db.TypeDocuments.Find(id);
            if (typeDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApproveProcess = await _documentServices.SelectListApproveProcess();
            return View(typeDocument);
        }

        // POST: TypeDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeDocumentId,Name,IsActive,CreateOn,ApproveProcessId")] TypeDocument typeDocument)
        {
            ViewBag.ApproveProcess = await _documentServices.SelectListApproveProcess();
            if (ModelState.IsValid)
            {
                _db.Entry(typeDocument).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeDocument);
        }

        // GET: TypeDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeDocument typeDocument = _db.TypeDocuments.Find(id);
            if (typeDocument == null)
            {
                return HttpNotFound();
            }
            return View(typeDocument);
        }

        // POST: TypeDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeDocument typeDocument = _db.TypeDocuments.Find(id);
            _db.TypeDocuments.Remove(typeDocument);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
