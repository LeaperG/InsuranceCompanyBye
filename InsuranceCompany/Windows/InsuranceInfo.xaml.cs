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
using System.Windows.Navigation;
using System.Windows.Shapes;

using static InsuranceCompany.HellperClass.EFClass;
using InsuranceCompany.HellperClass;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для InsuranceInfo.xaml
    /// </summary>
    public partial class InsuranceInfo : Page
    {
        public InsuranceInfo()
        {
            InitializeComponent();

            TxbInsuranseCost.Text = TempFile.InsuranseCost;
            TxbPolicyNumber.Text = "XXX411244412412";
            TxbCoverageAmount.Text = "72000.00 Рублей";
            TxbStartDate.Text = TempFileCalc.DatePickerStart.ToShortDateString();
            TxbEndDate.Text = TempFileCalc.DateEnd.ToString();
            TxbRegistrationNumber.Text = TempFileVehicleData.RegNumber + " " +TempFileVehicleData.Country;
            TxbCar.Text = TempFileVehicleData.Brand + " " + TempFileVehicleData.Model + " " + TempFileVehicleData.Generation;
            TxbUsagePurpose.Text = TempFileVehicleData.UsagePurposeText.ToString();
            TxbWin.Text = TempFileVehicleData.VIN;
            TxbFIO.Text = TempFileInsurant.LastName + " " + TempFileInsurant.FirstName + " " + TempFileInsurant.Patronymic;
        }
    }
}
