using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Text.RegularExpressions;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PageCTPVehicleData.xaml
    /// </summary>
    public partial class PageCTPVehicleData : Page
    {
        string selectedBrand = "";
        string selectedModel = "";
        public PageCTPVehicleData()
        {
            InitializeComponent();


          
                  CMBBrand.ItemsSource = ContextDB.CarInsuranceTariffs.Select(i => i.CarBrand).Distinct().ToList();
                  /*CMBBrand.SelectedIndex = 0;

                  //CMBModel.ItemsSource = ContextDB.CarInsuranceTariffs
                  //                .Where(i => i.CarBrand == selectedBrand)
                  //                .Select(i => i.CarModel)
                  //                .Distinct()
                  //                .ToList();
                  //CMBModel.SelectedIndex = 0;*/

                  CMBVehicleCategory.ItemsSource = ContextDB.VehicleCategory.ToList();
                  CMBVehicleType.ItemsSource = ContextDB.VehicleType.ToList();
                  CMBUsagePurpose.ItemsSource = ContextDB.UsagePurpose.ToList();


            if (TempFile.PageCTPVehicleDataBack == 0)
            {
                CMBBrand.SelectedIndex = 0;
                CMBVehicleCategory.SelectedIndex = 1;
                CMBVehicleType.SelectedIndex = CMBVehicleCategory.SelectedIndex;
                CMBUsagePurpose.SelectedIndex = 0;
                TempFile.PageCTPVehicleDataBack = 1;
            }
            else
            {
                CMBBrand.SelectedIndex = TempFileVehicleData.CMBBrand;
                CMBModel.SelectedIndex = TempFileVehicleData.CMBModel;
                CMBGeneration.SelectedIndex = TempFileVehicleData.CMBGeneration;
                TbRegNumber.Text = TempFileVehicleData.RegNumber;
                TbCountry.Text = TempFileVehicleData.Country;
                TbVIN.Text = TempFileVehicleData.VIN;
                CMBVehicleCategory.SelectedIndex = TempFileVehicleData.VehicleCategory;
                CMBVehicleType.SelectedIndex = TempFileVehicleData.VehicleType;
                TbPower.Text = TempFileVehicleData.Power.ToString();
                CMBUsagePurpose.SelectedIndex = TempFileVehicleData.CMBUsagePurpose;
            }

            if (TempFile.SelectCar != null)
            {
                var car = ContextDB.Vehicles.ToList();
                var selectedCar = car.FirstOrDefault(i => (i.IdVehicles == TempFile.SelectCar.IdVehicles));


                //TbPassportSeries.Text = selectedCar.VehicleDocuments.DocumentSeries;
                //TbPassportNumber.Text = selectedCar.VehicleDocuments.DocumentNumber;
                //DatePickerDriverLicense.SelectedDate = selectedCar.VehicleDocuments.DocumentIssueDate;

                foreach (var item in CMBBrand.Items)
                {
                    if (item.ToString() == selectedCar.Brand.ToString())
                    {
                        CMBBrand.SelectedItem = item;
                        break; // Прервать цикл, как только найден нужный элемент.
                    }
                }


                foreach (var item in CMBModel.Items)
                {
                    if (item.ToString() == selectedCar.Model.ToString())
                    {
                        CMBModel.SelectedItem = item;
                        break; // Прервать цикл, как только найден нужный элемент.
                    }
                }


                foreach (var item in CMBGeneration.Items)
                {
                    if (item.ToString() == selectedCar.Generation.ToString())
                    {
                        CMBGeneration.SelectedItem = item;
                        break; // Прервать цикл, как только найден нужный элемент.
                    }
                }

                TbRegNumber.Text = selectedCar.RegistrationNumber.ToString();
                TbCountry.Text = selectedCar.Country.ToString();
                TbVIN.Text = selectedCar.VIN.ToString();
                CMBVehicleCategory.SelectedIndex = selectedCar.IdVehicleCategory - 1;
                CMBVehicleType.SelectedIndex = selectedCar.IdVehicleType - 1;
                //CMBTypeVehicle.SelectedIndex = TempFileVehicleData.TypeVehicleDocument + 1;


                TbPower.Text = selectedCar.Power.ToString();
                TbYear.Text = selectedCar.Year.ToString();
                CMBUsagePurpose.SelectedIndex = 0;
            }


           /* //if (TempFile.SelectCar != null)
            //{
            //    CMBBrand.ItemsSource = ContextDB.Vehicles.Select(i => i.Brand).Distinct().ToList();
            //    CMBModel.ItemsSource = ContextDB.Vehicles.Select(i => i.Model).Distinct().ToList();


            //    //var car = ContextDB.Vehicles.Select(i => i.IdVehicles == TempFile.SelectCar.IdVehicles).Distinct().ToList();
            //    var car = ContextDB.Vehicles.ToList();
            //    var selectedCar = car.FirstOrDefault(i => (i.IdVehicles == TempFile.SelectCar.IdVehicles));

            //    foreach (var item in CMBBrand.Items)
            //    {
            //        if (item.ToString() == selectedCar.Brand.ToString())
            //        {
            //            CMBBrand.SelectedItem = item;
            //            break; // Прервать цикл, как только найден нужный элемент.
            //        }
            //    }

            //    //CMBBrand.SelectedItem.Equals(CMBBrand.ToString() == selectedCar.Brand.ToString());
            //    // CMBBrand.SelectedItem = CMBBrand.Items.OfType<ComboBoxItem>().FirstOrDefault(x => x.Name == selectedCar.Brand.ToString());

            //    //CMBModel.SelectedIndex = TempFileVehicleData.CMBModel;



            //    foreach (var item in CMBModel.Items)
            //    {
            //        if (item.ToString() == selectedCar.Model.ToString())
            //        {
            //            CMBModel.SelectedItem = item;
            //            break; // Прервать цикл, как только найден нужный элемент.
            //        }
            //    }

            //    CMBGeneration.SelectedIndex = TempFileVehicleData.CMBGeneration;
            //    TbRegNumber.Text = selectedCar.RegistrationNumber.ToString();
            //    TbCountry.Text = selectedCar.Country.ToString();
            //    TbVIN.Text = selectedCar.VIN.ToString();
            //    CMBVehicleCategory.SelectedIndex = TempFileVehicleData.VehicleCategory;
            //    CMBVehicleType.SelectedIndex = TempFileVehicleData.VehicleType;
            //    TbPower.Text = TempFileVehicleData.Power.ToString();
            //    CMBUsagePurpose.SelectedIndex = TempFileVehicleData.CMBUsagePurpose;



            //    TempFile.SelectCar = null;
            //}
            //else*/

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (TbRegNumber.Text == "")
                {
                    MessageBox.Show("введите номер автомообиля !");
                    return;
                }

                if (TbCountry.Text == "")
                {
                    MessageBox.Show("введите страну номера !");
                    return;
                }


                //if (!Regex.IsMatch(TbCountry.Text, "^[a-za-z]+$"))
                //{
                //    MessageBox.Show("введите текст страну номера авто на английском правильно!");
                //    return;
                //}





                if (TbVIN.Text == "")
                {
                    MessageBox.Show("введите vin номер !");
                    return;
                }


                //if (!Regex.IsMatch(TbVIN.Text, "^[a-za-z]+$"))
                //{
                //    MessageBox.Show("введите vin на английском правильно!");
                //    return;
                //}



                //if (tbbrand.text == "")
                //{
                //    messagebox.show("введите бренд машины !");
                //    return;
                //}

                if (TbYear.Text == "")
                {
                    MessageBox.Show("введите год выпуска машины !");
                    return;
                }

                //if (modification.text == "")
                //{
                //    messagebox.show("введите vin номер !");
                //    return;
                //}

                if (TbPower.Text == "")
                {
                    MessageBox.Show("введите мощность!");
                    return;
                }


                if (!Regex.IsMatch(TbPower.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Введите число правильно!");
                    TbPower.Text = "";
                    return;
                }



                TempFileVehicleData.CMBBrand = CMBBrand.SelectedIndex;
                TempFileVehicleData.CMBModel = CMBModel.SelectedIndex;
                TempFileVehicleData.CMBGeneration = CMBGeneration.SelectedIndex;
                TempFileVehicleData.CMBUsagePurpose = CMBGeneration.SelectedIndex;
                TempFileVehicleData.Brand = CMBBrand.SelectedItem.ToString();
                TempFileVehicleData.Model = CMBModel.SelectedItem.ToString();
                TempFileVehicleData.Generation = CMBGeneration.SelectedItem.ToString();
                TempFileVehicleData.RegNumber = TbRegNumber.Text;
                TempFileVehicleData.Country = TbCountry.Text;
                TempFileVehicleData.VIN = TbVIN.Text;
                TempFileVehicleData.VehicleCategory = CMBVehicleCategory.SelectedIndex;
                TempFileVehicleData.VehicleType = CMBVehicleType.SelectedIndex;
                //TempFileVehicleData.Modification = TbModification.Text;
                if (TbPower.Text != "")
                {
                    TempFileVehicleData.Power = Convert.ToDouble(TbPower.Text);
                }

                if (TbYear.Text != "")
                {
                    TempFileVehicleData.Year = Convert.ToInt32(TbYear.Text);
                }

                TempFileVehicleData.CMBUsagePurpose = CMBUsagePurpose.SelectedIndex;
                TempFileVehicleData.UsagePurposeText = CMBUsagePurpose.Text;
                NavigationService.Navigate(new PageCTPCalc());


            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }
        }

        private void BackPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageInsuranceOverview());
        }

        private void CMBVehicleCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CMBVehicleType.SelectedIndex = CMBVehicleCategory.SelectedIndex;
        }

        private void CMBBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBrand = CMBBrand.SelectedItem.ToString();
            CMBModel.ItemsSource = ContextDB.CarInsuranceTariffs
                            .Where(i => i.CarBrand.Equals(selectedBrand))
                            .Select(i => i.CarModel)
                            .Distinct()
                            .ToList();
            CMBModel.SelectedIndex = 0;
        }


        private void CMBModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CMBModel.SelectedIndex >= 0)
            {
                 selectedModel = CMBModel.SelectedItem.ToString();
                 CMBGeneration.ItemsSource = ContextDB.CarInsuranceTariffs
                               .Where(i =>  i.CarModel.Equals(selectedModel))
                               .Select(i => i.Generation)
                               .Distinct()
                               .ToList();
                 CMBGeneration.SelectedIndex = 0;
            }
            else
            {
                return;
            }
        }

        private void CheckReset_Checked(object sender, RoutedEventArgs e)
        {
            CMBBrand.SelectedIndex = 0;
            CMBModel.SelectedIndex = 0;
            CMBGeneration.SelectedIndex = 0;
            TbRegNumber.Text = "";
            TbCountry.Text = "";
            TbVIN.Text = "";
            CMBVehicleCategory.SelectedIndex = 0;
            CMBVehicleType.SelectedIndex = 0;
            TbPower.Text = null;
            CMBUsagePurpose.SelectedIndex = 0;
            
            CheckReset.IsChecked = false;
        }
    }
}
