using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using DanxExamProject.Annotations;
using DanxExamProject.Model;
using DanxExamProject.Persistency;
using DanxExamProject.ViewModel;

namespace DanxExamProject.Handler
{
    class EmployeeHandler: INotifyPropertyChanged
    {
        private readonly MainViewModel _viewModel;
        public static Employee SelectedEmployee { get; set; }
        private static Employee _employeeToLogout;
        private static Employee _lastLoggedIn;

        public Employee LastLoggedIn
        {
            get { return _lastLoggedIn; }
            set
            {
                _lastLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public EmployeeHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void LoginOrLogout()
        {
            PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);

            var employees = _viewModel.EmployeesInDb.ToList();
            var matcingEmloyee = employees.Find(e => e.EmployeeId.ToString() == _viewModel.LoginOrLogoutBox);

            var matchingLoggedInEmployee = _viewModel.LoggedInEmployees.Find(e => e.EmployeeId.ToString() == _viewModel.LoginOrLogoutBox);

            //If user IS NOT logged in, he will be logged in:
            if (matcingEmloyee != null && matchingLoggedInEmployee == null)
            {
                matcingEmloyee.LastLogin = DateTime.Now;
                LastLoggedIn = matcingEmloyee;
                _viewModel.RecentlyLoggedInEmployee.Clear();
                _viewModel.RecentlyLoggedInEmployee.Add(LastLoggedIn);
                PersistencyService.PostDataLoggedIn(matcingEmloyee); //Posted to logged in employees database.
                PersistencyService.PutData(matcingEmloyee); //Updates logintime for the employee on the shown employee list. 
                MainPage.CloseCanvases();
                MainPage.MainScreenLoginCanvas.Visibility = Visibility.Visible;
                if (matcingEmloyee.GetType() == typeof (AdminEmp))
                    MainPage.AdminToolsCanvas.Visibility = Visibility.Visible;
                else MainPage.AdminToolsCanvas.Visibility = Visibility.Collapsed;

            }
                //If user IS logged in, he will be logged out:
            else if (matchingLoggedInEmployee != null)
            {
                _employeeToLogout = matchingLoggedInEmployee;
                UpdateLogoutTime();
                UpdateTotalHours();
                _employeeToLogout = null;
                PersistencyService.DeleteDataLoggedIn(matchingLoggedInEmployee); //Removing logged out employee from logged in table.

                var goodbyeMsg = new MessageDialog("You have been logged out. Have a nice day!", "Goodbye");
                goodbyeMsg.ShowAsync();
                MainPage.CloseCanvases();
                MainPage.MainScreenCanvas.Visibility = Visibility.Visible;
            }
                //If a wrong user id is entered:
            else
            {
                var errorMsg = new MessageDialog("Please enter a valid employee id.", "Error");
                errorMsg.ShowAsync();
            }

        }

        //public void AdminManage()
        //{
        //    var employees = _viewModel.EmployeesInDb.ToList();
        //    var matchingEmployee = employees.Find(e => e.EmployeeId.ToString() == _viewModel.AdminManageBox);
        //    if (matchingEmployee != null && matchingEmployee.GetType() == typeof (AdminEmp))
        //    {
        //        AdminLoggedIn = true;
        //    }
        //    else
        //    {
        //        AdminLoggedIn = false;
        //        var errorMsg = new MessageDialog("That user is not an admin. Please try again.", "Error");
        //        errorMsg.ShowAsync();
        //    }
        //}

        //private void UpdateLoginTime()
        //{
        //    PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);

        //    var recentEmployee = _viewModel.LoggedInEmployees.Last();

            
        //        var updatedEmployee = new StandardEmp()
        //        {
        //            EmployeeId = recentEmployee.EmployeeId,
        //            Name = recentEmployee.Name,
        //            TotalHours = recentEmployee.TotalHours,
        //            LastLogin = DateTime.Now,
        //            LastLogout = recentEmployee.LastLogout

        //        };
                
        //        PersistencyService.PutDataLoggedin(updatedEmployee); //Updated login time for logged in employee
                
        //    }

        //public void Logout()
        //{
        //    PersistencyService.GetData(_viewModel.EmployeesInDb);
        //    PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);
        //    var matchingEmployee = _viewModel.LoggedInEmployees.Find(e => e.EmployeeId.ToString() == _viewModel.LogoutBox);
        //    if (matchingEmployee != null)
        //    {
        //        _employeeToLogout = matchingEmployee;
        //        UpdateLogoutTime();
        //        UpdateTotalHours();
        //        _employeeToLogout = null;
        //        PersistencyService.DeleteDataLoggedIn(matchingEmployee); //Removing logged out employee from logged in table. 
        //    }
        //    else
        //    {
        //        var errorMsg = new MessageDialog("Invalid employee id, or the user is not logged in.", "Error");
        //        errorMsg.ShowAsync();
        //    }
        //}

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
            PersistencyService.GetData(_viewModel.EmployeesInDb);
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


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
    }

