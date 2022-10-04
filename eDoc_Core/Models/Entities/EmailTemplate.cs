using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eDoc_Core.Models.Entities
{
    public class EmailTemplate:BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int EmailTemplateId { get; set; }
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [DisplayName("Nội dung")]
        [AllowHtml]
        public string Content { get; set; }
        [DisplayName("Người tạo")]
        public string Email { get; set; }
        public ICollection<ParamEmailTemplate> ParamEmailTemplates { get; set; }
    }
}
