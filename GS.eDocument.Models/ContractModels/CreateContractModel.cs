using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.eDocument.Models.ContractModels
{
    public class CreateContractModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Files { get; set; }
    }
}
