using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanxExamProject.Model;
using DanxExamProject.Persistency;
using DanxExamProject.ViewModel;

namespace DanxExamProject.Handler
{
    class EmployeeHandler
    {
        private MainViewModel _viewModel;
        private bool _isLoggedIn;
        public static Employee SelectedEmployee { get; set; }

        public EmployeeHandler(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Login()
        {
            var employees = _viewModel.EmployeesInDb.ToList();
            var matcingEmloyee = employees.Find(e => e.EmployeeId.ToString() == _viewModel.LoginBox);
            if (matcingEmloyee != null)
            {
                PersistencyService.PostDataLoggedIn(matcingEmloyee); //Posted to logged in employees database.
                matcingEmloyee.LastLogin = DateTime.Now;
                PersistencyService.PutData(matcingEmloyee); //Updates logintime for the employee on the show employee list. 
                UpdateLoginTime();
                _isLoggedIn = true;

            }
            else _isLoggedIn = false;

        }

        private void UpdateLoginTime()
        {
            PersistencyService.GetDataLoggedIn(_viewModel.LoggedInEmployees);

            var recentEmployee = _viewModel.LoggedInEmployees.Last();

            
                var updatedEmployee = new StandardEmp()
                {
                    EmployeeId = recentEmployee.EmployeeId,
                    Name = recentEmployee.Name,
                    TotalHours = recentEmployee.TotalHours,
                    LastLogin = DateTime.Now,
                    LastLogout = recentEmployee.LastLogout

                };
                
                PersistencyService.PutDataLoggedin(updatedEmployee); //Updated login time for logged in employee
                
            
            }


            

            


        }
    }

