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
using InsuranceCompany.DB;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PageOwner.xaml
    /// </summary>
    public partial class PageOwner : Page
    {

        public PageOwner()
        {
            InitializeComponent();
            CMBGender.ItemsSource = ContextDB.Gender.ToList();
            CMBGender.SelectedIndex = 0;

            if (TempFile.client == null)
            {
                CheckInfoOwner.Visibility = Visibility.Collapsed;
            }
            else
            {
                CheckInfoOwner.Visibility = Visibility.Visible;
            }

            if (TempFile.PageOwnerBack == 0)
            {
                DatePickerBirthDate.SelectedDate = DateTime.Now;
                TempFileOwner.DatePickerBirthDate = DateTime.Now;
                TempFile.PageOwnerBack = 1;
            }
            else
            {
                TbFirstName.Text = TempFileOwner.FirstName;
                TbLastName.Text = TempFileOwner.LastName;
                TbPatronymic.Text = TempFileOwner.Patronymic;
                DatePickerBirthDate.SelectedDate = TempFileOwner.DatePickerBirthDate;
                CMBGender.SelectedIndex = TempFileOwner.Gender-1;
                TbPassportSeries.Text = TempFileOwner.PassportSeries;
                TbPassportNumber.Text = TempFileOwner.PassportNumber;
                TbRegion.Text = TempFileOwner.Region;
                TbCity.Text = TempFileOwner.City;
                TbStreet.Text = TempFileOwner.Street;
                TbHouse.Text = TempFileOwner.House;
                TbApartment.Text = TempFileOwner.Apartment;
            }
        }

        private void DatePickerBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerBirthDate.Text != null)
            {
                DateTime selectedDate = DatePickerBirthDate.SelectedDate.Value;
                DateTime newDate = selectedDate.AddYears(1);
            }
        }

        private void DatePickerBirthDate_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "Дата рождения: " || textBox.Text.Length < 8)
            {
                textBox.Text = "";
            }

            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                string newText = textBox.Text.Substring(firstDigitIndex);
                DatePickerBirthDate.Text = newText;
                textBox.Text = newText;
            }
        }

        private void DatePickerBirthDate_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                string newText = textBox.Text.Substring(firstDigitIndex);

                textBox.Text = "Дата рождения: " + newText;
            }
        }

        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageInsurant());
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TbFirstName.Text == "")
                {
                    MessageBox.Show("Введите Имя");
                    return;
                }


                if (TbLastName.Text == "")
                {
                    MessageBox.Show("Введите Фамилию");
                    return;
                }

                if (DatePickerBirthDate.SelectedDate == null)
                {
                    MessageBox.Show("Введите Дату рождения !");
                    return;
                }

                if (TbPassportSeries.Text == "")
                {
                    MessageBox.Show("Введите серию Паспорта !");
                    return;
                }

                if (TbPassportNumber.Text == "")
                {
                    MessageBox.Show("Введите номер Паспорта !");
                    return;
                }

                if (TbRegion.Text == "")
                {
                    MessageBox.Show("Введите Ресублика/Край/область !");
                    return;
                }

                if (TbCity.Text == "")
                {
                    MessageBox.Show("Введите Населённый пункт !");
                    return;
                }

                if (TbStreet.Text == "")
                {
                    MessageBox.Show("Введите улицу !");
                    return;
                }

                if (TbHouse.Text == "")
                {
                    MessageBox.Show("Введите Дом !");
                    return;
                }

                if (TbApartment.Text == "")
                {
                    MessageBox.Show("Введите номер квартиры !");
                    return;
                }


                var idCarInsuranceTariffs = ContextDB.CarInsuranceTariffs
        .Where(i => i.CarBrand.Equals(TempFileVehicleData.Brand) && i.CarModel.Equals(TempFileVehicleData.Model) && i.Generation.Equals(TempFileVehicleData.Generation))
        .Select(i => i.idCarInsuranceTariffs)
        .FirstOrDefault();


                var tariffPrice = ContextDB.CarInsuranceTariffs
        .Where(i => i.idCarInsuranceTariffs == idCarInsuranceTariffs)
        .Select(i => i.Tariff)
        .FirstOrDefault();



                double TB = Convert.ToDouble(tariffPrice);
                double KT = 1.8;
                int KO = 1;
                double KS = 0;
                double KM = 0;
                double KBS = 0;
                double KBM = 1;


                int months = TempFileCalc.Duration+3;
                double monthsKef = TB;
                TB = TB / months;


                switch (months)
                {
                    case 3:
                        monthsKef *= 0.55;
                        break;
                    case 4:
                        monthsKef *= 0.60;
                        break;
                    case 5:
                        monthsKef *= 0.65;
                        break;
                    case 6:
                        monthsKef *= 0.7;
                        break;
                    case 7:
                        monthsKef *= 0.75;
                        break;
                    case 8:
                        monthsKef *= 0.80;
                        break;
                    case 9:
                        monthsKef *= 0.85;
                        break;
                    case 10:
                        monthsKef *= 0.90;
                        break;
                    case 11:
                        monthsKef *= 0.95;
                        break;
                    case 12:
                        monthsKef *= 1;
                        break;
                    default:
                        return ;
                }

                TB = monthsKef;


                switch (TempFileCalc.PeriodOfUse)
                {
                    case 0:
                        KS = 0.5;
                        break;


                    case 1:
                        KS = 0.6;
                        break;


                    case 2:
                        KS = 0.65;
                        break;


                    case 3:
                        KS = 0.7;
                        break;


                    case 4:
                        KS = 0.8;
                        break;


                    case 5:
                        KS = 0.9;
                        break;


                    case 6:
                        KS = 0.95;
                        break;


                    case 7:
                        KS = 1;
                        break;

                    default:
                        break;
                }


                double power = TempFileVehicleData.Power;
                if(power > 0 && power <= 50)
                {
                    KM = 0.6;
                }
                else if(power >= 50 && power <= 70) 
                {
                    KM = 1;
                }
                else if (power >= 70 && power <= 100)
                {
                    KM = 1.1;
                }
                else if (power >= 100 && power <= 120)
                {
                    KM = 1.2;
                }
                else if (power >= 120 && power <= 150)
                {
                    KM = 1.4;
                }
                else if (power > 150)
                {
                    KM = 1.6;
                }


                Driver youngDriver = DriverManager.Drivers.OrderBy(d => d.BirthDate).FirstOrDefault();
                Driver driverExperience = DriverManager.Drivers.OrderBy(d => d.DataDriverLicense - DateTime.Now).FirstOrDefault();
                int ageDriver = 0;
                if (youngDriver != null)
                {
                    ageDriver = CalculateAge(youngDriver.BirthDate);
                }


                // Если молодой водитель был найден, вычисляем его стаж
                TimeSpan experience = TimeSpan.Zero;
                if (youngDriver != null)
                {
                    experience = DateTime.Now - youngDriver.DataDriverLicense;
                }

                // Получаем стаж водителя в годах
                int yearsOfExperience = (int)(experience.TotalDays / 365.25);

                // Получаем стаж водителя в месяцах (остаток от деления на 365.25 дней в году)
                int monthsOfExperience = (int)((experience.TotalDays % 365.25) / 30.4375);



                if (ageDriver < 22 && yearsOfExperience <3)
                {
                    KBS = 1.8;
                }
                else if (ageDriver >22 && yearsOfExperience < 3)
                {
                    KBS = 1.7;
                }
                else if (ageDriver < 22 && yearsOfExperience > 3)
                {
                    KBS = 1.6;
                }
                else if (ageDriver > 22 && yearsOfExperience > 3)
                {
                    KBS = 1;
                }



                TempFileOwner.FirstName = TbFirstName.Text;
                TempFileOwner.LastName = TbLastName.Text;
                TempFileOwner.Patronymic = TbPatronymic.Text;
                TempFileOwner.DatePickerBirthDate = DatePickerBirthDate.SelectedDate.Value;
                TempFileOwner.Gender = CMBGender.SelectedIndex+1;
                TempFileOwner.PassportSeries = TbPassportSeries.Text;
                TempFileOwner.PassportNumber = TbPassportNumber.Text;
                TempFileOwner.Region = TbRegion.Text;
                TempFileOwner.City = TbCity.Text;
                TempFileOwner.Street = TbStreet.Text;
                TempFileOwner.House = TbHouse.Text;
                TempFileOwner.Apartment = TbApartment.Text;


                double T = TB * KT * KO * KS * KM * KBS * KBM ;


                TempFile.InsuranseCost = T.ToString();
                NextPage.Content = "Оформить";
                NavigationService.Navigate(new Policy());
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }
        }

        // Метод для вычисления возраста по дате рождения
        private int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }

        private void CheckInfoOwner_Unchecked(object sender, RoutedEventArgs e)
        {
            // Очищение текстовых полей
            TbFirstName.Text = "";
            TbLastName.Text = "";
            TbPatronymic.Text = "";

            // Сброс выбора в ComboBox
            CMBGender.SelectedIndex = 0;

            // Очищение даты в DatePicker

            DatePickerBirthDate.SelectedDate = DateTime.Now;

            // Очищение данных паспорта
            TbPassportSeries.Text = "";
            TbPassportNumber.Text = "";

            // Очищение адресных данных
            TbRegion.Text = "";
            TbCity.Text = "";
            TbStreet.Text = "";
            TbHouse.Text = "";
            TbApartment.Text = "";

            CheckInfoOwner.Content = "Внести ваши данные из аккаунта ?";
        }

        private void CheckInfoOwner_Checked(object sender, RoutedEventArgs e)
        {
            var client = TempFile.client;


            TbFirstName.Text = client.FirstName.Trim();
            TbLastName.Text = client.LastName.Trim();
            TbPatronymic.Text = client.Patronymic.Trim();
            CMBGender.SelectedIndex = client.IdGender - 1;
            DatePickerBirthDate.SelectedDate = client.BirthDate;
            TbPassportSeries.Text = client.PassportSeries.Trim();
            TbPassportNumber.Text = client.PassportSeries.Trim();
            TbRegion.Text = client.Address.Region.Trim();
            TbCity.Text = client.Address.City.Trim();
            TbStreet.Text = client.Address.Street.Trim();
            TbHouse.Text = client.Address.House.Trim();
            TbApartment.Text = client.Address.Apartment.Trim();
            CheckInfoOwner.Content = "Очистить данные ?";
        }
    }
}
