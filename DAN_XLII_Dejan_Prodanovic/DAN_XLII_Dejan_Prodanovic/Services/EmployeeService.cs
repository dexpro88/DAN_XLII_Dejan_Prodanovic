using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public vwEmployee AddEmployee(vwEmployee employee)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblEmployee newEmployee = new tblEmployee();
                    
                    newEmployee.FirstName = employee.FirstName;
                    newEmployee.LastName = employee.LastName;
                    newEmployee.JMBG = employee.JMBG;
                    newEmployee.RegistrationNumber = employee.RegistrationNumber;
                    newEmployee.TelefonNumber = employee.TelefonNumber;
                    newEmployee.LocationID = employee.LocationID;
                    newEmployee.DateOfBirth = employee.DateOfBirth;
                    newEmployee.MenagerID = employee.MenagerID;
                    newEmployee.GenderID = employee.GenderID;
                    newEmployee.SectorID = employee.SectorID;




                    context.tblEmployees.Add(newEmployee);

                    context.SaveChanges();
                    
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogAddIDCardToFile(idCard);

                    employee.EmployeeID = newEmployee.EmployeeID;
                    return employee;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<vwMenager> GetAllPotentialMenagers()
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<vwMenager> list = new List<vwMenager>();
                    list = (from x in context.vwMenagers where !x.Menager.Equals(" ") select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee GetEmployeeByJMBG(string JMBG)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblEmployee emoloyee = (from e in context.tblEmployees where e.JMBG.Equals(JMBG) select e).First();


                    return emoloyee;

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee GetEmployeeByRegnumber(string registrationNumber)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblEmployee emoloyee = (from e in context.tblEmployees where e.RegistrationNumber.Equals(registrationNumber) select e).First();


                    return emoloyee;

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwMenager GetMenagerByName(string name)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    vwMenager menager = (from m in context.vwMenagers where m.Menager.Equals(name) select m).First();


                    return menager;

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblEmployee AddEmptyMenager()
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblEmployee newEmployee = new tblEmployee();

                    newEmployee.FirstName = "";
                    newEmployee.LastName = "";
                  
                    context.tblEmployees.Add(newEmployee);

                    context.SaveChanges();
                  
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogAddIDCardToFile(idCard);

                    
                    return newEmployee;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public bool IsMenager(int employeeID)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    tblEmployee emloyee = (from e in context.tblEmployees where e.MenagerID==employeeID select e).First();

                    if (emloyee == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                     

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public List<vwMenager> GetAllPotentialMenagersForEditWindow(int userToEditID)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<vwMenager> list = new List<vwMenager>();
                    list = (from x in context.vwMenagers where !x.Menager.Equals(" ")&& x.EmployeeID != userToEditID select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public vwEmployee EditEmployee(vwEmployee employee)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                   
                    tblEmployee employeeToEdit = (from e in context.tblEmployees where e.EmployeeID == employee.EmployeeID select e).First();

                    vwEmployee oldUserData = new vwEmployee();
                    oldUserData.FirstName = employee.FirstName;
                    oldUserData.LastName = employee.LastName;
                    oldUserData.JMBG = employee.JMBG;
                    oldUserData.RegistrationNumber = employee.RegistrationNumber;
                    oldUserData.TelefonNumber = employee.TelefonNumber;
                    oldUserData.Gender = employee.Gender;

                    employeeToEdit.FirstName = employee.FirstName;
                    employeeToEdit.LastName = employee.LastName;
                    employeeToEdit.JMBG = employee.JMBG;
                    employeeToEdit.TelefonNumber = employee.TelefonNumber;
                    employeeToEdit.RegistrationNumber = employee.RegistrationNumber;
                    employeeToEdit.LocationID = employee.LocationID;
                    employeeToEdit.SectorID = employee.SectorID;
                    employeeToEdit.DateOfBirth = employee.DateOfBirth;
                    employeeToEdit.MenagerID = employee.MenagerID;
                    employeeToEdit.GenderID = employee.GenderID;


                    context.SaveChanges();
 
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogEditUserToFilevwUser(user, oldUserData);
                    return employee;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
