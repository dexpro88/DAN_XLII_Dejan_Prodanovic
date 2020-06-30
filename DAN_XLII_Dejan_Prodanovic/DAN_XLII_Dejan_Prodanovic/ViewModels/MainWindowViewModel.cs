using DAN_XLII_Dejan_Prodanovic.Commands;
using DAN_XLII_Dejan_Prodanovic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLII_Dejan_Prodanovic.ViewModels
{
    class MainWindowViewModel : ViewModelBase
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
        #endregion
    }
}
