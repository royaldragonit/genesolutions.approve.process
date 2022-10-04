using eDoc_APP.Services;
using eDoc_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eDoc_APP.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDocumentServices _documentServices;
        public HomeController(eDocumentContext db,IDocumentServices documentServices) : base(db)
        {
            _documentServices = documentServices;
        }

        public async Task<ActionResult> Index()
        {
            var model =await _documentServices.SummaryDocument(GetEmail());
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}