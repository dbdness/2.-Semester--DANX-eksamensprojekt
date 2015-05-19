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
    public class UnitTest1
    {
        private MainViewModel _mainViewModel;
        private EmployeeHandler _employeeHandler;


        [TestInitialize]
        public void BeforeTest()
        {
            MainViewModel.OpenDbConnection = false;
            _mainViewModel = new MainViewModel();
            _employeeHandler = new EmployeeHandler(_mainViewModel);

            
            _mainViewModel.EmployeesInDb.Add(new StandardEmp(){EmployeeId = 45, Name = "TestEmployee"});
            _mainViewModel.EmployeesInDb.Add(new StandardEmp(){EmployeeId = 22, Name = "TestEmployee2"});
        

        }
        
        
        [TestMethod]
        public void TestMethod1()
        {
            //TestCase 1.1
            _mainViewModel.LoginOrLogoutBox = "89";
            Assert.AreEqual("89", _mainViewModel.LoginOrLogoutBox);

            try
            {
                _employeeHandler.LoginOrLogout();
                
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Please enter a valid employee id.");
            }
            

        }

        [TestMethod]
        public void TestMethod2()
        {
            //TestCase 1.2

            //IF DB = OPEN IN EMPLOYEEHANDLER
            _mainViewModel.LoginOrLogoutBox = "45";
            Assert.AreEqual("45", _mainViewModel.LoginOrLogoutBox);

            _employeeHandler.LoginOrLogout();

        }
    }
}
