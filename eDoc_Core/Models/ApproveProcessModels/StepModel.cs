using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.ApproveProcessModels
{
    public class StepModel
    {
        public List<string> ApproveWith { get; set; }
        public int RollBack { get; set; }
        public bool IsAllAccept { get; set; }
    }
}
