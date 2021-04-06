using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MIS421_FinalProject.Models
{
    public class Employee
    {
        [Key]
        public int empID { get; set; }

        [Required]
        public string empFName { get; set; }

        [Required]
        public string empLName { get; set; }

        [DataType(DataType.Date)]
        public DateTime empBDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime empHireDate { get; set; }
        public  string empPhone { get; set; }
        public string empAddress { get; set; }
        public string empCity { get; set; }
        public string empZip { get; set; }


        //Foreign key to Department
        public virtual Department Department { get; set; }
    }
}
