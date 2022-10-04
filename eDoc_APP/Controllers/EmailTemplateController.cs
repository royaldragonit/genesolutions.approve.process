using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eDoc_Core.Models.Entities;

namespace eDoc_APP.Controllers
{
    public class EmailTemplateController : BaseController
    {
        private readonly eDocumentContext _db;

        public EmailTemplateController(eDocumentContext db) : base(db)
        {
            _db= db;
        }

        // GET: EmailTemplate
        public ActionResult Index()
        {
            return View(_db.EmailTemplates.ToList());
        }
        public ActionResult ListParams()
        {
            return View(_db.ParamEmailTemplates.Include(x=>x.EmailTemplate).ToList());
        }
        public ActionResult CreateParam()
        {
            ViewBag.EmailTemplateId = new SelectList(_db.EmailTemplates, "EmailTemplateId", "Content");
            return View();
        }
        [HttpPost]
        public ActionResult CreateParam([Bind(Include = "EmailTemplateId,ParamName,ParamValue,Description")] ParamEmailTemplate emailTemplate)
        {
            if (ModelState.IsValid)
            {
                emailTemplate.IsActive = true;
                emailTemplate.CreateOn = DateTime.Now;
                _db.ParamEmailTemplates.Add(emailTemplate);
                _db.SaveChanges();
                return RedirectToAction("ListParams");
            }

            return View(emailTemplate);
        }
        // GET: EmailTemplate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailTemplate emailTemplate = _db.EmailTemplates.Find(id);
            if (emailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(emailTemplate);
        }

        // GET: EmailTemplate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailTemplate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "EmailTemplateId,Content,Title,Email")] EmailTemplate emailTemplate)
        {
            if (ModelState.IsValid)
            {
                emailTemplate.IsActive = true;
                emailTemplate.CreateOn= DateTime.Now;
                emailTemplate.Email=GetEmail();
                _db.EmailTemplates.Add(emailTemplate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailTemplate);
        }

        // GET: EmailTemplate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailTemplate emailTemplate = _db.EmailTemplates.Find(id);
            if (emailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(emailTemplate);
        }

        // POST: EmailTemplate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "EmailTemplateId,Content,Title,Email")] EmailTemplate emailTemplate)
        {
            if (ModelState.IsValid)
            {
                emailTemplate.Email = GetEmail();
                _db.Entry(emailTemplate).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailTemplate);
        }

        // GET: EmailTemplate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailTemplate emailTemplate = _db.EmailTemplates.Find(id);
            if (emailTemplate == null)
            {
                return HttpNotFound();
            }
            return View(emailTemplate);
        }

        // POST: EmailTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailTemplate emailTemplate = _db.EmailTemplates.Find(id);
            _db.EmailTemplates.Remove(emailTemplate);
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
