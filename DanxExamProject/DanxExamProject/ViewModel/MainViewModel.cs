using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanxExamProject.Model;
using DanxExamProject.Persistency;

namespace DanxExamProject.ViewModel
{
    class MainViewModel
    {
       public ObservableCollection<Employee> EmployeesInDb { get; set; } 

        public MainViewModel()
        {
            EmployeesInDb = new ObservableCollection<Employee>();

            var admin = new AdminOne();
            var stdEmp = new StandardEmp();
            

            EmployeesInDb.Add(admin);
            EmployeesInDb.Add(stdEmp);


           
            //PersistencyService.GetDataAsync(EmployeesInDb);
        }
    }
}
