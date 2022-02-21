using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationMVC.Models
{
    public class ProjectAttendanceDetailsModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int ProjectId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfRequest { get; set; }
        [Required(ErrorMessage = "Please select a Project Name")]
        public string ProjectName { get; set; }
        public string AttendanceStatus { get; set; }
    }
}