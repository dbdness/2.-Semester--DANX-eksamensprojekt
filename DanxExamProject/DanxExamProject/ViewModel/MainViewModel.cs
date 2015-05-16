using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DanxExamProject.Common;
using DanxExamProject.Handler;
using DanxExamProject.Model;
using DanxExamProject.Persistency;
using DanxExamProject.View;

namespace DanxExamProject.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<Employee> EmployeesInDb { get; set; }
        public EmployeeHandler EmployeeHandler { get; set; }
        public List<Employee> LoggedInEmployees { get; set; }
        public ObservableCollection<Employee> DatabaseTable { get; set; } 
        public string LoginOrLogoutBox { get; set; }
        public int StandardVacationDays { get; set; }
        public int StandardSickDays { get; set; }
        public RelayCommand LoginOrLogoutCommand { get; set; }
        public RelayCommand CompleteEmployeeListCommand { get; set; }
        public RelayCommand PersonalEntryListCommand { get; set; }
        public RelayCommand OwnDepartmentListCommand { get; set; }
        public RelayCommand AddSickOrVacationdaysCommand { get; set; }

        public List<int> AgeList { get; set; } 
        

       
        public MainViewModel()
        {
            EmployeeHandler = new EmployeeHandler(this);

            EmployeesInDb = new ObservableCollection<Employee>();
            LoggedInEmployees = new List<Employee>();
            DatabaseTable = new ObservableCollection<Employee>();
            
            PersistencyService.GetData(EmployeesInDb);
            PersistencyService.GetData(EmployeesInDb);
            PersistencyService.GetDataLoggedIn(LoggedInEmployees);

            LoginOrLogoutCommand = new RelayCommand(EmployeeHandler.LoginOrLogout);
            CompleteEmployeeListCommand = new RelayCommand(EmployeeHandler.CompleteEmployeeList);
            PersonalEntryListCommand = new RelayCommand(EmployeeHandler.PersonalEntryList);
            OwnDepartmentListCommand = new RelayCommand(EmployeeHandler.OwnDepartmentList);
            AddSickOrVacationdaysCommand = new RelayCommand(EmployeeHandler.ChangeVacationOrSickdays);

            AgeList = Ages();





        }

        private List<int> Ages()
        {
            var ages = new List<int>();
            for (int i = 1; i < 30; i++) ages.Add(i);
            return ages;
        } 

    }
}
