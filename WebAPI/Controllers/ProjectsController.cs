using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;

namespace WebAPI.Controllers
{
    public class ProjectsController : ApiController
    {
        DBHelper DBHelper = new DBHelper();

        // List of Projects
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<ProjectDetail> ListProject()
        {
            return DBHelper.GetAllProjectsIdName();
        }

        // Get Project by it  ID
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ProjectDetail GetProject(int id)
        {
            return DBHelper.GetProjectDetail(id);
        }

        // Add Project
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public HttpResponseMessage AddProject(ProjectDetail project)
        {

            if (project.ProjectName.Length < 3)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (project.StartDate == null || project.EndDate == null 
                || project.StartDate.Date > project.EndDate.Date)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (project.StartDate.Date == project.EndDate.Date)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            ProjectDetail projectDetail = new ProjectDetail()
            {
                ProjectName = project.ProjectName,
                EndDate = project.EndDate,
                StartDate = project.StartDate
            };

            DBHelper.AddProject(projectDetail);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // Update Project
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public HttpResponseMessage UpdateProject(int? id, ProjectDetail project)
        {

            if(id == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (project.ProjectName.Length < 3)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (project.StartDate == null || project.EndDate == null
                || project.StartDate.Date > project.EndDate.Date)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (project.StartDate.Date == project.EndDate.Date)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            DBHelper.UpdateProject(project);
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        // Delete Project
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public HttpResponseMessage DeleteProject(int? id)
        {
            if (id == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            DBHelper.DeleteProject(id.Value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        

    }
}
