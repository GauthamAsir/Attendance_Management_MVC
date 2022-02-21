using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Newtonsoft.Json;

namespace BusinessLayer
{
    public class Business
    {

        string BASEURL = "https://localhost:44339/";

        public async Task<List<ProjectDetail>> GetAllProjectsIdName(string accessToken)
        {
            List<ProjectDetail> projects = new List<ProjectDetail>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/Projects");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    projects = JsonConvert.DeserializeObject<List<ProjectDetail>>(response);
                }

                return projects;

            }

        }

        public async Task<ProjectDetail> GetProjectDetail(string accessToken, int id)
        {
            ProjectDetail projectDetail = new ProjectDetail();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/Projects/" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    projectDetail = JsonConvert.DeserializeObject<ProjectDetail>(response);
                }

                return projectDetail;

            }

        }

        public async Task<bool> AddProject(string accessToken, ProjectDetail projectDetail)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string pD = JsonConvert.SerializeObject(projectDetail);

                var stringContent = new StringContent(pD, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("api/Projects/", stringContent);

                return responseMessage.IsSuccessStatusCode;

            }
        }

        public async Task<bool> DeleteProject(string accessToken, int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.DeleteAsync("api/Projects/" + id);

                return responseMessage.IsSuccessStatusCode;

            }
        }

        public async Task<bool> UpdateProject(string accessToken, ProjectDetail projectDetail)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string pD = JsonConvert.SerializeObject(projectDetail);

                var stringContent = new StringContent(pD, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PutAsync("api/Projects/" + projectDetail.ProjectId, stringContent);

                return responseMessage.IsSuccessStatusCode;

            }
        }

        public async Task<List<EmployeeDetail>> ListEmployeesInProject(string accessToken, int id)
        {
            List<EmployeeDetail> employeeDetails = new List<EmployeeDetail>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/ProjectDetails/" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    employeeDetails = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response);
                }

                return employeeDetails;

            }

        }

        public async Task<bool> RemoveEmployeeFromProject(string accessToken, int id, int projectid)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.DeleteAsync($"api/ProjectDetails/{id}/{projectid}");

                return responseMessage.IsSuccessStatusCode;

            }
        }

        public async Task<bool> AddEmployeeToProject(string accessToken, ProjectAttendance projectAttendance)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string pD = JsonConvert.SerializeObject(projectAttendance);

                var stringContent = new StringContent(pD, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("api/ProjectDetails/", stringContent);

                return responseMessage.IsSuccessStatusCode;

            }
        }

        public async Task<List<EmployeeDetail>> ListEmployeesNotInProject(string accessToken, int id)
        {
            List<EmployeeDetail> employeeDetails = new List<EmployeeDetail>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/ProjectDetails/ListEmpNotInProject/" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    employeeDetails = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response);
                }

                return employeeDetails;

            }

        }


        //-------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------//

        public async Task<List<ProjectAttendanceDetails>> Index(string accessToken, int id)
        {
            List<ProjectAttendanceDetails> attendance = new List<ProjectAttendanceDetails>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("api/Attendance/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var attendanceresponse = res.Content.ReadAsStringAsync().Result;
                    attendance = JsonConvert.DeserializeObject<List<ProjectAttendanceDetails>>(attendanceresponse);
                }
            };
            return attendance;
        }

        public async Task<List<ProjectDetail>> GetProjectOfEmployees(string accessToken, int id)
        {
            List<ProjectDetail> attendance = new List<ProjectDetail>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("EmployeeProjects/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var attendanceresponse = res.Content.ReadAsStringAsync().Result;
                    attendance = JsonConvert.DeserializeObject<List<ProjectDetail>>(attendanceresponse);
                }
            };
            return attendance;
        }

        public async Task<bool> IsAttendanceMarked(string accessToken, int empid, int projectid)
        {
            bool status = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync($"Employee/AttMarked/{empid}/{projectid}");
                if (res.IsSuccessStatusCode)
                {
                    var attendanceresponse = res.Content.ReadAsStringAsync().Result;
                    status = JsonConvert.DeserializeObject<bool>(attendanceresponse);
                }
            };
            return status;
        }

        public async Task<bool> AddAttendance(string accessToken, ProjectAttendance pa)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string acc = JsonConvert.SerializeObject(pa);
                var content = new StringContent(acc, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("api/Attendance", content);
                return res.IsSuccessStatusCode;
            }
        }

        public async Task<List<KeyValuePair<string, string>>> DeleteAttendance(string accessToken, int id, int empid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.DeleteAsync($"Employee/DeleteAttendance/{empid}/{id}");
                List<KeyValuePair<string, string>> keyValuePair = new List<KeyValuePair<string, string>>();
                if (res.IsSuccessStatusCode)
                {
                    keyValuePair.Add(new KeyValuePair<string, string>("status", "200"));
                }
                else if (res.StatusCode == System.Net.HttpStatusCode.NotAcceptable)
                {
                    keyValuePair.Add(new KeyValuePair<string, string>("status", "406"));
                    keyValuePair.Add(new KeyValuePair<string, string>("error", "This is your first attendance,You cannot delete it!!"));
                }
                else
                {
                    keyValuePair.Add(new KeyValuePair<string, string>("status", res.StatusCode.ToString()));
                    keyValuePair.Add(new KeyValuePair<string, string>("error", "Something went wrong.Try again"));
                }
                return keyValuePair;
            };
        }
        public async Task<List<ProjectAttendanceDetails>> ManagerViewPendingAttendance(string accessToken, int id)
        {
            List<ProjectAttendanceDetails> attendance = new List<ProjectAttendanceDetails>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("Manager/ViewAttendance/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var attendanceresponse = res.Content.ReadAsStringAsync().Result;
                    attendance = JsonConvert.DeserializeObject<List<ProjectAttendanceDetails>>(attendanceresponse);
                }
            };
            return attendance;
        }

        public async Task<List<ProjectAttendanceDetails>> AdminViewPendingAttendance(string accessToken)
        {
            List<ProjectAttendanceDetails> attendance = new List<ProjectAttendanceDetails>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("Admin/ViewAttendance");
                if (res.IsSuccessStatusCode)
                {
                    var attendanceresponse = res.Content.ReadAsStringAsync().Result;
                    attendance = JsonConvert.DeserializeObject<List<ProjectAttendanceDetails>>(attendanceresponse);
                }
            };
            return attendance;
        }

        public async Task<bool> UpdateAttendance(string accessToken, int managerid, ProjectAttendance pa)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string acc = JsonConvert.SerializeObject(pa);
                var content = new StringContent(acc, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PutAsync($"Manager/ApproveAttendance/{managerid}", content);
                return res.IsSuccessStatusCode;
            };
        }

        public async Task<bool> AdminUpdateAttendance(string accessToken, ProjectAttendance pa)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string acc = JsonConvert.SerializeObject(pa);
                var content = new StringContent(acc, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PutAsync("Admin/ApproveAttendance", content);
                return res.IsSuccessStatusCode;
            };
        }

        //-------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------//

        public async Task<List<LeaveTransactionDetail>> ViewLeave(string accessToken, int id)
        {
            List<LeaveTransactionDetail> leave = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("ViewLeave/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    leave = JsonConvert.DeserializeObject<List<LeaveTransactionDetail>>(response);
                    return leave;
                }
            }
            return leave;
        }

        public async Task<bool> RequestLeave(string accessToken, LeaveTransactionDetail leaveTransaction)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string leave = JsonConvert.SerializeObject(leaveTransaction);
                var stringContent = new StringContent(leave, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("api/Leave", stringContent);
                return res.IsSuccessStatusCode;
            }
        }

        public async Task<bool> UpdateLeave(string accessToken, LeaveTransactionDetail leaveTransaction)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string leave = JsonConvert.SerializeObject(leaveTransaction);
                var stringContent = new StringContent(leave, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PutAsync("api/Leave", stringContent);
                return res.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteLeave(string accessToken, int Transid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.DeleteAsync("Delete/" + Transid.ToString());
                return res.IsSuccessStatusCode;
            }
        }

        public async Task<List<LeaveTransactionDetail>> ManagerViewPendingLeaves(string accessToken, int id)
        {
            List<LeaveTransactionDetail> leave = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("ManagerViewPendingLeaves/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    leave = JsonConvert.DeserializeObject<List<LeaveTransactionDetail>>(response);
                    return leave;
                }
            }
            return leave;
        }

        public async Task<List<LeaveTransactionDetail>> AdminViewPendingLeaves(string accessToken)
        {
            List<LeaveTransactionDetail> leave = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("AdminViewPendingLeaves");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    leave = JsonConvert.DeserializeObject<List<LeaveTransactionDetail>>(response);
                    return leave;
                }
            }
            return leave;
        }

        public async Task<bool> UpdateLeaveStatus(string accessToken, LeaveTransactionDetail leaveTransaction)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                string leave = JsonConvert.SerializeObject(leaveTransaction);
                var stringContent = new StringContent(leave, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PutAsync("UpdateStatus/", stringContent);
                return res.IsSuccessStatusCode;
            }
        }

        public async Task<string> GetRemainingLeaves(string accessToken, int id)
        {
            string response = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage res = await client.GetAsync("GetRemainingLeaves/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    response = res.Content.ReadAsStringAsync().Result;
                    return response;
                }
            }
            return response;
        }

        //-------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------//
        //-------------------------------------------------------------------------------------------//

        public async Task<string> GetToken(UserLogin userLogin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                KeyValuePair<string, string> UserName = new KeyValuePair<string, string>("UserName", userLogin.UserName);
                KeyValuePair<string, string> Password = new KeyValuePair<string, string>("Password", userLogin.Password);
                KeyValuePair<string, string> grant_type = new KeyValuePair<string, string>("grant_type", userLogin.grant_type);

                List<KeyValuePair<string, string>> mykey = new List<KeyValuePair<string, string>>();
                mykey.Add(UserName);
                mykey.Add(Password);
                mykey.Add(grant_type);

                var formContent = new FormUrlEncodedContent(mykey);

                HttpResponseMessage Res = await client.PostAsync("almtoken", formContent);
                if (Res.IsSuccessStatusCode)
                {
                    var tokenResponse = Res.Content.ReadAsStringAsync().Result;
                    return tokenResponse;
                }

                return null;
            }
        }

        public async Task<List<EmployeeDetail>> GetAllEmployees(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage Res = await client.GetAsync("api/Employee");
                if (Res.IsSuccessStatusCode)
                {
                    var ReqResponse = Res.Content.ReadAsStringAsync().Result;
                    List<EmployeeDetail> employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(ReqResponse);
                    return employees;
                }
            }
            return null;
        }

        public async Task<string> GetEmployee(string accessToken, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage Res = await client.GetAsync("api/Employee/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var ReqResponse = Res.Content.ReadAsStringAsync().Result;
                    return ReqResponse;
                }
            }
            return null;
        }

        public async Task<bool> AddEmployee(string accessToken, string employeeInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                var stringContent = new StringContent(employeeInfo, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Employee", stringContent);
                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> UpdateEmployee(string accessToken, int id, string employeeNewInfo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                var stringContent = new StringContent(employeeNewInfo, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PutAsync("api/Employee/" + id, stringContent);

                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> RemoveEmployee(string accessToken, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                HttpResponseMessage Res = await client.DeleteAsync("api/Employee/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<AuthModel> GetAuthModel(string token)
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                HttpResponseMessage responseMessage = await client.GetAsync("api/Values");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<AuthModel>(response);
                }

                return null;

            }

        }

        public async Task<List<sp_AdminViewAllAttendance_Result>> AdminViewAllAttendance(string token,
            DateTime? startDate, DateTime? endDate)
        {
            List<sp_AdminViewAllAttendance_Result> leave = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                HttpResponseMessage res = await client.GetAsync($"api/AdminViewAllAttendance?StartDate={startDate}&EndDate={endDate}");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    leave = JsonConvert.DeserializeObject<List<sp_AdminViewAllAttendance_Result>>(response);
                    return leave;
                }
            }
            return leave;
        }

        public async Task<List<sp_AdminViewAllLeave_Result>> AdminViewAllLeave(string token)
        {
            List<sp_AdminViewAllLeave_Result> leave = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                HttpResponseMessage res = await client.GetAsync("api/AdminViewAllLeave");
                if (res.IsSuccessStatusCode)
                {
                    var response = res.Content.ReadAsStringAsync().Result;
                    leave = JsonConvert.DeserializeObject<List<sp_AdminViewAllLeave_Result>>(response);
                    return leave;
                }
            }
            return leave;
        }

        public async Task<string> SignUp(string signupInfo)
        {
            string BASEURL = "https://localhost:44339/";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var stringContent = new StringContent(signupInfo, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Auth", stringContent);
                if (Res.IsSuccessStatusCode)
                {
                    return Res.Content.ReadAsStringAsync().Result;
                    
                }
            }
            return null;
        }

        public async Task<DateTime?> GetLastRefreshedDate(int empId, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                HttpResponseMessage Res = await client.GetAsync("api/Auth/" + empId);
                if (Res.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<DateTime>(Res.Content.ReadAsStringAsync().Result);

                }
            }
            return null;
        }

        public async Task AddMonthlyLeaves(int empid, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                HttpResponseMessage Res = await client.PostAsync("api/Auth/"+empid, null);
                
            }
        }

        public async Task<List<EmployeeDetail>> GetAllManagers(string accessToken)
        {
            List<EmployeeDetail> employees = new List<EmployeeDetail>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(BASEURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                HttpResponseMessage responseMessage = await client.GetAsync("api/GetAllManagers");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<EmployeeDetail>>(response);
                }

                return employees;

            }

        }

    }
}
