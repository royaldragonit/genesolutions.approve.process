using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GS.eDocument.Models.ApproveProcessModels
{
    public class ApproveProcessRequestModel
    {
        public string Email { get; set; }
        [Required]
        [Description("Tên mẫu quy trình (Process)")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StepModel> Steps { get; set; }
    }
}