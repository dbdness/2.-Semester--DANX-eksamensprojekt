using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DanxExamProject.Handler;
using DanxExamProject.Model;
using DanxExamProject.Persistency;
using DanxExamProject.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using DanxExamProject;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using Assert = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert;

namespace UnitTestLibrary
{
    [TestClass]
    public class UnitTest
    {
        private MainViewModel _mainViewModel;
        private EmployeeHandler _employeeHandler;
        private StandardEmp _testEmployee;
        private StandardEmp _testEmployee2;
        private AdminEmp _testAdminEmp;
        private AdminEmp _testAdminEmp2;

        [TestInitialize]
        public void BeforeTest()
        {
            MainViewModel.OpenDbConnection = false;
            Assert.IsFalse(MainViewModel.OpenDbConnection); //Making sure that the Database API connection is closed during unit testing. 

            _mainViewModel = new MainViewModel();
            _employeeHandler = new EmployeeHandler(_mainViewModel);
            
            _testEmployee = new StandardEmp() { EmployeeId = 22, Name = "TestEmployee", SickDays = 0, VacationDays = 0, Manager = "NotAdminTestEmployee", SalaryNumber = 1};
            _testEmployee2 = new StandardEmp(){EmployeeId = 50, Name = "TestEmployee2"};
            _testAdminEmp = new AdminEmp(){EmployeeId = 75, Name = "AdminTestEmployee", AdminLvl = 1};
            _testAdminEmp2 = new AdminEmp(){EmployeeId = 15, AdminLvl = 2, Name = "AdminLvl2TestEmployee"};


            
            _mainViewModel.EmployeesInDb.Add(_testEmployee);
            _mainViewModel.EmployeesInDb.Add(_testEmployee2);

            _mainViewModel.EmployeesInDb.Add(_testAdminEmp);
            _mainViewModel.EmployeesInDb.Add(_testAdminEmp2);

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

        [UITestMethod]
        public void TestMethod7()
        {
            //Testcase 2.1
            _mainViewModel.LoginOrLogoutBox = "22";
            Assert.AreEqual("22", _mainViewModel.LoginOrLogoutBox);
            
            Assert.AreEqual(_testEmployee.SickDays, 0);

            _employeeHandler.LoginOrLogout(); //Employee logs in

            DanxMainPage.SickDayRButton.IsChecked = true;

            _employeeHandler.ChangeVacationOrSickdays();

            Assert.AreEqual(_employeeHandler.LastLoggedIn.SickDays, 1);


        }

        [UITestMethod]
        public void TestMethod8()
        {
            //Testcase 2.2
            _mainViewModel.LoginOrLogoutBox = "22";
            Assert.AreEqual("22", _mainViewModel.LoginOrLogoutBox);

            Assert.AreEqual(_testEmployee.VacationDays, 0);

            _employeeHandler.LoginOrLogout(); //Employee logs in

            DanxMainPage.VacationDayRButton.IsChecked = true;

            _employeeHandler.ChangeVacationOrSickdays();

            Assert.AreEqual(_employeeHandler.LastLoggedIn.VacationDays, 1);
        }


        [TestMethod]
        public void TestMethod9()
        {
            //Testcase 3.1
            _employeeHandler.SelectedEmployee = null; //No employee has been selected from the list.

            try
            {
                _employeeHandler.AdminChangePersonalInfo();
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual(ex.Message, "Please select an employee to edit.");
            }
            
        }

        [TestMethod]
        public void TestMethod10()
        {
            //Testcase 3.2
            _mainViewModel.LoginOrLogoutBox = "75";
            Assert.AreEqual(_mainViewModel.LoginOrLogoutBox, "75");

            _employeeHandler.LoginOrLogout(); //Admin level 1 logs in

            _employeeHandler.SelectedEmployee = _testEmployee; //Admin lvl 1 selects an employee outside of his department.

            try
            {
                _employeeHandler.AdminChangePersonalInfo();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Error! You can only change data for your own employees.");
            }

        }


        [TestMethod]
        public void TestMethod11()
        {
            //Testcase 3.3
            _mainViewModel.LoginOrLogoutBox = "15";
            Assert.AreEqual(_mainViewModel.LoginOrLogoutBox, "15");

            _employeeHandler.LoginOrLogout(); //Admin level 2 logs in

            _employeeHandler.SelectedEmployee = _testEmployee; //Admin lvl 2 selects an employee outside of his department. 

            _employeeHandler.AdminChangePersonalInfo(); //Success
        }

        [TestMethod]
        public void TestMethod12()
        {
            //Testcase 3.4
            _employeeHandler.SelectedEmployee = _testEmployee;

            _mainViewModel.AdminChangeSalaryNumberBox = "notAnInt";

            _employeeHandler.AdminChangeSalaryInfo();

            //The FormatException gets caught in the code. 


        }




















    }
}
