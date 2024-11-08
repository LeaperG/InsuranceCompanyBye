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
using System.Text.RegularExpressions;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для StaffRegistration.xaml
    /// </summary>
    public partial class StaffRegistration : Page
    {
        public StaffRegistration()
        {
            InitializeComponent();

            CMBGender.ItemsSource = ContextDB.Gender.ToList();
            CMBRole.ItemsSource = ContextDB.Role.Where(i => i.IdRole != 1 && i.IdRole != 2).ToList();


            CMBRole.SelectedIndex = 0;
            CMBGender.SelectedIndex = 0;


            var agent = TempFile.SelectAgent;
            if (agent != null)
            {
                BtnSave.Visibility = Visibility.Visible;
                BtnReg.Visibility = Visibility.Collapsed;

                TbEmailBox.Text = agent.UserAccount.Email;
                TbBirthdayBox.Text = agent.BirthDate.ToString();
                TbFirstNameBox.Text = agent.FirstName.ToString();
                TbLastNameBox.Text = agent.LastName.ToString();
                TbPatronymicBox.Text = agent.Patronymic.ToString();
                TbPhoneBox.Text = agent.UserAccount.Phone.ToString();
                TbSalaryBox.Text = agent.Salary.ToString();
                TbLoginBox.Text = agent.UserAccount.LoginName.ToString();
                PassBox.Text = agent.UserAccount.Password;
                CMBGender.SelectedIndex = agent.IdGender-1;

                CMBRole.SelectedIndex = agent.UserAccount.IdRole-1;

                if (agent.UserAccount.IdRole == 3)
                {
                    CMBRole.SelectedIndex = 0;
                }
                else
                {
                    CMBRole.SelectedIndex = 1;
                }
            }
            else
            {
                BtnSave.Visibility = Visibility.Collapsed;
                BtnReg.Visibility = Visibility.Visible;
            }
        }


        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {



                if (TbEmailBox.Text == string.Empty || TbPhoneBox.Text == string.Empty || TbLoginBox.Text == string.Empty || PassBox.Text.ToString() == string.Empty)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (!Regex.IsMatch(TbEmailBox.Text, @"^\w+@\w+\.\w{2,3}$"))
                {
                    MessageBox.Show("Почта не верна!");
                    return;
                }


                if (!Regex.IsMatch(TbPhoneBox.Text, @"^[0-9]{11}$"))
                {
                    MessageBox.Show("Телефон не верен!");
                    return;
                }



                if (PassBox.Text.ToString().Length < 8)
                {
                    MessageBox.Show("Пароль должен быть минимум из 8 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var accounts = ContextDB.UserAccount.ToList();
                var CheckUser = accounts.FirstOrDefault(i => (i.LoginName.ToLower() == TbLoginBox.Text.ToLower()));

                if (CheckUser != null)
                {
                    MessageBox.Show("Такой логин есть", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                CheckUser = accounts.FirstOrDefault(i => (i.Email.ToLower() == TbEmailBox.Text.ToLower()));

                if (CheckUser != null)
                {
                    MessageBox.Show("Такая почта уже есть", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }


                CheckUser = accounts.FirstOrDefault(i => (i.Phone.Contains(TbPhoneBox.Text)));

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
                    LoginName = TbLoginBox.Text.Trim(),
                    Email = TbEmailBox.Text.Trim(),
                    Phone = TbPhoneBox.Text.Trim(),
                    Password = PassBox.Text,
                    PasswordDate = DateTime.Now,
                    PasswordRecovery = str,
                    IdRole = 1

                };

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
            NavigationService.Navigate(new AgentList());
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                var agent = TempFile.SelectAgent;

                agent.UserAccount.Email = TbEmailBox.Text;
                agent.BirthDate = DateTime.Parse(TbBirthdayBox.Text);
                agent.FirstName = TbFirstNameBox.Text;
                agent.LastName = TbLastNameBox.Text;
                agent.Patronymic = TbPatronymicBox.Text;
                agent.UserAccount.Phone = TbPhoneBox.Text;
                agent.Salary = decimal.Parse(TbSalaryBox.Text);
                //agent.MedPolis = TbMedPolisBox.Text;
                agent.UserAccount.LoginName = TbLoginBox.Text;
                agent.UserAccount.Password = PassBox.Text;
                agent.IdGender = CMBGender.SelectedIndex+1;

                if (CMBRole.SelectedIndex == 0)
                {
                    agent.UserAccount.IdRole = 3;
                }
                else
                {
                    agent.UserAccount.IdRole = 4;
                }
                

            
                ContextDB.SaveChanges();

                NavigationService.Navigate(new AgentList());

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }

        }
    }
}
