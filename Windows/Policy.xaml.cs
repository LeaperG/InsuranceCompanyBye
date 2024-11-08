using InsuranceCompany.DB;
using InsuranceCompany.HellperClass;
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
    /// Логика взаимодействия для Policy.xaml
    /// </summary>
    public partial class Policy : Page
    {

        public Policy()
        {
            InitializeComponent();

            CMBStatus.ItemsSource = ContextDB.Status.ToList();
            CMBStatus.SelectedIndex = 0;

            if (TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3)
            {
                CMBStatus.IsEnabled = true;
            }
            else
            {
                CMBStatus.IsEnabled = false;
            }


            if (TempFile.user.IdRole == 2 && TempFile.carPoliciesContinue != true)
            {
                BtnSave.Visibility = Visibility.Collapsed;
                Btnback.Margin = new Thickness(0, 0, 0, 50);
                Grid.SetColumnSpan(Btnback, 4);
            }

            if (TempFile.SelectPolicy != null)
            {
                NextPage.Visibility = Visibility.Collapsed;

                var policy = ContextDB.Policies.ToList();
                var selectedPolicy = policy.FirstOrDefault(i => (i.IdPolicy == TempFile.SelectPolicy.IdPolicy));

                TempFile.SelectPolicy = selectedPolicy;

                var car = ContextDB.Vehicles.ToList();
                var selectedCar = car.FirstOrDefault(i => (i.IdVehicles == selectedPolicy.IdVehicles));

                var client = ContextDB.Clients.ToList();
                var selectClient = client.FirstOrDefault(i => (i.IdClient == selectedPolicy.IdClient));

                var owner = ContextDB.Owner.ToList();
                var selectOwner = owner.FirstOrDefault(i => (i.IdOwner == selectedPolicy.IdOwner));



                TxbInsuranseCost.Text = selectedPolicy.InsuranseCost.ToString();
                TxbPolicyNumber.Text = "XXX" + selectedPolicy.IdPolicy;
                //TxbCoverageAmount.Text = "72000.00 Рублей";
                double cost = Convert.ToDouble(TxbInsuranseCost.Text) * 10;
                TxbCoverageAmount.Text = Convert.ToString(cost);
                TxbStartDate.Text = selectedPolicy.StartDate.ToShortDateString();
                TxbEndDate.Text = selectedPolicy.EndDate.ToShortDateString();
                TxbRegistrationNumber.Text = selectedCar.RegistrationNumber + " " + selectedCar.Country;
                TxbCar.Text = selectedCar.Brand + " " + selectedCar.Model + " " + selectedCar.Generation;
                //TxbUsagePurpose.Text = selectedCar.IdUsagePurpose.ToString();
                TxbUsagePurpose.Text = "Личное";
                TxbVIN.Text = selectedCar.VIN;
                TxbFIO.Text = selectClient.LastName + " " + selectClient.FirstName + " " + selectClient.Patronymic;
                TxbOwner.Text = selectOwner.LastName + " " + selectOwner.FirstName + " " + selectOwner.Pantronymic;
                TxbAgent.Text = selectedPolicy.Agents.FirstName + " " + selectedPolicy.Agents.LastName + " " + selectedPolicy.Agents.Patronymic;
                CMBStatus.SelectedIndex = selectedPolicy.IdStatus - 1;
            }
            else
            {

                BtnSave.Visibility = Visibility.Collapsed;

                TxbInsuranseCost.Text = TempFile.InsuranseCost;
                TxbPolicyNumber.Text = "XXX";
                //TxbCoverageAmount.Text = "72000.00 Рублей";
                double cost = Convert.ToDouble(TxbInsuranseCost.Text) * 10;
                TxbCoverageAmount.Text =Convert.ToString(cost);
                TxbStartDate.Text = TempFileCalc.DatePickerStart.ToShortDateString();
                TxbEndDate.Text = TempFileCalc.DateEnd.ToString();
                TxbRegistrationNumber.Text = TempFileVehicleData.RegNumber + " " + TempFileVehicleData.Country;
                TxbCar.Text = TempFileVehicleData.Brand + " " + TempFileVehicleData.Model + " " + TempFileVehicleData.Generation;
                TxbUsagePurpose.Text = TempFileVehicleData.UsagePurposeText.ToString();
                TxbVIN.Text = TempFileVehicleData.VIN;
                TxbFIO.Text = TempFileInsurant.LastName + " " + TempFileInsurant.FirstName + " " + TempFileInsurant.Patronymic;
                TxbOwner.Text = TempFileOwner.LastName + " " + TempFileOwner.FirstName + " " + TempFileOwner.Patronymic;
            }

            if (TxbAgent.Text == "")
            {
                TxbAgent.Visibility = Visibility.Hidden;
                HintAgent.Visibility = Visibility.Hidden;
            }
        }

        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            //if (TempFile.DriverShow != true && TempFile.user.IdRole == 2)
            //{
            //    NavigationService.Navigate(new Administrator());
            //}
            //else
            //{
            //    NavigationService.Navigate(new Administrator());
            //}
                NavigationService.Navigate(new Administrator());
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Policies policies = new Policies();
            
                int allPolicies = ContextDB.Policies.ToList().Count;

                double cost = Convert.ToDouble(TxbInsuranseCost.Text) * 11;


                policies.PolicyNumber = TxbPolicyNumber.Text.Trim() + allPolicies;
                policies.CoverageAmount = Convert.ToDecimal(cost);
                policies.StartDate = Convert.ToDateTime(TxbStartDate.Text);
                policies.EndDate = Convert.ToDateTime(TxbEndDate.Text);
                policies.IdClient = TempFile.client.IdClient;
                //policies.IdOwner = 1;
                //policies.IdVehicles = 1;
                policies.IdAgent = 1;
                policies.IdStatus = 1;
                policies.InsuranseCost = Convert.ToDecimal(TxbInsuranseCost.Text);



                Owner owner = new Owner();
                policies.Owner = owner;

                //Owner
                owner.LastName = TempFileOwner.LastName;
                owner.FirstName = TempFileOwner.FirstName;
                owner.Pantronymic = TempFileOwner.Patronymic;
                owner.BirthDate = TempFileOwner.DatePickerBirthDate;
                owner.PassportSeries = TempFileOwner.PassportSeries;
                owner.PassportNumber = TempFileOwner.PassportNumber;

                //owner.IdAddress = 1;
                owner.IdGender = TempFileOwner.Gender;

                Address address = new Address();
                //owner.IdAddress = address.IdAddress;
                owner.Address = address;

                address.Region = TempFileOwner.Region;
                address.City = TempFileOwner.City;
                address.Street = TempFileOwner.Street;
                address.House = TempFileOwner.House;
                address.Apartment = TempFileOwner.Apartment;


                Vehicles vehicles = new Vehicles();
                policies.Vehicles = vehicles;

                vehicles.RegistrationNumber = TempFileVehicleData.RegNumber;
                vehicles.Country = TempFileVehicleData.Country;
                vehicles.VIN = TempFileVehicleData.VIN;
                vehicles.Brand = TempFileVehicleData.Brand;
                vehicles.IdVehicleCategory = TempFileVehicleData.VehicleCategory +1;
                vehicles.IdVehicleType = TempFileVehicleData.VehicleType + 1;
                vehicles.Model = TempFileVehicleData.Model;
                vehicles.Year = TempFileVehicleData.Year;
                vehicles.Generation = TempFileVehicleData.Generation;
                vehicles.Power = TempFileVehicleData.Power;
                vehicles.PeriodOfUse = TempFileCalc.PeriodOfUseText;
                vehicles.IdUsagePurpose = TempFileVehicleData.CMBUsagePurpose + 1;




                VehicleDocuments vehicleDocuments = new VehicleDocuments();
                vehicles.VehicleDocuments = vehicleDocuments;
                vehicleDocuments.TypeDocument = TempFileCalc.TypeVehicleText;
                vehicleDocuments.DocumentSeries = TempFileCalc.PassportSeries;
                vehicleDocuments.DocumentNumber = TempFileCalc.PassportNumber;
                vehicleDocuments.DocumentIssueDate = TempFileCalc.DatePickerDriverLicense;



                //Drivers driver = policies.Drivers;
            

                foreach (var driver in DriverManager.Drivers)
                {
                    Drivers drivers = new Drivers();
                    driver.IdGender = driver.IdGender;
                    drivers.LastName = driver.LastName;
                    drivers.FirstName = driver.FirstName;
                    drivers.Patronymic = driver.Patronymic;
                    drivers.BirthDate = driver.BirthDate;
                    drivers.DriverLicenseSeries = driver.DriverLicenseSeries;
                    drivers.DriverLicenseNumber = driver.DriverLicenseNumber;
                    drivers.DataDriverLicense = driver.DataDriverLicense;
                    drivers.IdGender = 1;
                    ContextDB.Drivers.Add(drivers);
                    policies.Drivers.Add(drivers);
                }


                owner.Policies.Add(policies);
                vehicles.Policies.Add(policies);
                ContextDB.Vehicles.Add(vehicles);
                ContextDB.VehicleDocuments.Add(vehicleDocuments);
                ContextDB.Policies.Add(policies);
                address.Owner.Add(owner);
                ContextDB.Address.Add(address);
                ContextDB.Owner.Add(owner);
                ContextDB.SaveChanges();

                MessageBox.Show("Ваша страховка на рассмотрении, Информацию можете узнать в приложении,СМС и Почте", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new PersonalAccount());

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }

        }

        private void BtnDrivers_Click(object sender, RoutedEventArgs e)
        {
            TempFile.DriverShow = true;
            NavigationService.Navigate(new PageDrivers());
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Policies policies;
                policies = TempFile.SelectPolicy;
                policies.IdStatus = CMBStatus.SelectedIndex + 1;
                policies.IdAgent = TempFile.user.IdRole;
                TxbAgent.Text = policies.Agents.FirstName + " " + policies.Agents.LastName + " " + policies.Agents.Patronymic;
                TxbAgent.Visibility = Visibility.Visible;
                HintAgent.Visibility = Visibility.Visible;
                ContextDB.SaveChanges();
                NavigationService.Navigate(new Administrator());

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
    }
}
