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


namespace DanxExamProject.ViewModel
{
   public class MainViewModel
    {
        public ObservableCollection<Employee> EmployeesInDb { get; set; }
        public EmployeeHandler EmployeeHandler { get; set; }
        public List<Employee> LoggedInEmployees { get; set; }
        public ObservableCollection<Employee> DatabaseTable { get; set; } 
        public string LoginOrLogoutBox { get; set; }
        public string AdminChangeNameBox { get; set; }
        public string AdminChangeManagerBox { get; set; }
        public string AdminChangeSalaryNumberBox { get; set; }
        public string AdminChangeVacationDaysBox { get; set; }
        public string AdminChangeSickDaysBox { get; set; }
        public string AdminChangeWorkedDaysBox { get; set; }
        public RelayCommand LoginOrLogoutCommand { get; set; }
        public RelayCommand CompleteEmployeeListCommand { get; set; }
        public RelayCommand PersonalEntryListCommand { get; set; }
        public RelayCommand OwnDepartmentListCommand { get; set; }
        public RelayCommand AddSickOrVacationdaysCommand { get; set; }
        public RelayCommand AdminChangePersonalInfoCommand { get; set; }
        public RelayCommand AdminChangeSalaryInfoCommand { get; set; }
        public RelayCommand SortByNameCommand { get; set; }
        public RelayCommand SortByEmployeeIdCommand { get; set; }
        public RelayCommand SpreadSheetCommand { get; set; }
        public RelayCommand ExportAsCsvCommand { get; set; }
       

        /// <summary>
        /// For unittest purposes. The unittest does not run properly if the Database connection is open. 
        /// </summary>
       public static bool OpenDbConnection = true;
        

       
        public MainViewModel()
        {
            EmployeeHandler = new EmployeeHandler(this);
           if(OpenDbConnection) {PersistencyService.OpenApiConnection();}

            EmployeesInDb = new ObservableCollection<Employee>();
            LoggedInEmployees = new List<Employee>();
            DatabaseTable = new ObservableCollection<Employee>();

            if (OpenDbConnection)
            {
                PersistencyService.GetData(EmployeesInDb);
                PersistencyService.GetDataLoggedIn(LoggedInEmployees);
            }

            LoginOrLogoutCommand = new RelayCommand(EmployeeHandler.LoginOrLogout);
            CompleteEmployeeListCommand = new RelayCommand(EmployeeHandler.CompleteEmployeeList);
            PersonalEntryListCommand = new RelayCommand(EmployeeHandler.PersonalEntryList);
            OwnDepartmentListCommand = new RelayCommand(EmployeeHandler.OwnDepartmentList);
            AddSickOrVacationdaysCommand = new RelayCommand(EmployeeHandler.ChangeVacationOrSickdays);
            AdminChangePersonalInfoCommand = new RelayCommand(EmployeeHandler.AdminChangePersonalInfo);
            AdminChangeSalaryInfoCommand = new RelayCommand(EmployeeHandler.AdminChangeSalaryInfo);
            SortByNameCommand = new RelayCommand(EmployeeHandler.SortByName);
            SortByEmployeeIdCommand = new RelayCommand(EmployeeHandler.SortByEmployeeId);
            ExportAsCsvCommand = new RelayCommand(() => ExportEmployeesToCsvFile());


        }


       private async Task ExportEmployeesToCsvFile()
       {
           var csv = new CsvExport<Employee>(DatabaseTable.ToList());
           await csv.ExportToFile("DanxEmployees.csv");
           
       }



      
       

      

    }
}
