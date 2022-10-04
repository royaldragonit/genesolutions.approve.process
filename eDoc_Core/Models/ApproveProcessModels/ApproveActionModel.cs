using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.ApproveProcessModels
{
    public class ApproveActionModel
    {
        public int DocumentId { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
