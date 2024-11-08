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
using System.Net;
using System.Security.Principal;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для Personal.xaml
    /// </summary>
    public partial class Personal : Page
    {
        public Personal()
        {
            InitializeComponent();

            if (TempFile.Skip == true)
            {
                BtnSkip.Visibility = Visibility.Visible;
            }
            else
            {
                BtnSkip.Visibility = Visibility.Collapsed;
            }

            CMBGender.ItemsSource = ContextDB.Gender.ToList();
            CMBGender.SelectedIndex = 0;
            CMBRole.ItemsSource = ContextDB.Role.Where(i => i.IdRole == 2 || i.IdRole == 4).ToList();
            CMBRole.SelectedIndex = 0;

            if (TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3)
            {
                CMBGender.Width = 200;
                CMBRole.Width = 200;
            }
            else
            {
                CMBRole.Visibility = Visibility.Collapsed;
            }


            if (TempFile.SelectClient == null && TempFile.client == null)
            {
                BtnSave.Content = "Зарегистрировать";
            }


            if (TempFile.client != null || TempFile.SelectClient != null)
            {
                var client = TempFile.client;

                TbEmailBox.IsEnabled = false;
                TbPhoneBox.IsEnabled = false;
                TbLoginBox.IsEnabled = false;
                PassBox.IsEnabled = false;

                
                if (TempFile.SelectClient != null)
                {
                    client = TempFile.SelectClient;
                    
                    TbEmailBox.IsEnabled = true;
                    TbPhoneBox.IsEnabled = true;
                    TbLoginBox.IsEnabled = true;
                    PassBox.IsEnabled = true;
                }

                TbFirstName.Text = client.FirstName;
                TbLastName.Text = client.LastName;
                TbPatronymic.Text = client.Patronymic;
                DatePickerBirthDate.SelectedDate = client.BirthDate;
                CMBGender.SelectedIndex = client.IdGender-1;

                if (client.UserAccount.IdRole == 2)
                {
                    CMBRole.SelectedIndex = 0;
                }
                else
                {
                    CMBRole.SelectedIndex = 1;
                }
                
                
                TbPassportSeries.Text = client.PassportSeries;
                TbPassportNumber.Text = client.PassportNumber;
                TbRegion.Text = client.Address.Region;
                TbCity.Text = client.Address.City;
                TbStreet.Text = client.Address.Street;
                TbHouse.Text = client.Address.House;
                TbApartment.Text = client.Address.Apartment;
                TbEmailBox.Text = client.UserAccount.Email;
                TbPhoneBox.Text = client.UserAccount.Phone;
                TbLoginBox.Text = client.UserAccount.LoginName;
                PassBox.Text = client.UserAccount.Password;
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

        private void DatePickerBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerBirthDate.Text != null)
            {
                DateTime selectedDate = DatePickerBirthDate.SelectedDate.Value;
                DateTime newDate = selectedDate.AddYears(1);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            TempFile.NewUser = false;

            if (TempFile.SelectClient != null || TempFile.SelectClient == null && TempFile.client == null)
            {
                NavigationService.Navigate(new ClientList());
            }
            else
            {
                NavigationService.Navigate(new PersonalAccount());
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (TempFile.client != null || TempFile.SelectClient != null || TempFile.SelectClient == null && TempFile.client == null)
                {
                    UserAccount account = null;


                    var client = TempFile.client;

                    if (client != null)
                    {
                        client = TempFile.client;
                        account = TempFile.user;
                    }
                    else if (TempFile.SelectClient != null)
                    {
                        client = TempFile.SelectClient;
                        account = TempFile.SelectClient.UserAccount;
                    }
                    else
                    {
                        client = new Clients();
                    }


                    client.FirstName = TbFirstName.Text;
                    client.LastName = TbLastName.Text;
                    client.Patronymic = TbPatronymic.Text;
                    client.BirthDate = DatePickerBirthDate.SelectedDate.Value;
                    client.IdGender = CMBGender.SelectedIndex+1;
                    client.PassportSeries = TbPassportSeries.Text;
                    client.PassportNumber = TbPassportNumber.Text;


                    var address = TempFile.SelectClientAddress;

                    if (TempFile.SelectClient != null)
                    {
                        address = TempFile.SelectClient.Address;
                    }
                    else if(TempFile.client != null)
                    {
                        address = client.Address;
                    }
                    else
                    {
                        address = new Address();
                    }

                    address.Region = TbRegion.Text;
                    address.City = TbCity.Text;
                    address.Street = TbStreet.Text;
                    address.House = TbHouse.Text;
                    address.Apartment = TbApartment.Text;


                    // Рандомный Пароль для Востановления
                    Random rnd = new Random();
                    rnd.Next(1000, 9999);
                    string str = "";
                    const string symbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";


                    for (int i = 0; i < 4; i++)
                    {
                        str += symbols[rnd.Next(0, 62)];
                    }



                    if (TempFile.user != null)
                    {
                       // account = TempFile.user;
                    }
                    else
                    {
                        account = new UserAccount();
                    }

                    account.Email = TbEmailBox.Text;
                    account.Phone = TbPhoneBox.Text;
                    account.LoginName = TbLoginBox.Text;
                    account.Password =PassBox.Text;
                    account.PasswordDate = DateTime.Now;
                    account.PasswordRecovery = str;
                    account.IdRole = 2;

                    if (CMBRole.SelectedIndex == 0)
                    {
                        account.IdRole = 2;
                    }
                    else if(CMBRole.SelectedIndex == 1)
                    {
                        account.IdRole = 4;
                    }
                    else
                    {
                        account.IdRole = 2;
                    }

                    if (TempFile.SelectClient == null && TempFile.client == null)
                    {
                        ContextDB.Address.Add(address);
                        ContextDB.Clients.Add(client);
                        ContextDB.UserAccount.Add(account);
                    }

                    if (TempFile.user.IdRole != 1 && TempFile.user.IdRole != 3)
                    {
                        TempFile.user = account;
                        TempFile.Auth = true;
                    }
                }


                ContextDB.SaveChanges();
                TempFile.Skip = false;

                if (TempFile.NewUser != true && TempFile.Personal != true && TempFile.user.IdRole != 1 && TempFile.user.IdRole != 3)
                {
                    NavigationService.Navigate(new PageInsuranceOverview());
                }
                else if (TempFile.Personal == true)
                {
                    TempFile.Personal = false;
                    NavigationService.Navigate(new PersonalAccount());
                }
                else if(TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3)
                {
                    NavigationService.Navigate(new ClientList());
                }
                else
                {
                    NavigationService.Navigate(new PageInsurant());
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            TempFile.Auth = true;
            TempFile.Skip = false;
            NavigationService.Navigate(new PageInsuranceOverview());
        }
    }
}
