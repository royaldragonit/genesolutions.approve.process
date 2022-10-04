using eDoc_Core.Models.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities.StoredProcedures
{
    public class GS_GetListDocumentByEmail : vDocument
    {
        public bool IsApprove { get; set; }
        public short StateApprove { get; set; }
        public string DescriptionApprove { get; set; }
        public string Process { get; set; }
    }
}
