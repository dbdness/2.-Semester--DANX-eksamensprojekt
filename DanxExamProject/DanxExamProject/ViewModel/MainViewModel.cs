using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using DanxExamProject.Common;
using DanxExamProject.Handler;
using DanxExamProject.Model;
using DanxExamProject.Persistency;

namespace DanxExamProject.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<Employee> EmployeesInDb { get; set; }
        public EmployeeHandler EmployeeHandler { get; set; }
        public List<Employee> LoggedInEmployees { get; set; }
        public ObservableCollection<Employee> RecentlyLoggedInEmployee { get; set; } 
        public string LoginOrLogoutBox { get; set; }
        public string AdminManageBox { get; set; }
        public RelayCommand LoginOrLogoutCommand { get; set; }
        public RelayCommand AdminManageCommannd { get; set; }
        
        

       
        public MainViewModel()
        {
            EmployeeHandler = new EmployeeHandler(this);

            EmployeesInDb = new ObservableCollection<Employee>();
            LoggedInEmployees = new List<Employee>();
            RecentlyLoggedInEmployee = new ObservableCollection<Employee>();
            
            PersistencyService.GetData(EmployeesInDb);
            PersistencyService.GetData(EmployeesInDb);
            PersistencyService.GetDataLoggedIn(LoggedInEmployees);

            LoginOrLogoutCommand = new RelayCommand(EmployeeHandler.LoginOrLogout);
            AdminManageCommannd = new RelayCommand(EmployeeHandler.AdminManage);






        }

    }
}
