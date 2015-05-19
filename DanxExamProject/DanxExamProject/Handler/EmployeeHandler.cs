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
   public class EmployeeHandler: INotifyPropertyChanged
    {
        private readonly MainViewModel _viewModel;
        private static Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
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
                _viewModel.DatabaseTable.Clear();
                _viewModel.DatabaseTable.Add(LastLoggedIn);
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
                try
                {
                    throw new ArgumentException("Please enter a valid employee id.");
                }
                catch (ArgumentException)
                {
                    var errorMsg = new MessageDialog("Please enter a valid employee id.", "Error");
                   if(MainViewModel.OpenDbConnection) errorMsg.ShowAsync(); //Unittest purposes
                }
                
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
            _employeeToLogout.WorkedDays += 1; 
            PersistencyService.PutData(_employeeToLogout);
            //PersistencyService.PutDataForLoggedin(updatedEmployee);
        }

        public void CompleteEmployeeList()
        {
            PersistencyService.GetData(_viewModel.DatabaseTable);
        }

        public void PersonalEntryList()
        {
            _viewModel.DatabaseTable.Clear();
            _viewModel.DatabaseTable.Add(LastLoggedIn);
        }

        public void OwnDepartmentList()
        {
            var allEmployees = new ObservableCollection<Employee>();
            PersistencyService.GetData(allEmployees);
            var ownDepartment = from e in allEmployees where e.Manager == LastLoggedIn.Name select e;
            _viewModel.DatabaseTable.Clear();
            foreach (var e in ownDepartment) _viewModel.DatabaseTable.Add(e);
        }

        public void ChangeVacationOrSickdays()
        {
            if (_viewModel.StandardVacationDays != 0){ LastLoggedIn.VacationDays += _viewModel.StandardVacationDays;}
            if (_viewModel.StandardSickDays != 0){ LastLoggedIn.SickDays += _viewModel.StandardSickDays;}
            PersistencyService.PutData(LastLoggedIn);

            PersistencyService.GetData(_viewModel.EmployeesInDb);
            var updatedEmpList = _viewModel.EmployeesInDb.ToList();
            var updatedEmp = updatedEmpList.Find(e => e.EmployeeId == LastLoggedIn.EmployeeId);
            _viewModel.DatabaseTable.Clear();
            _viewModel.DatabaseTable.Add(updatedEmp);

        }

        public void AdminChangePersonalInfo()
        {
            if (SelectedEmployee == null) return;

            //Admin level 1 and 2 privileges check
            var admin = (AdminEmp) LastLoggedIn;
            if (admin.AdminLvl != 2) //If admin is lvl 1
            {
                if (SelectedEmployee.Manager != LastLoggedIn.Name)
                {
                 var errorMsg = new MessageDialog("You can only change data for your own employees.", "Error");
                    errorMsg.ShowAsync();
                    return;
                }

            }
            
            
            if (_viewModel.AdminChangeNameBox != null) SelectedEmployee.Name = _viewModel.AdminChangeNameBox;
            if (_viewModel.AdminChangeManagerBox != null) SelectedEmployee.Manager = _viewModel.AdminChangeManagerBox;

            PersistencyService.PutData(SelectedEmployee);
            if(admin.AdminLvl != 2) OwnDepartmentList();
            else PersistencyService.GetData(_viewModel.DatabaseTable);
            
        }

        public void AdminChangeSalaryInfo()
        {           
            if (SelectedEmployee == null) return;

            var admin = (AdminEmp)LastLoggedIn;
            if (admin.AdminLvl != 2) //If admin is lvl 1
            {
                if (SelectedEmployee.Manager != LastLoggedIn.Name)
                {
                    var errorMsg = new MessageDialog("You can only change data for your own employees.", "Error");
                    errorMsg.ShowAsync();
                    return;
                }
                
            }
            
            try
            {
                //The following needs parsing due to the fact that the TextBox controls are bound to string values. 

                if (!String.IsNullOrWhiteSpace(_viewModel.AdminChangeSalaryNumberBox))
                    SelectedEmployee.SalaryNumber = int.Parse(_viewModel.AdminChangeSalaryNumberBox);

                if (!String.IsNullOrWhiteSpace(_viewModel.AdminChangeVacationDaysBox))
                    SelectedEmployee.VacationDays = int.Parse(_viewModel.AdminChangeVacationDaysBox);

                if (!String.IsNullOrWhiteSpace(_viewModel.AdminChangeSickDaysBox))
                    SelectedEmployee.SickDays = int.Parse(_viewModel.AdminChangeSickDaysBox);  

                if (!String.IsNullOrWhiteSpace(_viewModel.AdminChangeWorkedDaysBox))
                    SelectedEmployee.WorkedDays = int.Parse(_viewModel.AdminChangeWorkedDaysBox);


                PersistencyService.PutData(SelectedEmployee);
                if(admin.AdminLvl != 2) OwnDepartmentList();
                else PersistencyService.GetData(_viewModel.DatabaseTable);
            }
            catch (FormatException)
            {
                var errorMsg = new MessageDialog("Only integer values are allowed.", "Error");
                errorMsg.ShowAsync();
            }
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
    
