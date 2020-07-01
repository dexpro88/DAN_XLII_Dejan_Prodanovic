using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic.Services
{
    interface IEmployeeService
    {
        List<vwEmployee> GetAllEmployees();
        List<vwMenager> GetAllPotentialMenagers();
        vwEmployee AddEmployee(vwEmployee employee);
        tblEmployee GetEmployeeByJMBG(string JMBG);
        tblEmployee GetEmployeeByRegnumber(string registrationNumber);
        void DeleteEmployee(int employeeID);
       
    }
}
