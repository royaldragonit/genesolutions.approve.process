using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities
{
    public class ParamEmailTemplate:BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ParamEmailTemplateId { get; set; }
        [StringLength(200)]
        [DisplayName("Tên tham số, ví dụ: {name}")]
        public string ParamName { get; set; }
        [DisplayName("Mô tả tham số (để làm gì)")]
        [StringLength(200)]
        public string Description { get; set; }
        [ForeignKey("EmailTemplate")]
        [DisplayName("Danh sách mẫu Email")]
        public int EmailTemplateId { get; set; }
        [StringLength(1000)]
        [DisplayName("Giá trị truyền vào cho tham số trên")]
        public string ParamValue { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
    }
}
