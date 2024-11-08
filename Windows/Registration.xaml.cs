using InsuranceCompany.DB;
using InsuranceCompany.HellperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
using static InsuranceCompany.HellperClass.PasswordBoxHelper;
using static InsuranceCompany.HellperClass.HintedPasswordBox;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {

            try
            {



                if (EmailBox.Text == string.Empty || PhoneBox.Text == string.Empty || LoginBox.Text == string.Empty || PassBox.passwordBox.Password.ToString() == string.Empty || VerPassBox.passwordBox.Password.ToString() == string.Empty)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (!Regex.IsMatch(EmailBox.Text, @"^\w+@\w+\.\w{2,3}$"))
                {
                    MessageBox.Show("Почта не верна!");
                    return;
                }


                if (!Regex.IsMatch(PhoneBox.Text, @"^[0-9]{11}$"))
                {
                    MessageBox.Show("Телефон не верен!");
                    return;
                }


                if (PassBox.passwordBox.Password.ToString() != VerPassBox.passwordBox.Password.ToString())
                {
                    MessageBox.Show("Пароли не совподают!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }


                if (PassBox.passwordBox.Password.ToString().Length < 8 && VerPassBox.passwordBox.Password.ToString().Length < 8)
                {
                    MessageBox.Show("Пароль должен быть минимум из 8 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var accounts = ContextDB.UserAccount.ToList();
                var CheckUser = accounts.FirstOrDefault(i => (i.LoginName.ToLower() == LoginBox.Text.ToLower()));

                if (CheckUser != null)
                {
                    MessageBox.Show("Такой логин есть", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                CheckUser = accounts.FirstOrDefault(i => (i.Email.ToLower() == EmailBox.Text.ToLower()));

                if (CheckUser != null)
                {
                    MessageBox.Show("Такая почта уже есть", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }


                CheckUser = accounts.FirstOrDefault(i => (i.Phone.Contains(PhoneBox.Text)));

                if (CheckUser != null)
                {
                    MessageBox.Show("Такой номер телефона уже есть", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }




                // Рандомный Пароль для Востановления
                Random rnd = new Random();
                rnd.Next(1000, 9999);
                string str = "";
                const string symbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";


                for (int i = 0; i < 4; i++)
                {
                    str += symbols[rnd.Next(0, 62)];
                }






                UserAccount user = new UserAccount()
                {
                    LoginName = LoginBox.Text.Trim(),
                    Email = EmailBox.Text.Trim(),
                    Phone = PhoneBox.Text.Trim(),
                    //Проверка
                    //Password = PassBox.Text ,
                    Password = PassBox.passwordBox.Password,
                    PasswordDate = DateTime.Now,
                    PasswordRecovery = str,
                    IdRole = 2
                };


                Address address = new Address()
                {
                    Region = "",
                    City = "",
                    Street = "",
                    House = "",
                    Apartment = "",
                };




                Clients client = new Clients()
                {
                    IdUser = user.IdUser,
                    FirstName = "",
                    LastName = "",
                    Patronymic = "",
                    BirthDate = DateTime.Now,
                    IdAddress = address.IdAddress,
                    IdGender = 1,
                    PassportNumber = "",
                    PassportSeries = "",
                };

                TempFile.Skip = true;
                TempFile.user = user;
                TempFile.client = client;
                TempFile.SelectClientAddress = address;

                ContextDB.Address.Add(address);
                ContextDB.Clients.Add(client);
                ContextDB.UserAccount.Add(user);
                ContextDB.SaveChanges();
                MessageBox.Show("Успешно сохранено", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                if (TempFile.NewUser != true && TempFile.Reg != true)
                {
                    NavigationService.Navigate(new PageInsurant());
                }
                else
                {
                    TempFile.Reg = false;
                    NavigationService.Navigate(new Personal());
                }

                }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }

        }

        private void BackAuth_Click(object sender, RoutedEventArgs e)
        {
            TempFile.NewUser = false;
            NavigationService.Navigate(new PageInsuranceOverview());
        }
    }
}
