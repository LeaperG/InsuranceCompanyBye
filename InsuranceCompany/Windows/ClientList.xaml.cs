using InsuranceCompany.DB;
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
using InsuranceCompany.DB;


namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Page
    {
        public ClientList()
        {
            InitializeComponent();
            CMBRole.ItemsSource = ContextDB.Role.Where(i => i.IdRole == 2 || i.IdRole == 4).ToList();
            CMBRole.SelectedIndex = 0;
            ListViewUpdate();
        }


        private void ListViewUpdate()
        {
            var clients = ContextDB.Clients.ToList();
            clients = clients.Where(i => i.FirstName.ToLower().Contains(TbSearch.Text.ToLower())
            || i.LastName.ToLower().Contains(TbSearch.Text.ToLower())
            || Convert.ToString(i.IdClient).Contains(TbSearch.Text)).ToList();


            //Фильтр

            if(CMBRole.SelectedIndex == 0)
            {
                clients = clients.Where(i => i.UserAccount.Role.IdRole == 2).ToList();
            }
            else
            {
                clients = clients.Where(i => i.UserAccount.Role.IdRole == 4).ToList();
            }

            LvClients.ItemsSource = clients;
        }



        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            TempFile.SelectClient = LvClients.SelectedItem as Clients;

            if (LvClients.SelectedItem == null)
            {
                return;
            }

            NavigationService.Navigate(new Personal());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Administrator());
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            LvClients.SelectedItem = null;
            TempFile.SelectClient = null;
            NavigationService.Navigate(new Personal());
        }

        private void CMBRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewUpdate();
        }

        private void BtnClientsCars_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalAccount());
        }

        private void BtnClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClaimsList());
        }
    }
}
