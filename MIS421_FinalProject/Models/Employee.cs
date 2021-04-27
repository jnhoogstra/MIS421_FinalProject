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
    public class Employee
    {
        [Key]
        public int empID { get; set; }

        //Employee Personal Information
        [Required(ErrorMessage = "Field is required.")]
        public string empFName { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empLName { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        [DataType(DataType.Date)]
        public DateTime empBDate { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public int empSSN { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empGender { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empMaritalStatus { get; set; }

        [DataType(DataType.Upload)]
        [DisplayName("Profile Picture")]
        public byte[] ProfilePic { get; set; }


        //employee contact inforamtion
        [Required(ErrorMessage = "Field is required.")]
        public string empPhone { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empEmail { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empAddress { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empCity { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empCountry { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empState { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public string empZip { get; set; }


        //Additional employee data
        [Required(ErrorMessage = "Field is required.")]
        [DataType(DataType.Date)]
        public DateTime empHireDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime empEndDate { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public bool empActive { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public float empSalary { get; set; }

        //Foreign key to Department
        [Required]
        public int deptID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }
    }
}
