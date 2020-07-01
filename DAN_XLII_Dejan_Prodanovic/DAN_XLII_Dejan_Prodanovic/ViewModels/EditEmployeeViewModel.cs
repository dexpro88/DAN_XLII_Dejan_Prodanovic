using DAN_XLII_Dejan_Prodanovic.Commands;
using DAN_XLII_Dejan_Prodanovic.Services;
using DAN_XLII_Dejan_Prodanovic.Validations;
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
    class EditEmployeeViewModel : ViewModelBase
    {
        EditEmployee editEmployee;
        ILocationService locationService;
        IEmployeeService employeeService;
        IGenderService genderService;
        ISectorService sectorService;

        #region Constructor
        public EditEmployeeViewModel(EditEmployee editEmployeeOpen,vwEmployee employeeEdit)
        {
            editEmployee = editEmployeeOpen;
            selectedMenager = new vwMenager();
            employee = employeeEdit;
            selctedLocation = new vwLOCATION();
            selectedMenager = new vwMenager();
            //selectedMenager.Menager = employee.MenagerName;
            StartDate = (DateTime)employee.DateOfBirth;
            Sector = employee.SectorName;

            locationService = new LocationService();
            employeeService = new EmployeeService();
            genderService = new GenderService();
            sectorService = new SectorService();

            LocationList = locationService.GetAllLocations().ToList();
            LocationList.OrderByDescending(x => x.Location);
            LocationList.Reverse();

            PotentialMenagers = employeeService.GetAllPotentialMenagersForEditWindow(employeeEdit.EmployeeID);

        }


        #endregion

        #region Properties
        private vwLOCATION selctedLocation;
        public vwLOCATION SelctedLocation
        {
            get
            {
                return selctedLocation;
            }
            set
            {
                selctedLocation = value;
                OnPropertyChanged("SelctedLocation");
            }
        }

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

        private vwMenager selectedMenager;
        public vwMenager SelectedMenager
        {
            get
            {
                return selectedMenager;
            }
            set
            {
                selectedMenager = value;
                OnPropertyChanged("SelectedMenager");
            }
        }

        private string sector;
        public string Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private string gender = "male";
        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<vwLOCATION> locationList;
        public List<vwLOCATION> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private List<vwMenager> potentialMenagers;
        public List<vwMenager> PotentialMenagers
        {
            get
            {
                return potentialMenagers;
            }
            set
            {
                potentialMenagers = value;
                OnPropertyChanged("PotentialMenagers");
            }
        }



        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }


        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }
        #endregion

        #region Commands

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {

                if (!ValidationClass.JMBGisValid(employee.JMBG))
                {
                    MessageBox.Show("JMBG  is not valid.");
                    return;
                }

                if (!ValidationClass.JMBGIsUniqueForEditWindow(employee.JMBG,employee))
                {
                    MessageBox.Show("JMBG  already exists in database");
                    return;
                }

                if (!ValidationClass.RegisterNumberIsValid(employee.RegistrationNumber))
                {
                    MessageBox.Show("Registration number  is not valid");
                    return;
                }
                if (!ValidationClass.RegNumberIsUniqueForEdit(employee.RegistrationNumber,employee))
                {
                    MessageBox.Show("Registration number  already exists in database");
                    return;
                }
                if (!ValidationClass.TelfonNumberValid(employee.TelefonNumber))
                {
                    MessageBox.Show("Telefon number  is not valid. It must have 9 numbers");
                    return;
                }
                employee.LocationID = SelctedLocation.LocationID;
                employee.DateOfBirth = StartDate;
                if (String.IsNullOrEmpty(selectedMenager.Menager))
                {
                    vwMenager menagerDB = employeeService.GetMenagerByName(" ");
                    employee.MenagerID = menagerDB.EmployeeID;
                    MessageBox.Show(employee.MenagerName.ToString());
                }
                else
                {
                    employee.MenagerID = selectedMenager.EmployeeID;
                    MessageBox.Show(employee.MenagerName.ToString());

                }

                string genderForDB;

                if (Gender.Equals("male"))
                {
                    genderForDB = "M";
                }
                else if (Gender.Equals("female"))
                {
                    genderForDB = "F";
                }
                else
                {
                    genderForDB = "O";
                }

                tblGender gender = genderService.GetGenderByName(genderForDB);
                tblSector sectorDB = sectorService.GetSectorByName(Sector);

                if (sectorDB == null)
                {

                    sectorDB = new tblSector();
                    sectorDB.SectorName = Sector;
                    sectorDB = sectorService.AddSector(sectorDB);
                    employee.SectorID = sectorDB.SectorID;

                }
                else
                {
                    employee.SectorID = sectorDB.SectorID;

                }

                employee.GenderID = gender.GenderID;

                isUpdateUser = true;


                employeeService.EditEmployee(employee);

                editEmployee.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {

            if (String.IsNullOrEmpty(Employee.FirstName) || String.IsNullOrEmpty(Employee.FirstName) ||
                String.IsNullOrEmpty(Employee.JMBG) || String.IsNullOrEmpty(Employee.RegistrationNumber) ||
                String.IsNullOrEmpty(Employee.TelefonNumber) || String.IsNullOrEmpty(SelctedLocation.Location) ||
                String.IsNullOrEmpty(Sector)
               )
            {
                return false;
            }
            else
            {
                return true;
            }
            //return true;
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                editEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }


        private ICommand chooseGender;
        public ICommand ChooseGender
        {
            get
            {
                if (chooseGender == null)
                {
                    chooseGender = new RelayCommand(ChooseGenderExecute, CanChooseGenderExecute);
                }
                return chooseGender;
            }
        }

        private void ChooseGenderExecute(object parameter)
        {
            Gender = (string)parameter;
        }

        private bool CanChooseGenderExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion
    }
}
