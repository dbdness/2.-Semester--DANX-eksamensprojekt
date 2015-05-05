using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using DanxExamProject.Common;
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

            //foreach (var v in EmployeesInDb)
            //{
            //    if (v.GetType() == typeof (AdminOne))
            //    {
            //        var message = new MessageDialog("It worked");
            //        message.ShowAsync();
            //    }
            //}

            

        }

        public void Something()
        {
            
        }
    }
}
