using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanxExamProject.Model
{
    abstract class Employee
    {
        public int EmployeeId { get; set; }
        public int SalaryNumber {get;set;}
        public string Name { get; set; }
        public string Manager { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastLogout { get; set; }
        public TimeSpan StdHours { get; set; }
        public TimeSpan WatchHours { get; set; }
        public TimeSpan TotalHours { get; set; }
        public int VacationDays { get; set; }
        public int SickDays { get; set; }
        public int WorkedDays { get; set; }

        
    }
}
