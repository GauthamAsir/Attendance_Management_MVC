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

    public partial class LoginCredential
    {
        [Display(Name = "Employee ID")]
        public Nullable<int> EmployeeId { get; set; }

        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
    }
}