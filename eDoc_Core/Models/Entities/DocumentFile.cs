using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Models.Entities
{
    public class DocumentFile:BaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DocumentFileId { get; set; }
        [Required]
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
        [StringLength(200)]
        public string FileName { get; set; }
        [StringLength(200)]
        public string FileNameOriginal { get; set; }
        [StringLength(300)]
        public string FileNameInServer { get; set; }
        public Document Document { get; set; }
        public bool IsPrimaryFile { get; set; }
    }
}
