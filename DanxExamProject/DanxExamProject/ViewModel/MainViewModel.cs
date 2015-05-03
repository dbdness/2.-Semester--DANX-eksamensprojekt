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

            PersistencyService.GetDataAsync(EmployeesInDb);



            //PersistencyService.GetDataAsync(EmployeesInDb);
        }
    }
}
