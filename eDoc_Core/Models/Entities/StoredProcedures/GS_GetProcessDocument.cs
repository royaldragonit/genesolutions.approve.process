using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities.StoredProcedures
{
    public class GS_GetProcessDocument
    {
        public int StepIndex { get; set; }
        public int Step { get; set; }
        public short StateApprove { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
    public class GS_Test
    {
        public int P1 { get; set; }
        public string P2 { get; set; }
    }
}
