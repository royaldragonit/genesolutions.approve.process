using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.DocumentModels
{
    public class GS_SummaryDocument
    {
        public int TotalDocument { get; set; }
        public int WaitForProcessDocument { get; set; }
        public int ApproveDocument { get; set; }
        public int RejectDocument { get; set; }
    }
}
