using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eDoc_Core.Models.DocumentModels
{
    public class CreateDocumentModel
    {
        [Required]
        public int TypeDocumentId { get; set; }
        public string Email { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public HttpFileCollectionBase Files { get; set; }
        public HttpServerUtilityBase Server { get; set; }
    }
}
