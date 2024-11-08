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
using System.Text.RegularExpressions;


namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PageDrivers.xaml
    /// </summary>
    public partial class PageDrivers : Page
    {
        DriverManager driverManager = new DriverManager();
        public PageDrivers()
        {
            try
            {

                InitializeComponent();

                CMBGender.ItemsSource  = ContextDB.Gender.ToList();
                CMBGender.SelectedIndex = 0;
                DatePickerDriverLicense.SelectedDate = DateTime.Now;
                DatePickerBirthDate.SelectedDate = DateTime.Now;


                if (TempFile.DriverShow == true)
                {
                    BtnSaveAuth.Visibility = Visibility.Collapsed;
                    BtnNextPage.Visibility = Visibility.Collapsed;
                    StackPanelOne.IsEnabled = false;
                    StackPanelTwo.IsEnabled = false;
                    Grid.SetColumnSpan(BtnBack, 2);
                    //BtnBack.Margin = new Thickness(500, 0, 0, 50);
                    //Grid.SetColumnSpan(BtnBack, 0);
                }

                if (TempFile.DriverShow == true)
                {

                    if (TempFile.user.IdRole == 2)
                    {
                        BtnSaveAuth.Visibility = Visibility.Collapsed;
                        BtnNextPage.Visibility = Visibility.Collapsed;
                    }

                    var drivers = ContextDB.Drivers.ToList();
                    drivers = drivers.Where(i => i.Policies.Contains(TempFile.SelectPolicy)).ToList();


                    for (int i = 0; i < drivers.Count; i++)
                    {
                        drivers[i].Number = i + 1;
                    }
                    LvDrivers.ItemsSource = drivers;
                    //LvDrivers.ItemsSource = ContextDB.Drivers.Where(i => i.Policies == TempFile.SelectPolicy).ToList();
                }

                if (TempFile.PageDriversBack == 0)
                {
                    DriverManager.Drivers = new List<Driver>();
                    TempFile.PageDriversBack = 1;
                }
                else
                {
                    LvDrivers.ItemsSource = DriverManager.Drivers.ToList();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }


        }

        private void DatePickerBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerBirthDate.Text != null)
            {
                DateTime selectedDate = DatePickerBirthDate.SelectedDate.Value;

                // Добавляем год
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
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
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
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);

                textBox.Text = "Дата рождения: " + newText;
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

        private void DatePickerDriverLicense_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "Дата выдачи ВУ: " || textBox.Text.Length < 8)
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

                textBox.Text = "Дата выдачи ВУ: " + newText;
            }
        }

        private void LvDrivers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                TempFile.DriverSelect = LvDrivers.SelectedItem as Driver; // вроде не нужно
                //TempFile.DriverSelect = LvDrivers.SelectedItem as Drivers; // вроде не нужно
                //var driver = LvDrivers.SelectedItem as Drivers;
                if (TempFile.DriverSelect != null)
                {
                    var driver = TempFile.DriverSelect;
                    TbFirstName.Text = driver.FirstName.ToString();
                    TbLastName.Text = driver.LastName.ToString();
                    if (driver.Patronymic != null)
                    {
                        TbPatronymic.Text = driver.Patronymic.ToString();
                    }
                    else
                    {
                        TbPatronymic.Text = "Нету Отчества";
                    }
                    DatePickerBirthDate.Text = driver.BirthDate.ToString();
                    TbDriverLicenseSeries.Text = driver.DriverLicenseSeries.ToString();
                    TbDriverLicenseNumber.Text = driver.DriverLicenseNumber.ToString();
                    DatePickerDriverLicense.Text = driver.DataDriverLicense.ToString();
                    CMBGender.SelectedIndex = driver.IdGender;
                }
                else if(LvDrivers.SelectedItem as Drivers != null)
                {

                    var driver = LvDrivers.SelectedItem as Drivers; ;
                    TbFirstName.Text = driver.FirstName.ToString();
                    TbLastName.Text = driver.LastName.ToString();
                    if (driver.Patronymic != null)
                    {
                        TbPatronymic.Text = driver.Patronymic.ToString();
                    }
                    else
                    {
                        TbPatronymic.Text = "Нету Отчества";
                    }
                    DatePickerBirthDate.Text = driver.BirthDate.ToString();
                    TbDriverLicenseSeries.Text = driver.DriverLicenseSeries.ToString();
                    TbDriverLicenseNumber.Text = driver.DriverLicenseNumber.ToString();
                    DatePickerDriverLicense.Text = driver.DataDriverLicense.ToString();
                    CMBGender.SelectedIndex = driver.IdGender -1;
                }



                this.NavigationService.Refresh();


                BtnSaveAuth.Content = "Изменить";
                TempFile.PageDriversChangeDriver = true;

                if (TempFile.DriverSelect != LvDrivers.SelectedItem)
                {
                    BtnSaveAuth.Content = "Добавить водителя";
                    TempFile.PageDriversChangeDriver = false;
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                TempFile.DriverSelect = LvDrivers.SelectedItem as Driver; // вроде не нужно

                if (LvDrivers.SelectedItem == null)
                {
                        TempFile.PageDriversChangeDriver = false;

                        BtnSaveAuth.Content = "Добавить водителя";
                }
                //else
                //{
                //    TempFile.PageDriversChangeDriver = true;
                //    BtnSaveAuth.Content = "Добавить водителя";
                //}

                if (TempFile.PageDriversChangeDriver == false)
                {

           


                      if (DriverManager.Drivers.Count() >= 5)
                      {
                          MessageBox.Show("Больше водителей добавить нельзя");
                          return;
                      }

                      Driver newDriver = new Driver
                      {
                          LastName = TbFirstName.Text,
                          FirstName = TbLastName.Text,
                          Patronymic = TbPatronymic.Text,
                          BirthDate = DatePickerBirthDate.SelectedDate.Value,
                          DriverLicenseSeries = TbDriverLicenseSeries.Text,
                          DriverLicenseNumber = TbDriverLicenseNumber.Text,
                          DataDriverLicense = DatePickerDriverLicense.SelectedDate.Value,
                          IdGender = CMBGender.SelectedIndex
                      };


                      DriverManager.Drivers.Add(newDriver);
                      DriverNumberUpdate();
                }
                else
                {
                    TempFile.DriverSelect.LastName = TbFirstName.Text;
                    TempFile.DriverSelect.FirstName = TbLastName.Text;
                    TempFile.DriverSelect.Patronymic = TbPatronymic.Text;
                    TempFile.DriverSelect.BirthDate = DatePickerBirthDate.SelectedDate.Value;
                    TempFile.DriverSelect.DriverLicenseSeries = TbDriverLicenseSeries.Text;
                    TempFile.DriverSelect.DriverLicenseNumber = TbDriverLicenseNumber.Text;
                    TempFile.DriverSelect.DataDriverLicense = DatePickerDriverLicense.SelectedDate.Value;
                    TempFile.DriverSelect.IdGender = CMBGender.SelectedIndex;

                    TempFile.PageDriversChangeDriver = false;
                    LvDrivers.SelectedItem = null;
                    BtnSaveAuth.Content = "Добавить водителя";
                }

                LvDrivers.ItemsSource = DriverManager.Drivers.ToList();


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
            if (TempFile.DriverShow == false)
            {
                NavigationService.Navigate(new PageCTPCalc());
            }


            if (TempFile.DriverShow == true)
            {
                TempFile.DriverShow = false;
                NavigationService.Navigate(new Policy());
            }

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            //TempFileDrivers.FirstName = TbFirstName.Text;
            //TempFileDrivers.LastName = TbLastName.Text;
            //TempFileDrivers.Patronymic = TbPatronymic.Text;
            //TempFileDrivers.DatePickerBirthDate = DatePickerBirthDate.SelectedDate.Value;
            //TempFileDrivers.LicenseSeries = TbDriverLicenseSeries.Text;
            //TempFileDrivers.LicenseNumber = TbDriverLicenseNumber.Text;
            //TempFileDrivers.Gender = CMBGender.SelectedIndex;
            //TempFileDrivers.DatePickerDriverLicense = DatePickerDriverLicense.SelectedDate.Value;
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

                if (TbDriverLicenseSeries.Text == "")
                {
                    MessageBox.Show("Введите серию ВУ !");
                    return;
                }

                if (!Regex.IsMatch(TbDriverLicenseSeries.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Введите число серию ВУ правильно!");
                    TbDriverLicenseSeries.Text = "";
                    return;
                }



                if (TbDriverLicenseNumber.Text == "")
                {
                    MessageBox.Show("Введите номер ВУ !");
                    return;
                }

                if (!Regex.IsMatch(TbDriverLicenseNumber.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Введите число номер ВУ правильно!");
                    TbDriverLicenseNumber.Text = "";
                    return;
                }

                //if (DatePickerDriverLicense.SelectedDate == null)
                //{
                //    MessageBox.Show("Введите номер квартиры !");
                //    return;
                //}
                PriceCalculation();


            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }

        }

        private void BtnDell_Click(object sender, RoutedEventArgs e)
        {
            if (TempFile.DriverShow == true)
            {
                return;
            }


            MessageBoxResult result = MessageBox.Show("Удалить Водителя ?", "Вопрос",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                if (button == null)
                {
                    return;
                }

                var driverNum = button.DataContext as Driver;
                var  drivers = DriverManager.Drivers.ToList();
                var selectDriver = drivers.FirstOrDefault(i => i.Number == driverNum.Number);
                DriverManager.Drivers.Remove(selectDriver);
                DriverNumberUpdate();
                LvDrivers.ItemsSource = DriverManager.Drivers.ToList();
            }
        }



        private void DriverNumberUpdate()
        {
            var a = DriverManager.Drivers.ToList();

            for (int i = 0; i < a.Count; i++)
            {
                a[i].Number = i + 1;
            }
        }

        private void CheckReset_Checked(object sender, RoutedEventArgs e)
        {
            TbFirstName.Text = "";
            TbLastName.Text = "";
            TbPatronymic.Text = "";
            TbPatronymic.Text = "";
            DatePickerBirthDate.SelectedDate = DateTime.Now;
            TbDriverLicenseSeries.Text = "";
            TbDriverLicenseNumber.Text = "";
            DatePickerDriverLicense.SelectedDate = DateTime.Now;

            LvDrivers.SelectedItem = null;
            CheckReset.IsChecked = false;
        }


        private void PriceCalculation()
        {
            var idCarInsuranceTariffs = ContextDB.CarInsuranceTariffs
.Where(i => i.CarBrand.Equals(TempFileVehicleData.Brand) && i.CarModel.Equals(TempFileVehicleData.Model) && i.Generation.Equals(TempFileVehicleData.Generation))
.Select(i => i.idCarInsuranceTariffs)
.FirstOrDefault();


            var tariffPrice = ContextDB.CarInsuranceTariffs
    .Where(i => i.idCarInsuranceTariffs == idCarInsuranceTariffs)
    .Select(i => i.Tariff)
    .FirstOrDefault();



            double TB = Convert.ToDouble(tariffPrice);
            //double TB = 4900;
            // MessageBox.Show("Базовый тариф " + TB);
            double KT = 1.8;
            int KO = 1;
            double KS = 0;
            double KM = 0;
            double KBS = 0;
            //double KMBS = 0;
            double KBM = 1;


            int months = TempFileCalc.Duration + 3;
            double monthsKef = TB;
            TB = TB / months;


            switch (months)
            {
                case 3:
                    monthsKef *= 0.55;
                    break;// Примерная цена для 3 месяцев
                case 4:
                    monthsKef *= 0.60;
                    break;// Примерная цена для 4 месяцев
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
                    break;// Примерная цена для 12 месяцев
                default:
                    return; // Если не указана цена для данного количества месяцев
            }

            TB = monthsKef;
            // MessageBox.Show(TB+"Цена Базового тарифа");




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
            if (power > 0 && power <= 50)
            {
                KM = 0.6;
            }
            else if (power >= 50 && power <= 70)
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

            // Выводим стаж водителя
            //MessageBox.Show($"Стаж водителя: {yearsOfExperience} лет и {monthsOfExperience} месяцев", "Информация о стаже водителя", MessageBoxButton.OK, MessageBoxImage.Information);




            if (ageDriver < 22 && yearsOfExperience < 3)
            {
                KBS = 1.8;
            }
            else if (ageDriver > 22 && yearsOfExperience < 3)
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


            double T = TB * KT * KO * KS * KM * KBS * KBM;

            //TbPrice.Text = "Цена страховки: " + T.ToString();

            TempFile.InsuranseCost = T.ToString();

            //NextPage.Content = "Оформить";

            //if (TempFile.PageOwnerBack == 0)
            //{
            //    TempFile.PageOwnerBack = 1;
            //    return;
            //}



            MessageBox.Show("Цена страховки: " + TempFile.InsuranseCost + " рублей", "Стоимость...", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBoxResult result = MessageBox.Show("Продолжить оформление ?", "Вопрос...",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if(TempFile.user != null)
                {
                    NavigationService.Navigate(new PageInsurant());
                }
                else
                {
                    TempFile.NewUser = true;
                    NavigationService.Navigate(new Registration());
                }
            }
            else { return; }

        }


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
    }
}
