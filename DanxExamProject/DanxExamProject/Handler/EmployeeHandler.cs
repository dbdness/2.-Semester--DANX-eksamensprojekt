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

       /// <summary>
       /// Will log the matching employee in if he isn't already, and start the time management.
       /// If the matching employee is logged in, he will be logged out, and the total hours will be updated thereafter.
       /// </summary>
        public void LoginOrLogout()
        {
          if(MainViewModel.OpenDbConnection) PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);

            var employees = _viewModel.EmployeesInDb.ToList();
            var matcingEmployee = employees.Find(e => e.EmployeeId.ToString() == _viewModel.LoginOrLogoutBox);

            var matchingLoggedInEmployee = _viewModel.LoggedInEmployees.Find(e => e.EmployeeId.ToString() == _viewModel.LoginOrLogoutBox);


            //If user IS NOT logged in, he will be logged in:
            if (matcingEmployee != null && matchingLoggedInEmployee == null)    
            {
                matcingEmployee.LastLogin = DateTime.Now;
                LastLoggedIn = matcingEmployee;
                _viewModel.DatabaseTable.Clear();
                _viewModel.DatabaseTable.Add(LastLoggedIn);
                if (MainViewModel.OpenDbConnection)
                {
                    PersistencyService.PostDataLoggedIn(matcingEmployee); //Posted to logged in employees database.

                    PersistencyService.PutData(matcingEmployee); //Updates logintime for the employee on the shown employee list. 

                    MainPage.CloseCanvases();
                    MainPage.MainScreenLoginCanvas.Visibility = Visibility.Visible;
                    if (matcingEmployee.GetType() == typeof (AdminEmp))
                        MainPage.AdminToolsCanvas.Visibility = Visibility.Visible;
                    else MainPage.AdminToolsCanvas.Visibility = Visibility.Collapsed;
                }
            }
            //If user IS logged in, he will be logged out:
            else if (matchingLoggedInEmployee != null)
            {
                _employeeToLogout = matchingLoggedInEmployee;
                UpdateLogoutTime();
                UpdateTotalHours();
                _employeeToLogout = null;
                if (MainViewModel.OpenDbConnection)
                {
                    PersistencyService.DeleteDataLoggedIn(matchingLoggedInEmployee); //Removing logged out employee from logged in table.


                    var goodbyeMsg = new MessageDialog("You have been logged out. Have a nice day!", "Goodbye");
                    goodbyeMsg.ShowAsync();
                    MainPage.CloseCanvases();
                    MainPage.MainScreenCanvas.Visibility = Visibility.Visible;
                }
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

       

        private void UpdateLogoutTime()
        {
            
           if(MainViewModel.OpenDbConnection) PersistencyService.GetData(_viewModel.EmployeesInDb);
            var employeeList = _viewModel.EmployeesInDb.ToList();
            var employeeToLogout = employeeList.Find(e => e.EmployeeId == _employeeToLogout.EmployeeId);
            _employeeToLogout = employeeToLogout;
            _employeeToLogout.LastLogout = DateTime.Now;
            
            
        }

        private TimeSpan CalculateTimeWorked()
        {

            var hoursWorked = _employeeToLogout.LastLogout.Subtract(_employeeToLogout.LastLogin);

            return _employeeToLogout.TotalHours += hoursWorked;

        }

        private void UpdateTotalHours()
        {
            _employeeToLogout.TotalHours = CalculateTimeWorked();
            _employeeToLogout.WorkedDays += 1; 
          if(MainViewModel.OpenDbConnection) PersistencyService.PutData(_employeeToLogout); //Unittest purposes
        }

       /// <summary>
       /// Returns the complete visual employeelist for the admin.
       /// </summary>
        public void CompleteEmployeeList()
        {
            PersistencyService.GetData(_viewModel.DatabaseTable);
        }

       /// <summary>
       /// Returns the admins own single entry in the database.
       /// </summary>
        public void PersonalEntryList()
        {
            _viewModel.DatabaseTable.Clear();
            _viewModel.DatabaseTable.Add(LastLoggedIn);
        }

       /// <summary>
       /// Returns the complete visual list of employees in the admin's own department.
       /// </summary>
        public void OwnDepartmentList()
        {
            var allEmployees = new ObservableCollection<Employee>();
            PersistencyService.GetData(allEmployees);
            var ownDepartment = from e in allEmployees where e.Manager == LastLoggedIn.Name select e;
            _viewModel.DatabaseTable.Clear();
            foreach (var e in ownDepartment) _viewModel.DatabaseTable.Add(e);
        }

       /// <summary>
       /// The standard- and admin employee's method of adding own vacation or sickdays.
       /// </summary>
        public void ChangeVacationOrSickdays()
        {
            if (_viewModel.StandardVacationDays != 0){ LastLoggedIn.VacationDays += _viewModel.StandardVacationDays;}
            if (_viewModel.StandardSickDays != 0){ LastLoggedIn.SickDays += _viewModel.StandardSickDays;}
           
            if(MainViewModel.OpenDbConnection == false) return;
            PersistencyService.PutData(LastLoggedIn);

            PersistencyService.GetData(_viewModel.EmployeesInDb);
            var updatedEmpList = _viewModel.EmployeesInDb.ToList();
            var updatedEmp = updatedEmpList.Find(e => e.EmployeeId == LastLoggedIn.EmployeeId);
            _viewModel.DatabaseTable.Clear();
            _viewModel.DatabaseTable.Add(updatedEmp);

        }
       
       /// <summary>
       /// The admin metohd of changing other employee's personal data (Name and Manager attribtues).
       /// </summary>
        public void AdminChangePersonalInfo()
        {
            if (SelectedEmployee == null)
            {
                if (MainViewModel.OpenDbConnection == false) throw new NullReferenceException("Please select an employee to edit.");
                return;
            }

            //Admin level 1 and 2 privileges check
            var admin = (AdminEmp) LastLoggedIn;
            if (admin.AdminLvl != 2) //If admin is lvl 1
            {
                if (SelectedEmployee.Manager != LastLoggedIn.Name)
                {
                    if(MainViewModel.OpenDbConnection == false) throw new ArgumentException("Error! You can only change data for your own employees.");
                    var errorMsg = new MessageDialog("You can only change data for your own employees.", "Error");
                    errorMsg.ShowAsync();
                    return;
                }

            }
            
            if (_viewModel.AdminChangeNameBox != null) SelectedEmployee.Name = _viewModel.AdminChangeNameBox;
            if (_viewModel.AdminChangeManagerBox != null) SelectedEmployee.Manager = _viewModel.AdminChangeManagerBox;

            if(MainViewModel.OpenDbConnection == false) return;

            PersistencyService.PutData(SelectedEmployee);
            if(admin.AdminLvl != 2) OwnDepartmentList();
            else PersistencyService.GetData(_viewModel.DatabaseTable);
            
        }

       /// <summary>
       ///  The admin metohd of changing other employee's salary data (SalaryNumber, VacationDays, SickDays and WorkedDays attribtues).
       /// </summary>
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

                if(MainViewModel.OpenDbConnection == false) return;

                PersistencyService.PutData(SelectedEmployee);
                if(admin.AdminLvl != 2) OwnDepartmentList();
                else PersistencyService.GetData(_viewModel.DatabaseTable);
            }
            catch (FormatException)
            {
                var errorMsg = new MessageDialog("Only integer values are allowed.", "Error");
               if(MainViewModel.OpenDbConnection) errorMsg.ShowAsync();
            }
        }

        
       /// <summary>
       /// The IPropertyChanged interface is implemented in order for the UI to always actively show the updated and correct values. 
       /// </summary>
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
    
