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
        public int deptID { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string deptName { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string deptAddress { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string deptCity { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string deptCountry { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string deptState { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string deptZip { get; set; }
    }
}
