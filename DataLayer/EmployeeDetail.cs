//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EmployeeDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeDetail()
        {
            this.LeaveDetails = new HashSet<LeaveDetail>();
            this.LeaveTransactions = new HashSet<LeaveTransaction>();
            this.LoginCredentials = new HashSet<LoginCredential>();
            this.ProjectAttendances = new HashSet<ProjectAttendance>();
        }
    
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DateValidatorDB]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Contact No. is requires")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Contact No.")]
        [Display(Name = "Contact No.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [Display(Name = "Job Titile")]
        public string JobTitle { get; set; }

        [Display(Name = "Manager ID")]
        public Nullable<int> ManagerId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "Role ID")]
        public Nullable<int> RoleId { get; set; }
    
        public virtual RoleDetail RoleDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveDetail> LeaveDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveTransaction> LeaveTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoginCredential> LoginCredentials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectAttendance> ProjectAttendances { get; set; }
    }
}
