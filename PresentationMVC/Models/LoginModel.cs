using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationMVC.Models
{
    public class LoginModel
    {

        [Display(Name = "Employee Id")]
        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeID { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Password { get; set; }

    }
}