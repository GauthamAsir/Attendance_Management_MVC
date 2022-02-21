using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProjectAttendanceDetails
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Project Id")]
        public int ProjectId { get; set; }

        [Display(Name = "Date Of Request")]
        public DateTime DateOfRequest { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Attendance Status")]
        public string AttendanceStatus { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
