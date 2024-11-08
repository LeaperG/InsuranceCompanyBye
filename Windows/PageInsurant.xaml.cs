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
    /// Логика взаимодействия для PageInsurant.xaml
    /// </summary>
    public partial class PageInsurant : Page
    {
        public PageInsurant()
        {
            InitializeComponent();

            if (TempFile.client == null)
            {
                CheckInfoClient.Visibility = Visibility.Collapsed;
            }
            else
            {
                CheckInfoClient.Visibility = Visibility.Visible;
            }

            TbEmail.Text = TempFileInsurant.Email;
            TbPhone.Text = TempFileInsurant.Phone;
            TbFirstName.Text = TempFileInsurant.FirstName;
            TbLastName.Text = TempFileInsurant.LastName;
            TbPatronymic.Text = TempFileInsurant.Patronymic;
            CMBGender.SelectedIndex = TempFileInsurant.Gender - 1;
            DatePickerBirthDate.SelectedDate = TempFileInsurant.DatePickerBirthDate;
            TbPassportSeries.Text = TempFileInsurant.PassportSeries;
            TbPassportNumber.Text = TempFileInsurant.PassportNumber;
            TbRegion.Text = TempFileInsurant.Region;
            TbCity.Text = TempFileInsurant.City;
            TbStreet.Text = TempFileInsurant.Street;
            TbHouse.Text = TempFileInsurant.House;
            TbApartment.Text = TempFileInsurant.Apartment;




            CMBGender.ItemsSource = ContextDB.Gender.ToList();
            CMBGender.SelectedIndex = 0;
           // DatePickerBirthDate.SelectedDate = DateTime.Now;
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

        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageDrivers());
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (TbEmail.Text == "")
                {
                    MessageBox.Show("Введите Почту");
                    return;
                }

                if (!Regex.IsMatch(TbEmail.Text, @"^[a-zA-Z0-9.+]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    MessageBox.Show("Введите Почту на английском правильно!");
                    return;
                }


                if (TbPhone.Text.Trim() == "")
                {
                    MessageBox.Show("Введите номер телефона");
                    return;
                }

                if (!Regex.IsMatch(TbPhone.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Введите телефон только числами правильно!");
                    TbPhone.Text = "";
                    return;
                }


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


                if (!Regex.IsMatch(TbPassportSeries.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Введите число серию ТС правильно!");
                    TbPassportSeries.Text = "";
                    return;
                }



                if (TbPassportNumber.Text == "")
                {
                    MessageBox.Show("Введите номер Паспорта !");
                    return;
                }

                if (!Regex.IsMatch(TbPassportNumber.Text, "^[0-9]+$"))
                {
                    MessageBox.Show("Введите число серию ТС правильно!");
                    TbPassportNumber.Text = "";
                    return;
                }


                if (TbRegion.Text == "")
                {
                    MessageBox.Show("Введите Ресублику/Край/область !");
                    return;
                }

                if (TbCity.Text == "")
                {
                    MessageBox.Show("Введите Населённый пункт !");
                    return;
                }


                if (TbStreet.Text == "")
                {
                    MessageBox.Show("Введите Улицу !");
                    return;
                }


                if (TbHouse.Text == "")
                {
                    MessageBox.Show("Введите номер Дома !");
                    return;
                }

                if (TbApartment.Text == "")
                {
                    MessageBox.Show("Введите номер Квартиры !");
                    return;
                }



                TempFileInsurant.Email = TbEmail.Text;
                TempFileInsurant.Phone = TbPhone.Text;
                TempFileInsurant.FirstName = TbFirstName.Text;
                TempFileInsurant.LastName = TbLastName.Text;
                TempFileInsurant.Patronymic = TbPatronymic.Text;
                TempFileInsurant.Gender = CMBGender.SelectedIndex + 1;
                TempFileInsurant.DatePickerBirthDate = DatePickerBirthDate.SelectedDate.Value;
                TempFileInsurant.PassportSeries = TbPassportSeries.Text;
                TempFileInsurant.PassportNumber = TbPassportNumber.Text;
                TempFileInsurant.Region = TbRegion.Text;
                TempFileInsurant.City = TbCity.Text;
                TempFileInsurant.Street = TbStreet.Text;
                TempFileInsurant.House = TbHouse.Text;
                TempFileInsurant.Apartment = TbApartment.Text;


                NavigationService.Navigate(new PageOwner());

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }
        }

        private void CheckInfoClient_Unchecked(object sender, RoutedEventArgs e)
        {
            // Очищение текстовых полей
            TbEmail.Text = "";
            TbPhone.Text = "";
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
            CheckInfoClient.Content = "Внести ваши данные из аккаунта ?";
        }

        private void CheckInfoClient_Checked(object sender, RoutedEventArgs e)
        {
            var client = TempFile.client;

            TbEmail.Text = client.UserAccount.Email.Trim();
            TbPhone.Text = client.UserAccount.Phone.Trim();
            TbFirstName.Text = client.FirstName.Trim();
            TbLastName.Text = client.LastName.Trim();
            TbPatronymic.Text = client.Patronymic.Trim();
            CMBGender.SelectedIndex = client.IdGender-1;
            DatePickerBirthDate.SelectedDate = client.BirthDate;
            TbPassportSeries.Text = client.PassportSeries.Trim();
            TbPassportNumber.Text = client.PassportSeries.Trim();
            TbRegion.Text = client.Address.Region.Trim();
            TbCity.Text = client.Address.City.Trim();
            TbStreet.Text = client.Address.Street.Trim();
            TbHouse.Text = client.Address.House.Trim();
            TbApartment.Text = client.Address.Apartment.Trim();

            CheckInfoClient.Content = "Очистить данные ?";
        }
    }
}
