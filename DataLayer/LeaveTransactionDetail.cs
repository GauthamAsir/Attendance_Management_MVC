using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LeaveTransactionDetail
    {
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }

        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Date Of Request")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfRequest { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date required")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End date is required,if 1day leave enter same value as start date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Transaction Status")]
        public string TransactionStatus { get; set; }

        [Display(Name = "Reason")]
        [Required(ErrorMessage = "Please enter a reason")]
        public string Reason { get; set; }

    }
}
