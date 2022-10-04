using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities.Views
{
    public class vDocument
    {
        public int DocumentId { get; set; }
        public int TypeDocumentId { get; set; }
        public int ApproveProcessId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string EmailRererence { get; set; }
        public string DocumentTypeName { get; set; }
        public string ApproveProcessName { get; set; }
        public int FileQuantity { get; set; }
        public int Step { get; set; }
        public short StateApprove { get; set; }
        public bool IsCompleted { get; set; }
    }
}
