using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using Newtonsoft.Json;

namespace PresentationMVC.Controllers
{
    public class EmployeeController : Controller
    {
        Business Business = new Business();

        List<RoleDetail> roleDetails = new List<RoleDetail>()
        {
                new RoleDetail(){RoleId=2,RoleName="Manager"},
                new RoleDetail(){RoleId=3,RoleName="Employee"}
        };
        public async Task<ActionResult> Index(string search)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            List<EmployeeDetail> employees = await Business.GetAllEmployees(accessToken);

            List<EmployeeDetail> filteredEmployees = employees;

            if (!string.IsNullOrEmpty(search))
            {
                string searchText = search.ToLower();

                filteredEmployees = employees.FindAll(e =>
                e.EmployeeId.ToString().Contains(searchText)
                || e.FirstName.ToLower().Contains(searchText)
                || e.LastName.ToLower().Contains(searchText));
            }

            return View(filteredEmployees);
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployee(int id)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            //string Employee = await employee.GetEmployee(Session["token"].ToString(),id);
            string Employee = await Business.GetEmployee(accessToken, id);
            EmployeeDetail emp = JsonConvert.DeserializeObject<EmployeeDetail>(Employee);

            return View(emp);
        }

        [HttpGet]
        public async Task<ActionResult> AddEmployee()
        {
            ViewBag.RoleId = new SelectList(roleDetails, "RoleId", "RoleName");

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            List<EmployeeDetail> managersList = new List<EmployeeDetail>();

            managersList = await Business.GetAllManagers(accessToken);

            List<SelectListItem> managersListItem = new List<SelectListItem>();

            managersList.ForEach(m =>
            {
                managersListItem.Add(new SelectListItem()
                {
                    Value = m.EmployeeId.ToString(),
                    Text = m.FirstName + " " + m.LastName
                });
            });

            ViewBag.Managers = managersListItem;

            List<SelectListItem> rolesList = new List<SelectListItem>();

            rolesList.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Manager"
            });

            rolesList.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Employee"
            });

            ViewBag.Roles = rolesList;

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee(EmployeeDetail employeeInfo)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            if (ModelState.IsValid)
            {

                string strEmployee = JsonConvert.SerializeObject(employeeInfo);
                //bool result = await employee.AddEmployee(Session["token"].ToString(), strEmployee);
                bool result = await Business.AddEmployee(accessToken, strEmployee);
                ViewBag.RoleId = new SelectList(roleDetails, "RoleId", "RoleName");
                if (result == true)
                {
                    return RedirectToAction("Index");
                }
            }

            List<EmployeeDetail> managersList = new List<EmployeeDetail>();

            managersList = await Business.GetAllManagers(accessToken);

            List<SelectListItem> managersListItem = new List<SelectListItem>();

            managersList.ForEach(m =>
            {
                managersListItem.Add(new SelectListItem()
                {
                    Value = m.EmployeeId.ToString(),
                    Text = m.FirstName + " " + m.LastName
                });
            });

            ViewBag.Managers = managersListItem;

            List<SelectListItem> rolesList = new List<SelectListItem>();

            rolesList.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Manager"
            });

            rolesList.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Employee"
            });

            ViewBag.Roles = rolesList;

            return View(employeeInfo);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateEmployee(int id)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();
            string Employee = await Business.GetEmployee(accessToken, id);
            EmployeeDetail emp = JsonConvert.DeserializeObject<EmployeeDetail>(Employee);

            ViewBag.RoleId = new SelectList(roleDetails, "RoleId", "RoleName", emp.RoleId);

            List<EmployeeDetail> managersList = new List<EmployeeDetail>();

            managersList = await Business.GetAllManagers(accessToken);

            List<SelectListItem> managersListItem = new List<SelectListItem>();

            managersList.ForEach(m =>
            {
                managersListItem.Add(new SelectListItem()
                {
                    Value = m.EmployeeId.ToString(),
                    Text = m.FirstName + " " + m.LastName,
                    Selected = emp.ManagerId == null ? false : m.EmployeeId == emp.ManagerId
                });
            });

            ViewBag.Managers = managersListItem;

            ViewBag.RoleViewStatus = emp.RoleId == 3;

            return View(emp);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeDetail employeeInfo)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            ViewBag.RoleId = new SelectList(roleDetails, "RoleId", "RoleName", employeeInfo.RoleId);

            if (ModelState.IsValid)
            {
                string strEmployee = JsonConvert.SerializeObject(employeeInfo);
                //bool result = await employee.UpdateEmployee(Session["token"].ToString(),id, strEmployee);
                bool result = await Business.UpdateEmployee(accessToken, id, strEmployee);
                if (result == true)
                {
                    return RedirectToAction("Index");
                }
            }

            List<EmployeeDetail> managersList = new List<EmployeeDetail>();

            managersList = await Business.GetAllManagers(accessToken);

            List<SelectListItem> managersListItem = new List<SelectListItem>();

            managersList.ForEach(m =>
            {
                managersListItem.Add(new SelectListItem()
                {
                    Value = m.EmployeeId.ToString(),
                    Text = m.FirstName + " " + m.LastName
                });
            });

            ViewBag.Managers = managersListItem;

            ViewBag.RoleViewStatus = employeeInfo.RoleId == 3;

            return View(employeeInfo);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveEmployee(int id)
        {

            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            //string Employee = await employee.GetEmployee(Session["token"].ToString(),id);
            string Employee = await Business.GetEmployee(accessToken, id);
            EmployeeDetail emp = JsonConvert.DeserializeObject<EmployeeDetail>(Employee);

            return View(emp);
        }
        [HttpPost]
        public async Task<ActionResult> RemoveEmployee(int id, EmployeeDetail emp)
        {
            if (Session["token"] == null)
            {
                return Redirect("~/");
            }

            string accessToken = Session["token"].ToString();

            //bool result = await employee.RemoveEmployee(Session["token"].ToString(), id);
            bool result = await Business.RemoveEmployee(accessToken, id);
            return RedirectToAction("Index");
        }

    }
}