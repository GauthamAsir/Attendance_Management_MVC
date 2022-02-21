using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using PresentationMVC.Models;

namespace PresentationMVC.Controllers
{
    public class ProjectController : Controller
    {
        Business Business = new Business();

        // GET: Project
        public async Task<ActionResult> Index(string search)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            List<ProjectDetail> projects = await Business.GetAllProjectsIdName(accessToken);

            List<ProjectDetail> filteredProjects = projects;

            if (!string.IsNullOrEmpty(search))
            {
                string searchText = search.ToLower();

                filteredProjects = projects.FindAll(p =>
                p.ProjectId.ToString().Contains(searchText)
                || p.ProjectName.ToLower().Contains(searchText));
            }

            return View(filteredProjects);
        }

        // GET: Project/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            ProjectDetail projectDetail = await Business.GetProjectDetail(accessToken, id);
            List<EmployeeDetail> employeeDetails = await Business.ListEmployeesInProject(accessToken, id);

            ViewBag.EmployeeDetails = employeeDetails;

            return View(new ProjectDetailsModel()
            {

                ProjectId = projectDetail.ProjectId,
                EndDate = projectDetail.EndDate,
                StartDate = projectDetail.StartDate,
                ProjectName = projectDetail.ProjectName,

            });
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProjectDetailsModel projectDetail)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            try
            {
                string accessToken = Session["token"].ToString();
                if (!ModelState.IsValid)
                {
                    return View(projectDetail);
                }

                bool res = await Business.AddProject(accessToken, new ProjectDetail()
                {

                    ProjectId = projectDetail.ProjectId,
                    ProjectName = projectDetail.ProjectName,
                    StartDate = projectDetail.StartDate,
                    EndDate = projectDetail.EndDate
                });

                if (res)
                {
                    return RedirectToAction("Index");
                }

                return HttpNotFound();
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: Project/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            ProjectDetail projectDetail = await Business.GetProjectDetail(accessToken, id);
            return View(new ProjectDetailsModel()
            {

                ProjectId = projectDetail.ProjectId,
                EndDate = projectDetail.EndDate,
                StartDate = projectDetail.StartDate,
                ProjectName = projectDetail.ProjectName,

            });
        }

        // POST: Project/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProjectDetailsModel projectDetail)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            try
            {
                string accessToken = Session["token"].ToString();

                if (!ModelState.IsValid)
                {
                    return View(projectDetail);
                }

                bool res = await Business.UpdateProject(accessToken, new ProjectDetail()
                {

                    ProjectId = projectDetail.ProjectId,
                    ProjectName = projectDetail.ProjectName,
                    StartDate = projectDetail.StartDate,
                    EndDate = projectDetail.EndDate
                });

                if (res)
                {
                    return RedirectToAction("Index");
                }

                return HttpNotFound();
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            ProjectDetail projectDetail = await Business.GetProjectDetail(accessToken, id);
            return View(projectDetail);
        }

        // POST: Project/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, ProjectDetail project)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            try
            {
                string accessToken = Session["token"].ToString();
                bool res = await Business.DeleteProject(accessToken, id);

                if (res)
                    return RedirectToAction("Index");

                return HttpNotFound();
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public async Task<ActionResult> RemoveEmployeeFromProject(int id, int projectid)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            bool res = await Business.RemoveEmployeeFromProject(accessToken, id, projectid);
            return RedirectToAction("Details/" + projectid);
        }

        public async Task<ActionResult> AddEmployeeToProject(int id, string search)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            List<EmployeeDetail> employees = await Business.ListEmployeesNotInProject(accessToken, id);
            ViewBag.ProjectId = id;

            List<EmployeeDetail> filteredEmployees = employees;

            if (!string.IsNullOrEmpty(search))
            {
                string searchText = search.ToLower();

                filteredEmployees = employees.FindAll(e =>
                e.EmployeeId.ToString().Contains(searchText)
                || e.FirstName.ToLower().Contains(searchText) ||
               e.LastName.ToLower().Contains(searchText));
            }

            return View(filteredEmployees);
        }

        public async Task<ActionResult> AssignEmployeeToProject(int id, int projectid)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            ProjectDetail projectDetail = await Business.GetProjectDetail(accessToken, projectid);

            bool res = await Business.AddEmployeeToProject(accessToken, new ProjectAttendance()
            {
                EmployeeId = id,
                ProjectId = projectid,
                DateOfAttendance = DateTime.Now,
                EndDate = projectDetail.EndDate
            });

            if (res)
            {
                ViewBag.Message = "Employee Added Successfuly";
            }

            return RedirectToAction("AddEmployeeToProject/" + projectid);
        }

    }
}
