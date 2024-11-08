using InsuranceCompany.DB;
using InsuranceCompany.HellperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Page
    {
        public PersonalAccount()
        {
            InitializeComponent();
            ListViewUpdate();
        }


        //var client = ContextDB.Clients.ToList();
        //var selectClient = client.FirstOrDefault(i => (i.IdUser == user.IdUser));




        private void ListViewUpdate()
        {
            var cars = ContextDB.Vehicles.ToList();

            if (TempFile.client == null)
            {
                TbHead.Text = "Все машины";
                cars = ContextDB.Vehicles.ToList();
            }
            else
            {
                //EditCar.Visibility = Visibility.Collapsed;
                cars = cars.Where(i => i.Clients.Contains(TempFile.client)).ToList();
            }
            cars = cars.Where(i => i.Brand.ToLower().Contains(TbSearch.Text.ToLower()) || Convert.ToString(i.IdVehicles).Contains(TbSearch.Text) || i.Model.ToLower().Contains(TbSearch.Text.ToLower())).ToList();
            LvCars.ItemsSource = cars;


            var user = TempFile.user;

            if (user.IdRole == 1 || user.IdRole == 3)
            {
                BtnPersonal.Visibility = Visibility.Collapsed;
                BtnAddCar.Visibility = Visibility.Collapsed;
                BtnPolicies.Visibility = Visibility.Collapsed;
            }
        }


            
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3)
            {
                NavigationService.Navigate(new ClientList());
            }
            else
            {
                NavigationService.Navigate(new PageInsuranceOverview());
            }
        }

        private void EditCar_Click(object sender, RoutedEventArgs e)
        {
            TempFile.SelectCar = LvCars.SelectedItem as Vehicles; // вроде не нужно

            if (LvCars.SelectedItem == null)
            {
                return;
            }

            NavigationService.Navigate(new EditCar());

        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            TempFile.BoolAddCar = true;
            TempFile.SelectCar = null;
            NavigationService.Navigate(new EditCar());
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void BtnPersonal_Click(object sender, RoutedEventArgs e)
        {
            TempFile.Personal = true;
            NavigationService.Navigate(new Personal());
        }

        private void BtnPolicies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Administrator());
        }

        private void BtnCTP_Click(object sender, RoutedEventArgs e)
        {
            //TempFile.SelectCar = LvCars.SelectedItem as Vehicles; // вроде не нужно

            //if (LvCars.SelectedItem == null)
            //{
            //    return;
            //}

            //NavigationService.Navigate(new PageCTPVehicleData());


           TempFile.carPoliciesContinue = true;
           MessageBoxResult result = MessageBox.Show("Оформить страховку ?", "Вопрос",
           MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                if (button == null)
                {
                    return;
                }

                var carNum = button.DataContext as Vehicles;
                var cars = ContextDB.Vehicles.ToList();
                var selectcar = cars.FirstOrDefault(i => i.IdVehicles == carNum.IdVehicles);

                TempFile.SelectCar = selectcar;
                NavigationService.Navigate(new PageCTPVehicleData());
            }

        }
    }
}
