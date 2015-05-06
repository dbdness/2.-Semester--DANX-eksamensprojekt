using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanxExamProject.Model
{
    class AdminEmp: Employee
    {
        public int AdminLvl;

        public override string ToString()
        {
            return String.Format("{0}h {1}m", TotalHours.Hours, TotalHours.Minutes);
        }
    }
}
