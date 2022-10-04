using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.eDocument.Models.Entities
{
    public class BaseEntities
    {
        public bool IsActive { get; set; } = true;
        public DateTime CreateOn { get; set; } = DateTime.Now;
    }
}
