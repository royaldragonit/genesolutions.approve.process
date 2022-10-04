using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.eDocument.Models.Entities
{
    /// <summary>
    /// Danh sách từng người Approve trong bước x
    /// </summary>
    public class Approve : BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ApproveId { get; set; }
        [Required]
        [ForeignKey("Step")]
        public int StepId { get; set; }
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public Step Step { get; set; }
        public bool IsApprove { get; set; }
    }
}
