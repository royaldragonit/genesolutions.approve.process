using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities.Views
{
    public class vApproveProcess
    {
        public int ApproveProcessId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int Step { get; set; }
    }
}
