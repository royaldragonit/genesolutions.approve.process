using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities
{
    /// <summary>
    /// Bảng này lưu những người Approve của Document
    /// </summary>
    public class ApproveDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ApproveDocumentId { get; set; }
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        public int StepId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int StepIndex { get; set; }
        /// <summary>
        /// 0: chưa Approve, 1: Approve, 2: Reject
        /// </summary>
        public short StateApprove { get; set; }
        public Document Document { get; set; }

    }
}
