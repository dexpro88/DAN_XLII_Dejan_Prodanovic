using DAN_XLII_Dejan_Prodanovic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAN_XLII_Dejan_Prodanovic.Views
{
    /// <summary>
    /// Interaction logic for EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public EditEmployee(vwEmployee employeeEdit)
        {
            InitializeComponent();
            this.DataContext = new EditEmployeeViewModel(this, employeeEdit);
        }
    }
}
