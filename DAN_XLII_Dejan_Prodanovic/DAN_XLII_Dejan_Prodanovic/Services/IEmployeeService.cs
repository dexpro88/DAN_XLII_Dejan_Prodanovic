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
        vwEmployee AddEmployee(vwEmployee employee);
        //List<tblEmployee> GetAllEmployees();
        void DeleteEmployee(int employeeID);
        //List<EmployeeToPresent> GetAllEmployeesToPresent();
    }
}
