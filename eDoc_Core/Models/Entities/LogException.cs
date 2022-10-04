using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities
{
    /// <summary>
    /// Bảng này lưu toàn bộ lỗi ở Server
    /// </summary>
    public class LogException:BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int LogExceptionId { get; set; }
        [StringLength(500)]
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
