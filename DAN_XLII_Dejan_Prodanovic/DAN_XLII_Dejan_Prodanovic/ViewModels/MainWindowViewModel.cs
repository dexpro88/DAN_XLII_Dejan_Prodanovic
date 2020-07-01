using DAN_XLII_Dejan_Prodanovic.Commands;
using DAN_XLII_Dejan_Prodanovic.Services;
using DAN_XLII_Dejan_Prodanovic.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLII_Dejan_Prodanovic.ViewModels
{
    class MainWindowViewModel:ViewModelBase
    {
        MainWindow main;

        ILocationService locationService;
        IEmployeeService employeeService;

        #region Constructor

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;

            locationService = new LocationService();
            employeeService = new EmployeeService();

            if (locationService.GetAllLocations().Count == 0)
            {
                LocationFileActions.AddLocationsToDatabase();
            }

            vwMenager firstMenager = employeeService.GetMenagerByName(" ");

            if (firstMenager == null)
            {
                employeeService.AddEmptyMenager();
            }

            Employees = employeeService.GetAllEmployees();
        }
        #endregion

        #region Properties     

        private vwEmployee employee;
        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<vwEmployee> employees;
        public List<vwEmployee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }
        #endregion

        #region Commands 
        /// <summary>
        /// delete command
        /// </summary>
        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }

        private void DeleteEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this employee?",
                       "My App",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int employeeID = employee.EmployeeID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            if (employeeService.IsMenager(employeeID))
                            {
                                MessageBox.Show("This user is menager to other users. You have to delete them first.");
                                return;
                            }
                            employeeService.DeleteEmployee(employeeID);
                            Employees = employeeService.GetAllEmployees().ToList();

                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteEmployeeExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddEmployeeExecute(), param => CanAddEmployeeExecute());
                }
                return addEmployee;
            }
        }

        private void AddEmployeeExecute()
        {
            try
            {
                AddEmployee addEmployee = new AddEmployee();
                addEmployee.ShowDialog();

                if ((addEmployee.DataContext as AddEmployeeViewModel).IsUpdateUser == true)
                {
                    //we read users from database in case we added new idcard
                    Employees = employeeService.GetAllEmployees().ToList();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddEmployeeExecute()
        {
            return true;
        }

        private ICommand editEmployee;
        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        private void EditEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {
                    EditEmployee editEmployee = new EditEmployee(Employee);
                    editEmployee.ShowDialog();

                     
                    Employees = employeeService.GetAllEmployees().ToList();
                 

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditEmployeeExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
