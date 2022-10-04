using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities
{
    public class BaseEntities
    {
        public bool IsActive { get; set; } = true;
        [DisplayName("Ngày tạo")]
        public DateTime CreateOn { get; set; } = DateTime.Now;
    }
}
