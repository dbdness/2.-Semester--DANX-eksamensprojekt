using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DanxExamProject.Handler;
using DanxExamProject.Model;
using DanxExamProject.Persistency;
using DanxExamProject.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using DanxExamProject;

namespace UnitTestLibrary
{
    [TestClass]
    public class UnitTest
    {
        private MainViewModel _mainViewModel;
        private EmployeeHandler _employeeHandler;
        private StandardEmp _testEmployee = new StandardEmp() { EmployeeId = 22, Name = "TestEmployee" };
        private StandardEmp _testEmployee2 = new StandardEmp(){EmployeeId = 50, Name = "TestEmployee2"};

        [TestInitialize]
        public void BeforeTest()
        {
            MainViewModel.OpenDbConnection = false;
            Assert.IsFalse(MainViewModel.OpenDbConnection); //Making sure that the Database API connection is closed. 

            _mainViewModel = new MainViewModel();
            _employeeHandler = new EmployeeHandler(_mainViewModel);
            
            
            _mainViewModel.EmployeesInDb.Add(_testEmployee);
            _mainViewModel.EmployeesInDb.Add(_testEmployee2);
            _mainViewModel.LoggedInEmployees.Add(_testEmployee2);




        }
        
        
        [TestMethod]
        public void TestMethod1()
        {
            //Testcase 1.1
            _mainViewModel.LoginOrLogoutBox = "89";
            Assert.AreEqual("89", _mainViewModel.LoginOrLogoutBox);

            try
            {
                _employeeHandler.LoginOrLogout(); //Employee logs in
                
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Please enter a valid employee id.");
            }
            

        }

        [TestMethod]
        public void TestMethod2()
        {
            //Testcase 1.2
            _mainViewModel.LoginOrLogoutBox = "45";
            Assert.AreEqual("45", _mainViewModel.LoginOrLogoutBox);

            _employeeHandler.LoginOrLogout(); //Employee logs in

        }

        [TestMethod]
        public void TestMethod3()
        {
            //Testcase 1.3
            _mainViewModel.LoginOrLogoutBox = "22";
            Assert.AreEqual("22", _mainViewModel.LoginOrLogoutBox);

            var lastLoginBeforeLogin = _testEmployee.LastLogin;

            _employeeHandler.LoginOrLogout(); //Employee logs in

            Assert.AreNotEqual(_testEmployee.LastLogin, lastLoginBeforeLogin); //Checks if the last logged in employee has had his LastLogin attribute updated properly. 

        }

        [TestMethod]
        public void TestMethod4()
        {
            //Testcase 1.4
            _mainViewModel.LoginOrLogoutBox = "50";
            Assert.AreEqual("50", _mainViewModel.LoginOrLogoutBox);

            _employeeHandler.LoginOrLogout();
            _mainViewModel.LoggedInEmployees.Clear(); //Simulates the removal of the employee from the logged in table using the DeleteDataLoggedIn() method.
            
            
            Assert.IsFalse(_mainViewModel.LoggedInEmployees.Contains(_testEmployee2));
        }

        [TestMethod]
        public void TestMethod5()
        {
            //Testcase 1.5
            _mainViewModel.LoginOrLogoutBox = "50";
            Assert.AreEqual("50", _mainViewModel.LoginOrLogoutBox);

            var lastLogoutBeforeLogout = _testEmployee2.LastLogout;

            _employeeHandler.LoginOrLogout(); //Employee logs out

            Assert.AreNotEqual(_testEmployee2.LastLogout, lastLogoutBeforeLogout);


        }

        [TestMethod]
        public void TestMethod6()
        {
            //Testcase 1.6
            _mainViewModel.LoginOrLogoutBox = "50";
            Assert.AreEqual("50", _mainViewModel.LoginOrLogoutBox);

            var totalHoursBeforeLogout = _testEmployee2.TotalHours;

            _employeeHandler.LoginOrLogout(); //Employee logs out

            Assert.AreNotEqual(_testEmployee2.TotalHours, totalHoursBeforeLogout);
            
                
        }

        [TestMethod]
        public void TestMethod7()
        {
            //Testcase 2.1
            _mainViewModel.LoginOrLogoutBox = "22";
            Assert.AreEqual("22", _mainViewModel.LoginOrLogoutBox);

            Assert.AreEqual(_testEmployee2.SickDays, null);
            Assert.AreEqual(_testEmployee.VacationDays, null);

            _employeeHandler.LoginOrLogout(); //Employee logs in

            _mainViewModel.StandardSickDays = 1;
            _mainViewModel.StandardVacationDays = 5; //Employee adds 1 sick day and 5 vacation days.
            
            _employeeHandler.ChangeVacationOrSickdays();

            Assert.AreEqual(_employeeHandler.LastLoggedIn.SickDays, 1);
            Assert.AreEqual(_employeeHandler.LastLoggedIn.VacationDays, 5);


        }




    }
}
