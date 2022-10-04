using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities
{
    public class ApproveProcess : BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApproveProcessId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<TypeDocument> TypeDocuments { get; set; }
    }
}
