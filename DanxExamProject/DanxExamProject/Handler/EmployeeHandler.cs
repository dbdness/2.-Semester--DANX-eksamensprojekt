using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using DanxExamProject.Model;
using DanxExamProject.Persistency;
using DanxExamProject.ViewModel;

namespace DanxExamProject.Handler
{
    class EmployeeHandler
    {
        private readonly MainViewModel _viewModel;
        public static Employee SelectedEmployee { get; set; }
        private static Employee _employeeToLogout;
        public static Employee LastLoggedIn { get; set; }
        public static bool IsLoggedIn { get; set; }

        public EmployeeHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Login()
        {
            var employees = _viewModel.EmployeesInDb.ToList();
            var matcingEmloyee = employees.Find(e => e.EmployeeId.ToString() == _viewModel.LoginBox);
            if (matcingEmloyee != null)
            {
                matcingEmloyee.LastLogin = DateTime.Now;
                LastLoggedIn = matcingEmloyee;
                PersistencyService.PostDataLoggedIn(matcingEmloyee); //Posted to logged in employees database.
                PersistencyService.PutDataLoggedin(matcingEmloyee);
                PersistencyService.PutData(matcingEmloyee); //Updates logintime for the employee on the shown employee list. 
                IsLoggedIn = true;
            }
            else
            {
                IsLoggedIn = false;
                var errorMsg = new MessageDialog("Please enter a valid employee id.", "Error");
                errorMsg.ShowAsync();
            }

        }

        private void UpdateLoginTime()
        {
            PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);

            var recentEmployee = _viewModel.LoggedInEmployees.Last();

            
                var updatedEmployee = new StandardEmp()
                {
                    EmployeeId = recentEmployee.EmployeeId,
                    Name = recentEmployee.Name,
                    TotalHours = recentEmployee.TotalHours,
                    LastLogin = DateTime.Now,
                    LastLogout = recentEmployee.LastLogout

                };
                
                PersistencyService.PutDataLoggedin(updatedEmployee); //Updated login time for logged in employee
                
            }

        public void Logout()
        {
            PersistencyService.GetData(_viewModel.EmployeesInDb);
            PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);
            var matchingEmployee = _viewModel.LoggedInEmployees.Find(e => e.EmployeeId.ToString() == _viewModel.LogoutBox);
            if (matchingEmployee != null)
            {
                _employeeToLogout = matchingEmployee;
                UpdateLogoutTime();
                UpdateTotalHours();
                _employeeToLogout = null;
                PersistencyService.DeleteDataLoggedIn(matchingEmployee); //Removing logged out employee from logged in table. 
            }
            else
            {
                var errorMsg = new MessageDialog("Invalid employee id, or the user is not logged in.", "Error");
                errorMsg.ShowAsync();
            }
        }

        private void UpdateLogoutTime()
        {
            //if (_employeeToLogout.GetType() == typeof (StandardEmp))
            //{
            //    var updatedEmployee = new StandardEmp();
            //    {
            //        Id = EmployeeToLogOut.Id,
            //        Name = EmployeeToLogOut.Name,
            //        Last_logout = DateTime.Now,
            //        Last_login = EmployeeToLogOut.Last_login,
            //        Total_hours = EmployeeToLogOut.Total_hours

            //    };
            var employeeList = _viewModel.EmployeesInDb.ToList();
            var employeeToLogout = employeeList.Find(e => e.EmployeeId == _employeeToLogout.EmployeeId);
            _employeeToLogout = employeeToLogout;
            _employeeToLogout.LastLogout = DateTime.Now;
            
            
            //EmployeeToLogout = updatedEmployee;
            //PersistencyService.PutDataForLoggedin(updatedEmployee);

        }

        private TimeSpan CalculateTimeWorked()
        {

            var hoursWorked = _employeeToLogout.LastLogout.Subtract(_employeeToLogout.LastLogin);

            return _employeeToLogout.TotalHours += hoursWorked;


        }

        private void UpdateTotalHours()
        {
            //var updatedEmployee = new Employee
            //{
            //    Id = EmployeeToLogOut.Id,
            //    Name = EmployeeToLogOut.Name,
            //    Last_login = EmployeeToLogOut.Last_login,
            //    Last_logout = EmployeeToLogOut.Last_logout,
            //    Total_hours = CalculateTimeWorked()
            //};
            _employeeToLogout.TotalHours = CalculateTimeWorked();
            PersistencyService.PutData(_employeeToLogout);
            //PersistencyService.PutDataForLoggedin(updatedEmployee);
        }




            
            

            


        }
    }

