﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using DanxExamProject.Model;

namespace DanxExamProject.Persistency
{
    class PersistencyService
    {
        private const string ServerUri = "http://localhost:2000";
        private static HttpClient _client;

        public static void OpenApiConnection()
        {
            try
            {
                var handler = new HttpClientHandler();
                _client = new HttpClient(handler) {BaseAddress = new Uri(ServerUri)};
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            }
            catch (HttpRequestException)
            {
                var errorMsg = new MessageDialog("There was a problem in establishing connection to the database.",
                    "Error");
                errorMsg.ShowAsync();
            }
        }

        /// <summary>
        /// Gets the list of employees.
        /// </summary>
        /// <param name="collection"></param>
        public static void GetData(ObservableCollection<Employee> collection)
        {
                try
                {
                    var stdEmpResponse = _client.GetAsync("api/standardEmployees").Result;
                    var adminEmpResponse = _client.GetAsync("api/adminEmployees").Result;

                    if (stdEmpResponse.IsSuccessStatusCode && adminEmpResponse.IsSuccessStatusCode)
                    {
                        var stdEmpData = stdEmpResponse.Content.ReadAsAsync<IEnumerable<StandardEmp>>().Result;
                        var adminEmpData = adminEmpResponse.Content.ReadAsAsync<IEnumerable<AdminEmp>>().Result;

                        collection.Clear();

                        foreach (var e in stdEmpData) collection.Add(e);

                        foreach (var e in adminEmpData) collection.Add(e);
                        
                    }
                }
                catch (HttpRequestException)
                {

                }
            
        }

        /// <summary>
        /// Changes the login- and logout time for the employee.
        /// </summary>
        /// <param name="employee"></param>
        public static void PutData(Employee employee)
        {  
                try
                {

                    if (employee.GetType() == typeof(StandardEmp))
                    {
                        var response = _client.PutAsJsonAsync("api/standardEmployees/" + employee.EmployeeId, employee).Result;
                    }
                    if (employee.GetType() == typeof(AdminEmp))
                    {
                        var response = _client.PutAsJsonAsync("api/adminEmployees/" + employee.EmployeeId, employee).Result;
                    }

                }
                catch (HttpRequestException)
                {

                }
            
        }



        /// <summary>
        /// Changes the login-time for the logged in Employee
        /// </summary>
        /// <param name="employee"></param>
        public static void PutDataLoggedin(Employee employee)
        {
                try
                {
                    var response = _client.PutAsJsonAsync("api/loggedInEmployees/" + employee.EmployeeId, employee).Result;
                }
                catch (HttpRequestException)
                {

                }
            
        }

        /// <summary>
        /// Get the list of logged in employees.
        /// </summary>
        /// <param name="collection"></param>
        public static void GetDataLoggedIn(List<Employee> collection)
        {
                try
                {
                    var response = _client.GetAsync("api/loggedInEmployees").Result;

                    collection.Clear();
                    if (response.IsSuccessStatusCode)
                    {
                        var dbData = response.Content.ReadAsAsync<IEnumerable<StandardEmp>>().Result;
                        collection.AddRange(dbData);
                    }
                }
                catch (HttpRequestException)
                {

                }
            
        }

        /// <summary>
        /// Add an employee to the database for logged in employees.
        /// </summary>
        /// <param name="employee"></param>
        public static void PostDataLoggedIn(Employee employee)
        {
                try
                {
                    var response = _client.PostAsJsonAsync("api/loggedInEmployees", employee).Result;
                }
                catch (HttpRequestException)
                {

                }
            
        }

        /// <summary>
        /// Remove logged in employee from database when he logs out. 
        /// </summary>
        /// <param name="employee"></param>
        public static void DeleteDataLoggedIn(Employee employee)
        {
                try
                {
                    var response = _client.DeleteAsync("api/loggedInEmployees/" + employee.EmployeeId).Result;
                }
                catch (HttpRequestException)
                {

                }
            
        }
    }
}