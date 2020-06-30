using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLII_Dejan_Prodanovic.Services
{
    class EmployeeService:IEmployeeService
    {
        public List<vwEmployee> GetAllEmployees()
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<vwEmployee> list = new List<vwEmployee>();
                    list = (from x in context.vwEmployees select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// method that deletes employee from Employee table in database
        /// it logs this action to logs.txt file
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblEmployee employeeToDelete = (from e in context.tblEmployees where e.EmployeeID == employeeID select e).First();
                   
                    context.tblEmployees.Remove(employeeToDelete);
                 
                    context.SaveChanges();

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        //public List<EmployeeToPresent> GetAllEmployeesToPresent()
        //{
        //    List<EmployeeToPresent> employeesToPresents = new List<EmployeeToPresent>();
        //    List<tblEmployee> employees = GetAllEmployees();
        //    EmployeeToPresent empToPresent = new EmployeeToPresent();

        //    foreach (var employee in employees)
        //    {
        //        empToPresent.FirstName = employee.FirstName;
        //        empToPresent.FirstName = employee.LastName;
        //        empToPresent.FirstName = employee.FirstName;
        //        empToPresent.FirstName = employee.FirstName;
        //    }
        //}
    }
}
