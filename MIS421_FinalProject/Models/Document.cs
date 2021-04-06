using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MIS421_FinalProject.Models
{
    public class Document
    {
        [Key]
        public Guid docID { get; set; }

        [Required]
        public string docName { get; set; }
        public DateTime uploadDate { get; set; }
        public bool verified { get; set; }

        [DataType(DataType.Upload)]
        [DisplayName("File")]
        public byte[] content { get; set; }

        //Foreign key to Employee
        public virtual Employee Employee { get; set; }
    }
}
