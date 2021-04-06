using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MIS421_FinalProject.Models
{
    public class Department
    {
        [Key]
        public Guid deptID { get; set; }

        [Required]
        public string deptName { get; set; }
        [Required]
        public string deptAddress { get; set; }
        [Required]
        public string deptCity { get; set; }
        [Required]
        public string deptCountry { get; set; }

        
        public string deptState { get; set; }
        public string deptZip { get; set; }
    }
}
