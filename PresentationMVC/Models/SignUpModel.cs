using PresentationMVC.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationMVC.Models
{
    public class SignUpModel
    {

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Contact No.")]
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DateValidator]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Job Title")]
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}