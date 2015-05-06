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
        public string LoginBox { get; set; }
        public string LogoutBox { get; set; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        
        

       
        public MainViewModel()
        {
            EmployeeHandler = new EmployeeHandler(this);

            EmployeesInDb = new ObservableCollection<Employee>();
            LoggedInEmployees = new List<Employee>();
            
            PersistencyService.GetData(EmployeesInDb);
            PersistencyService.GetData(EmployeesInDb);
            PersistencyService.GetDataLoggedIn(LoggedInEmployees);

            LoginCommand = new RelayCommand(EmployeeHandler.Login);
            LogoutCommand = new RelayCommand(EmployeeHandler.Logout);

            //foreach (var v in EmployeesInDb)
            //{
            //    if (v.GetType() == typeof (AdminOne))
            //    {
            //        var message = new MessageDialog("It worked");
            //        message.ShowAsync();
            //    }
            //}



        }

    }
}
