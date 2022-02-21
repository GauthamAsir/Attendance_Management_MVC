using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DBHelper
    {

        AttendanceManagementEntities db = new AttendanceManagementEntities();

        public List<ProjectDetail> GetAllProjectsIdName()
        {

            List<ProjectDetail> projects = new List<ProjectDetail>();

            var list = db.sp_GetProjectName();

            list.ToList().ForEach(p =>
            {
                projects.Add(new ProjectDetail() { ProjectId = p.ProjectId, ProjectName = p.ProjectName });
            });

            return projects;
        }

        public ProjectDetail GetProjectDetail(int id)
        {
            var res = db.sp_GetDateForProject(id).ToList().FirstOrDefault();

            return new ProjectDetail()
            {
                ProjectId = id,
                ProjectName = res.ProjectName,
                StartDate = res.StartDate,
                EndDate = res.EndDate
            };
        }

        public void AddProject(ProjectDetail projectDetail)
        {

            System.Data.Entity.Core.Objects.ObjectParameter success =
                new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));

            db.sp_AddProject(projectname: projectDetail.ProjectName,
               startdate: projectDetail.StartDate, enddate: projectDetail.EndDate, success);

        }

        public void UpdateProject(ProjectDetail projectDetail)
        {

            System.Data.Entity.Core.Objects.ObjectParameter success =
                new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));

            db.sp_UpdateProject(id: projectDetail.ProjectId,
                projectname: projectDetail.ProjectName,
               startdate: projectDetail.StartDate, enddate: projectDetail.EndDate, success);

        }

        public void DeleteProject(int id)
        {
            ProjectDetail project = db.ProjectDetails.ToList()
                .Find(p => p.ProjectId == id);

            System.Data.Entity.Core.Objects.ObjectParameter success =
                new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));

            db.sp_DeleteProject(id: id, success);
        }

        public void AddEmployeeToProject(ProjectAttendance projectAttendance)
        {
            System.Data.Entity.Core.Objects.ObjectParameter success =
                new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));

            db.sp_AssignProject(
                employeeid: projectAttendance.EmployeeId,
                projectid: projectAttendance.ProjectId,
                startdate: projectAttendance.DateOfAttendance,
                enddate: projectAttendance.EndDate, success);


        }

        public void RemoveEmployeeFromProject(int empid, int projectid)
        {

            db.sp_RemoveEmployeeFromAssignedProject(employeeid: empid,
                projectid: projectid);

        }

        public List<EmployeeDetail> ListEmployeesFromProject(int projectId)
        {

            List<EmployeeDetail> employees = new List<EmployeeDetail>();

            var emps = db.sp_GetEmployeeDetailsForProject(projectId);

            emps.ToList().ForEach(e =>
            {
                employees.Add(new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.EmployeeName,
                    LastName = e.EmployeeName,
                    Email = e.Email,
                    ContactNo = e.ContactNo
                });
            });

            return employees;

        }

        public List<EmployeeDetail> ListEmployeesNotInProject(int projectId)
        {
            List<EmployeeDetail> employees = new List<EmployeeDetail>();

            var emps = db.sp_AssignEmployees(projectId);

            emps.ToList().ForEach(e =>
            {
                employees.Add(new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.EmployeeName,
                    LastName = e.EmployeeName,
                    ContactNo = e.ContactNo,
                    JobTitle = e.JobTitle
                });
            });

            return employees;
        }

        public List<ProjectAttendanceDetails> EmployeeViewAttendanceList(int id)
        {
            var proc = db.sp_EmployeeViewAttendance(id);
            List<ProjectAttendanceDetails> pa1 = new List<ProjectAttendanceDetails>();
            proc.ToList().ForEach(p => pa1.Add(new ProjectAttendanceDetails()
            {
                EmployeeId = id,
                ProjectId = p.ProjectId,
                DateOfRequest = p.DateOfRequest.Value,
                ProjectName = p.ProjectName,
                AttendanceStatus = p.AttendanceStatus,
            }));
            return pa1;
        }
        public List<ProjectDetail> GetProjectOfEmployees(int id)
        {
            var proc = db.sp_GetProjectOfEmployee(id);
            List<ProjectDetail> pa1 = new List<ProjectDetail>();
            proc.ToList().ForEach(p => pa1.Add(new ProjectDetail()
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                EndDate = p.EndDate
            }));
            return pa1;
        }
        public bool AddAttendance(ProjectAttendance pa)
        {
            if (pa != null)
            {
                db.sp_AddAttendance(pa.EmployeeId, pa.ProjectId);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public int DeleteAttendance(int? empid, int? projectid)
        {
            if (empid != null && projectid != null)
            {
                System.Data.Entity.Core.Objects.ObjectParameter success =
                    new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));
                db.sp_DeleteAttendance(empid.Value, projectid.Value, success);
                db.SaveChanges();
                return Convert.ToInt32(success.Value);
            }
            return 0;
        }
        public int IsAttendanceMarked(int? empid, int? projectid)
        {
            System.Data.Entity.Core.Objects.ObjectParameter status =
                    new System.Data.Entity.Core.Objects.ObjectParameter("status", typeof(int));
            db.sp_IsAttendanceMarked(empid, projectid, DateTime.Now, status);
            return Convert.ToInt32(status.Value);
        }
        public List<ProjectAttendanceDetails> ManagerViewAttendanceList(int id)
        {
            var proc = db.sp_ManagerViewPendingAttendanceRequest(id);
            List<ProjectAttendanceDetails> pa1 = new List<ProjectAttendanceDetails>();
            proc.ToList().ForEach(p => pa1.Add(new ProjectAttendanceDetails()
            {
                EmployeeId = p.EmployeeId,
                EmployeeName = p.EmployeeName,
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
            }));
            return pa1;
        }
        public bool UpdateAttendance(ProjectAttendance pa)
        {
            if (pa != null)
            {
                db.sp_UpdateAttendance(pa.EmployeeId, pa.ProjectId, pa.AttendanceStatus);
                return true;
            }
            return false;
        }
        public Object AdminViewAttendanceList()
        {
            return db.sp_AdminViewPendingAttendanceRequest();
        }

        public static string SHA2_512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return "0x" + hashedInputStringBuilder.ToString();
            }
        }

        public LoginCredential ValidateUser(LoginCredential login)
        {

            //var p = db.LoginCredentials.FirstOrDefault
            //    (user => user.EmployeeId == login.EmployeeId && user.LoginPassword == SHA2_512(login.LoginPassword));

            //return p;

            System.Data.Entity.Core.Objects.ObjectParameter status =
                    new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));

            System.Data.Entity.Core.Objects.ObjectParameter roleId =
                    new System.Data.Entity.Core.Objects.ObjectParameter("roleid", typeof(int));

            System.Data.Entity.Core.Objects.ObjectParameter employeeName =
                    new System.Data.Entity.Core.Objects.ObjectParameter("empName", typeof(string));

            db.sp_login(login.EmployeeId, login.LoginPassword, status, roleId, employeeName);

            int roleIdVal = Convert.ToInt32(roleId.Value);

            if (Convert.ToInt32(status.Value) == 1)
            {
                return new LoginCredential()
                {
                    EmployeeId = login.EmployeeId,
                    LoginPassword = login.LoginPassword,
                    EmployeeDetail = new EmployeeDetail()
                    {
                        RoleId = roleIdVal,
                        EmployeeId = login.EmployeeId.Value,
                        FirstName = Convert.ToString(employeeName.Value),
                        RoleDetail = new RoleDetail() {
                            RoleName = roleIdVal == 1 ? "Admin" : roleIdVal == 2 ? "Manager" : "Employee"
                        }
                    }
                };
            }

            return null;
        }
        public List<EmployeeDetail> GetAllEmployees()
        {
            var proc = db.sp_GetEmployeeDetails();
            List<EmployeeDetail> employees = new List<EmployeeDetail>();
            proc.ToList().ForEach(e => employees.Add(new EmployeeDetail()
            {
                EmployeeId = e.EmployeeId,
                ContactNo = e.ContactNo,
                DateOfBirth = e.DateOfBirth,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                JobTitle = e.JobTitle,
                ManagerId = e.ManagerId,
                RoleId = e.RoleId

            }));
            return employees;
        }
        public bool AddEmployee(EmployeeDetail employee)
        {
            AttendanceManagementEntities db = new AttendanceManagementEntities();
            if (employee != null)
            {
                db.EmployeeDetails.Add(employee);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public EmployeeDetail GetEmployee(int id)
        {
            var emp = db.EmployeeDetails.ToList().Find(e => e.EmployeeId == id);
            return new EmployeeDetail()
            {
                EmployeeId = emp.EmployeeId,
                ContactNo = emp.ContactNo,
                DateOfBirth = emp.DateOfBirth,
                Email = emp.Email,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                JobTitle = emp.JobTitle,
                ManagerId = emp.ManagerId,
                RoleId = emp.RoleId
            };
        }

        public bool UpdateEmployeeInfo(int id, EmployeeDetail employee)
        {
            EmployeeDetail emp = db.EmployeeDetails.ToList().Find(e => e.EmployeeId == id);
            if (emp != null)
            {
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.ContactNo = employee.ContactNo;
                emp.Email = employee.Email;
                emp.JobTitle = employee.JobTitle;
                emp.ManagerId = employee.ManagerId;
                emp.RoleId = employee.RoleId;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeleteEmployeeInfo(int id)
        {
            EmployeeDetail employee = db.EmployeeDetails.ToList().Find(e => e.EmployeeId == id);
            if (employee != null)
            {
                db.EmployeeDetails.Remove(employee);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Object GetRemainingLeaves(int id)
        {
            return db.sp_GetRemainingLeave(id);
        }

        public static int GetWorkingDays(DateTime from, DateTime to)
        {
            var totalDays = 0;
            for (var date = from; date <= to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday)
                    totalDays++;
            }
            return totalDays;
        }
        public int RequestLeave(LeaveTransactionDetail leaveTransaction)
        {
            if (leaveTransaction != null)
            {
                System.Data.Entity.Core.Objects.ObjectParameter success = new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));
                int no_of_days = GetWorkingDays(leaveTransaction.StartDate, leaveTransaction.EndDate);
                db.sp_RequestLeave(leaveTransaction.EmployeeId, leaveTransaction.StartDate, leaveTransaction.EndDate, no_of_days, leaveTransaction.Reason, success);
                db.SaveChanges();
                return Convert.ToInt32(success.Value);
            }
            return 0;

        }

        public int UpdateLeave(LeaveTransactionDetail leaveTransaction)
        {
            if (leaveTransaction != null)
            {
                System.Data.Entity.Core.Objects.ObjectParameter success = new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));
                int no_of_days = GetWorkingDays(leaveTransaction.StartDate, leaveTransaction.EndDate);
                db.sp_UpdateRequestLeave(leaveTransaction.TransactionId, leaveTransaction.EmployeeId, leaveTransaction.StartDate, leaveTransaction.EndDate, no_of_days, leaveTransaction.Reason, success);
                db.SaveChanges();
                return Convert.ToInt32(success.Value);
            }
            return 0;
        }

        public int DeleteLeave(int Transid)
        {
            System.Data.Entity.Core.Objects.ObjectParameter success = new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));
            db.sp_DeleteLeaveRequest(Transid, success);
            db.SaveChanges();
            return Convert.ToInt32(success.Value);
        }

        public List<LeaveTransactionDetail> ViewLeave(int id)
        {
            var proc = db.sp_EmployeeViewLeave(id);
            List<LeaveTransactionDetail> leaves = new List<LeaveTransactionDetail>();
            proc.ToList().ForEach(p => leaves.Add(new LeaveTransactionDetail()
            {
                EmployeeId = id,
                TransactionId = p.TransactionId,
                DateOfRequest = p.DateOfRequest,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                TransactionStatus = p.TransactionStatus,
                Reason = p.Reason
            }));
            return leaves;
        }

        public List<LeaveTransactionDetail> ManagerViewPendingLeaves(int id)
        {
            var proc = db.sp_ManagerViewPendingLeaveRequest(id);
            List<LeaveTransactionDetail> leaves = new List<LeaveTransactionDetail>();
            proc.ToList().ForEach(p => leaves.Add(new LeaveTransactionDetail()
            {
                EmployeeId = p.EmployeeId.Value,
                TransactionId = p.TransactionId,
                EmployeeName = p.Employee_Name,
                DateOfRequest = p.Date_of_Request.Value,
                StartDate = p.Start_Date.Value,
                EndDate = p.End_Date.Value,
                Reason = p.Reason
            }));
            return leaves;
        }

        public List<LeaveTransactionDetail> AdminViewPendingLeaves()
        {
            var proc = db.sp_AdminViewPendingLeaveRequest();
            List<LeaveTransactionDetail> leaves = new List<LeaveTransactionDetail>();
            proc.ToList().ForEach(p => leaves.Add(new LeaveTransactionDetail()
            {
                EmployeeId = p.EmployeeId.Value,
                TransactionId = p.TransactionId,
                EmployeeName = p.Employee_Name,
                DateOfRequest = p.Date_of_Request.Value,
                StartDate = p.Start_Date.Value,
                EndDate = p.End_Date.Value,
                Reason = p.Reason
            }));
            return leaves;
        }

        public bool UpdateLeaveStatus(LeaveTransactionDetail leaveTransaction)
        {
            if (leaveTransaction.TransactionStatus != null)
            {
                db.sp_UpdateLeaveStatus(leaveTransaction.TransactionId, leaveTransaction.TransactionStatus);
                db.SaveChanges();
                return true;
            }
            return false;

        }

        public List<sp_AdminViewAllAttendance_Result> AdminViewAllAttandance(Nullable<DateTime> StartDate, Nullable<DateTime> EndDate)
        {
            AttendanceManagementEntities db = new AttendanceManagementEntities();
            var proc = db.sp_AdminViewAllAttendance(StartDate, EndDate);
            List<sp_AdminViewAllAttendance_Result> pal = new List<sp_AdminViewAllAttendance_Result>();
            proc.ToList().ForEach(p => pal.Add(new sp_AdminViewAllAttendance_Result()
            {
                EmployeeId = p.EmployeeId,
                EmployeeName = p.EmployeeName,
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                DateOfAttendance = p.DateOfAttendance,


            }));
            return pal;
        }

        public List<sp_AdminViewAllLeave_Result> AdminViewAllLeaves()
        {
            AttendanceManagementEntities db = new AttendanceManagementEntities();
            var proc = db.sp_AdminViewAllLeave();
            List<sp_AdminViewAllLeave_Result> leaves = new List<sp_AdminViewAllLeave_Result>();

            proc.ToList().ForEach(p => leaves.Add(new sp_AdminViewAllLeave_Result()
            {
                EmployeeId = p.EmployeeId,
                Employee_Name = p.Employee_Name,
                Start_Date = p.Start_Date,
                End_Date = p.End_Date,
                NoOfDays = p.NoOfDays,
                Reason = p.Reason,

            }));
            return leaves;

        }

        public List<KeyValuePair<string, string>> SignUp(SignUpModel signup)
        {
            System.Data.Entity.Core.Objects.ObjectParameter success =
                new System.Data.Entity.Core.Objects.ObjectParameter("success", typeof(int));
            System.Data.Entity.Core.Objects.ObjectParameter id =
                new System.Data.Entity.Core.Objects.ObjectParameter("id", typeof(int));

            db.sp_AddEmployee(firstname: signup.FirstName,
                              lastname: signup.LastName,
                              dob: signup.DateOfBirth,
                              contact: signup.ContactNo,
                              email: signup.Email,
                              job: signup.JobTitle,
                              managerid: null,
                              role: 3,
                              success,
                              id);

            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();

            keys.Add(new KeyValuePair<string, string>("status", success.Value.ToString()));

            if (success.Value.ToString() == "1")
            {
                int eid = Convert.ToInt32(id.Value.ToString());
                db.sp_AddEmpidandPassword(eid, password: signup.Password);
                if (success.Value.ToString() == "1")
                {
                    keys.Add(new KeyValuePair<string, string>("eid", eid.ToString()));
                    return keys;
                }
            }
            return keys;
        }

        public DateTime? GetLastRefreshedDate(int empid)
        {
            System.Data.Entity.Core.Objects.ObjectParameter lastDate =
                new System.Data.Entity.Core.Objects.ObjectParameter("lastdate", typeof(DateTime));

            db.sp_GetLastRefreshedDate(empid, lastDate);
            return Convert.ToDateTime(lastDate.Value);
        }

        public void AddMonthlyLeaves(int empid)
        {
            db.sp_addleaveeverymonth(empid);
        }

        public List<EmployeeDetail> GetAllManagers()
        {
            var emps = db.sp_GetManagerName();

            List<EmployeeDetail> employeeDetails = new List<EmployeeDetail>();

            emps.ToList().ForEach(e => employeeDetails.Add(
                new EmployeeDetail()
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                }));
            return employeeDetails;
        }

    }
}
