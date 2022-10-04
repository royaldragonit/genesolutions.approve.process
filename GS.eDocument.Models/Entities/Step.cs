using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.eDocument.Models.Entities
{
    public class Step : BaseEntities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StepId { get; set; }
        public int UserId { get; set; }
        public int RollBackToStep { get; set; }
        [ForeignKey("ApproveProcess")]
        public int ApproveProcessId { get; set; }
        public ApproveProcess ApproveProcess { get; set; }
        public virtual ICollection<Approve> Approves { get; set; }
    }
}
