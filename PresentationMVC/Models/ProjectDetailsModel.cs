using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationMVC.Models
{
    public class ProjectDetailsModel
    {
        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Project Name is required")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "Project Name should be greater than length 3")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}