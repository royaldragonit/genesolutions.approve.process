using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace eDoc_Core.Models.Entities
{
    public class TypeDocument : BaseEntities
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TypeDocumentId { get; set; }
        [Required]
        [StringLength(300)]
        [DisplayName("Tên tài liệu")]
        public string Name { get; set; }
        [ForeignKey("ApproveProcess")]
        public int ApproveProcessId { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ApproveProcess ApproveProcess { get; set; }
    }
}
