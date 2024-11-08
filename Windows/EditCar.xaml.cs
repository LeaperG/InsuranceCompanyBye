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
using System.Text.RegularExpressions;
using InsuranceCompany.DB;
using System.Windows.Controls.Primitives;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditCar.xaml
    /// </summary>
    public partial class EditCar : Page
    {

        string selectedBrand = "";
        string selectedModel = "";


        List<string> listTypeDocument = new List<string>()
        {
            "Паспорт ТС",
            "ЭПТС"
        };



        public EditCar()
        {
                 InitializeComponent();

                CMBBrand.ItemsSource = ContextDB.CarInsuranceTariffs.Select(i => i.CarBrand).Distinct().ToList();
                CMBTypeVehicle.ItemsSource = listTypeDocument;
                CMBBrand.ItemsSource = ContextDB.Vehicles.Select(i => i.Brand).Distinct().ToList();
                CMBModel.ItemsSource = ContextDB.Vehicles.Select(i => i.Model).Distinct().ToList();
                CMBGeneration.ItemsSource = ContextDB.Vehicles.Select(i => i.Generation).Distinct().ToList();
                CMBUsagePurpose.ItemsSource = ContextDB.Vehicles.Select(i => i.UsagePurpose).Distinct().ToList();
                CMBVehicleCategory.ItemsSource = ContextDB.Vehicles.Select(i => i.VehicleCategory).Distinct().ToList();
                CMBVehicleType.ItemsSource = ContextDB.Vehicles.Select(i => i.VehicleType).Distinct().ToList();

                if(TempFile.BoolAddCar)
                {
                     CMBVehicleCategory.SelectedIndex = 0;
                     CMBVehicleType.SelectedIndex = 0;
                     CMBTypeVehicle.SelectedIndex = 0;
                     CMBUsagePurpose.SelectedIndex = 0;
                     CMBBrand.SelectedIndex = 0;
                     DatePickerDriverLicense.SelectedDate = DateTime.Now;
                }

            if (TempFile.SelectCar != null)
            {
                var car = ContextDB.Vehicles.ToList();
                var selectedCar = car.FirstOrDefault(i => (i.IdVehicles == TempFile.SelectCar.IdVehicles));

                
                TbPassportSeries.Text = selectedCar.VehicleDocuments.DocumentSeries;
                TbPassportNumber.Text = selectedCar.VehicleDocuments.DocumentNumber;
                DatePickerDriverLicense.SelectedDate = selectedCar.VehicleDocuments.DocumentIssueDate;

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
                CMBVehicleCategory.SelectedIndex = selectedCar.IdVehicleCategory -1;
                CMBVehicleType.SelectedIndex = selectedCar.IdVehicleType -1;
                CMBTypeVehicle.SelectedIndex = TempFileVehicleData.TypeVehicleDocument;


                TbPower.Text = selectedCar.Power.ToString();
                TbYear.Text = selectedCar.Year.ToString();
                CMBUsagePurpose.SelectedIndex = 0;
            }

        }

        private void SaveEditCar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Vehicles selectCar;
                if (TempFile.SelectCar != null)
                {
                    selectCar = TempFile.SelectCar;

                }
                else
                {
                    selectCar = new Vehicles();
                }

                selectCar.Brand = CMBBrand.SelectedItem.ToString();
                selectCar.Model = CMBModel.SelectedItem.ToString();
                selectCar.Generation = CMBGeneration.SelectedItem.ToString();
                selectCar.VIN = TbVIN.Text;
                selectCar.Power = Convert.ToInt32(TbPower.Text);
                selectCar.Year = Convert.ToInt32(TbYear.Text);
                selectCar.Country = TbCountry.Text;
                selectCar.RegistrationNumber = TbRegNumber.Text;
                selectCar.IdVehicleCategory = CMBVehicleCategory.SelectedIndex + 1;
                selectCar.IdVehicleType = CMBVehicleCategory.SelectedIndex + 1;
                selectCar.IdUsagePurpose = 1;
                selectCar.PeriodOfUse = "Владения авто 3 месяца";

                VehicleDocuments vd;
                if (TempFile.BoolAddCar)
                {
                    vd = new VehicleDocuments();

                }
                else
                {
                    vd = selectCar.VehicleDocuments;
                }

                TempFileVehicleData.TypeVehicleDocument = CMBTypeVehicle.SelectedIndex;
                vd.TypeDocument = CMBTypeVehicle.SelectedItem.ToString();
                vd.DocumentIssueDate = DatePickerDriverLicense.SelectedDate.Value;
                vd.DocumentSeries = TbPassportSeries.Text;
                vd.DocumentNumber = TbPassportNumber.Text;
                selectCar.IdVehicleDocuments = vd.IdVehicleDocuments;

                var client = TempFile.client;


                if (TempFile.BoolAddCar)
                {
                    selectCar.Clients.Add(client);
                    client.Vehicles.Add(selectCar);
                    ContextDB.VehicleDocuments.Add(vd);
                    ContextDB.Vehicles.Add(selectCar);
                }   

     

                ContextDB.SaveChanges();
                NavigationService.Navigate(new PersonalAccount());

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalAccount());
        }

        private void CMBModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CMBModel.SelectedIndex >= 0)
            {
                selectedModel = CMBModel.SelectedItem.ToString();
                CMBGeneration.ItemsSource = ContextDB.CarInsuranceTariffs
                              .Where(i => i.CarModel.Equals(selectedModel))
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

        private void CMBVehicleCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CMBVehicleType.SelectedIndex = CMBVehicleCategory.SelectedIndex;
        }

        private void DatePickerDriverLicense_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "Дата выдачи: " || textBox.Text.Length < 8)
            {
                textBox.Text = "";
            }



            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);
                DatePickerDriverLicense.Text = newText;
                textBox.Text = newText;
            }
        }

        private void DatePickerDriverLicense_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);

                textBox.Text = "Дата выдачи: " + newText;
            }
        }

        private void DatePickerDriverLicense_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDriverLicense.Text != null)
            {
                DateTime selectedDate = DatePickerDriverLicense.SelectedDate.Value;

                // Добавляем год
                DateTime newDate = selectedDate.AddYears(1);

            }
        }
    }
}
