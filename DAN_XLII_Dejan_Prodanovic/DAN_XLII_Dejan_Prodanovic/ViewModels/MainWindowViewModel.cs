using DAN_XLII_Dejan_Prodanovic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAN_XLII_Dejan_Prodanovic.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;

        ILocationService locationService;


        #region Constructor

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;

            locationService = new LocationService();

            if (locationService.GetAllLocations().Count == 0)
            {
                LocationFileActions.AddLocationsToDatabase();
              
            }
        }
        #endregion
    }
}
