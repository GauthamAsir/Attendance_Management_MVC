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
    
    public partial class sp_SearchEmployeeDetails_Result
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public Nullable<int> RoleId { get; set; }
    }
}
