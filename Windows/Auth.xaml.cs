using InsuranceCompany.HellperClass;
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

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }


        private void SaveAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var hintedPasswordBox = Pbox;
                var password = hintedPasswordBox.passwordBox;
                password.BorderBrush = new SolidColorBrush(Colors.Blue);
                string login = LogBox.Text;

                if (login == string.Empty || password.Password == string.Empty)
                {
                    MessageBox.Show("Введите данные!");
                    return;
                }

                var accounts = ContextDB.UserAccount.ToList();
                var user = accounts.FirstOrDefault(i => (i.LoginName == login));

                if (user == null)
                {
                    MessageBox.Show("Логин не правильный, попробуйте снова...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                TempFile.user = user;

                var client = ContextDB.Clients.ToList();
                var selectClient = client.FirstOrDefault(i => (i.IdUser == user.IdUser));

                var agent = ContextDB.Agents.ToList();
                var selectAgent = agent.FirstOrDefault(i => (i.IdUser == user.IdUser));
                TempFile.agent = selectAgent;

                //if (selectClient == null)
                //{
                //    MessageBox.Show("Пользоватль не найден!");
                //    return;
                //}
                TempFile.client = selectClient;

                if (user == null)   
                {
                    MessageBox.Show("Такого пользователя с логином не существует!");
                    return;
                }

                if (user.IdRole == 4)
                {
                    MessageBox.Show("Упс вы забанены, Нам НЕ жаль:)!", "Вход", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }



                if (password.Password == user.Password)
                {
                    //MessageBox.Show("Вход успешно выполнен!", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                    TempFile.Auth = true;
                    if (TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3)
                    {
                        NavigationService.Navigate(new Administrator());
                    }
                    else
                    {
                        NavigationService.Navigate(new PageInsuranceOverview());
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Произошла непредвиденная ошибка, попроубйте позже или перезапустите приложение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                throw;
            }

        }


        private void RegHyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }


        private void ForgotPasswordHyperlink_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageInsuranceOverview());
        }
    }
}
