using InsuranceCompany.HellperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PageInsuranceOverview.xaml
    /// </summary>
    public partial class PageInsuranceOverview : Page
    {
        public PageInsuranceOverview()
        {
            InitializeComponent();


            if (TempFile.user != null)
            {
                if (TempFile.user.IdRole == 3)
                {
                    BtnAdministrator.Content = "РАБота";
                }


                if (TempFile.user.IdRole == 1  || TempFile.user.IdRole == 3)
                {
                    BtnAdministrator.Visibility = Visibility.Visible;
                    BtnPersonalAccount.Visibility = Visibility.Collapsed;
                    BtnPersonalAccount.Content = "Машины";
                }
                else
                {
                    BtnAdministrator.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                BtnAdministrator.Visibility = Visibility.Collapsed;
            }



            if (TempFile.Auth != false)
            {
                BtnAuth.Visibility = Visibility.Collapsed;
                BtnReg.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnPersonalAccount.Visibility = Visibility.Collapsed;
                BtnSignOut.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnComprehensive_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Registration());
        }

        private void BtnCTP_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCTPVehicleData());
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            TempFile.Reg = true;
            NavigationService.Navigate(new Registration());
        }

        private void BtnPersonalAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalAccount());
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            TempFile.Reset();
            TempFile.client = null;
            TempFileVehicleData.Reset();
            TempFileCalc.Reset();
            DriverManager.Reset();
            TempFileInsurant.Reset();
            TempFileOwner.Reset();

            TempFile.Auth = false;
            TempFile.user = null;
            BtnAdministrator.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new PageInsuranceOverview());
        }

        private void BtnAdministrator_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Administrator());
        }

        private void BtnClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClaimsList());
        }
    }
}
