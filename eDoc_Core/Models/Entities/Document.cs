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
    public class Document:BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DocumentId { get; set; }
        [Required]
        [ForeignKey("TypeDocument")]
        public int TypeDocumentId { get; set; }
        public int ApproveProcessId { get; set; }
        [Required]
        public string Description { get; set; }
        [Description("Người tạo ra Document này")]
        public string Email { get; set; }
        public int Step { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsApprove { get; set; }
        public TypeDocument TypeDocument { get; set; }
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
        public virtual ICollection<ApproveDocument> ApproveDocuments { get; set; }
    }
}
