using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MIS421_FinalProject.Models
{
    public class Document
    {
        [Key]
        public Guid docID { get; set; }

        [Display(Name = "File Name")]
        public string docName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Uploaded")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime uploadDate { get; set; }

        [Display(Name = "Size (bytes)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public long Size { get; set; }

        public bool verified { get; set; }

        [DataType(DataType.Upload)]
        [DisplayName("File")]
        public byte[] content { get; set; }




        //Foreign key to Employee
        [Required]
        public int empID { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

    }
}
