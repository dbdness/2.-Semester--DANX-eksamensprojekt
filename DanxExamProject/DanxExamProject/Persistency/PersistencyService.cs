using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DanxExamProject.Model;

namespace DanxExamProject.Persistency
{
    class PersistencyService
    {
        private const string ServerUri = "http://localhost:2000";

        /// <summary>
        /// Gets the list of employees.
        /// </summary>
        /// <param name="collection"></param>
        public static void GetDataAsync(ObservableCollection<Employee> collection)
        {
            var handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.GetAsync("api/mainEmployees").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //Add employees to collection. Cannot read as abstract class Employee.
                        
                        
                    }
                }
                catch (HttpRequestException)
                {

                }
            }
        }

        /// <summary>
        /// Changes the login- and logout time for the employee.
        /// </summary>
        /// <param name="employee"></param>
        public static void PutData(Employee employee)
        {
            var handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
            
                try
                {
                    var response = client.PutAsJsonAsync("api/mainEmployees/" + employee.EmployeeId, employee).Result;
                }
                catch (HttpRequestException)
                {

                }
            }
        }

        /// <summary>
        /// Changes the login-time for the logged in Employee
        /// </summary>
        /// <param name="employee"></param>
        public static void PutDataForLoggedin(Employee employee)
        {
            var handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.PutAsJsonAsync("api/loggedInEmployees/" + employee.EmployeeId, employee).Result;
                }
                catch (HttpRequestException)
                {

                }
            }
        }

        /// <summary>
        /// Get the list of logged in employees.
        /// </summary>
        /// <param name="collection"></param>
        public async static void GetDataAsync(List<Employee> collection)
        {
            var handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await client.GetAsync("api/loggedInEmployees");

                    if (response.IsSuccessStatusCode)
                    {
                        var dbData = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                        collection.AddRange(dbData);
                    }
                }
                catch (HttpRequestException)
                {

                }
            }
        }

        /// <summary>
        /// Add an employee to the database for logged in employees.
        /// </summary>
        /// <param name="employee"></param>
        public static void PostData(Employee employee)
        {
            var handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.PostAsJsonAsync("api/loggedInEmployees", employee).Result;
                }
                catch (HttpRequestException)
                {

                }
            }
        }

        /// <summary>
        /// Remove logged in employee from database when he logs out. 
        /// </summary>
        /// <param name="employee"></param>
        public static void DeleteData(Employee employee)
        {
            var handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.DeleteAsync("api/loggedInEmployees/" + employee.EmployeeId).Result;
                }
                catch (HttpRequestException)
                {

                }
            }
        }
    }
}
