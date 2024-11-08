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
    /// Логика взаимодействия для AgentList.xaml
    /// </summary>
    public partial class AgentList : Page
    {
        public AgentList()
        {
            InitializeComponent();

            ListViewUpdate();
        }


        private void ListViewUpdate()
        {
            var agents = ContextDB.Agents.ToList();
            agents = agents.Where(i => i.FirstName.ToLower().Contains(TbSearch.Text.ToLower()) 
            || i.LastName.ToLower().Contains(TbSearch.Text.ToLower())
            || Convert.ToString(i.IdAgent).Contains(TbSearch.Text)).ToList();
            LvAgents.ItemsSource = agents;
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Administrator());
        }

        private void EditAgent_Click(object sender, RoutedEventArgs e)
        {
            TempFile.SelectAgent = LvAgents.SelectedItem as Agents; // вроде не нужно

            if (LvAgents.SelectedItem == null)
            {
                return;
            }

            NavigationService.Navigate(new StaffRegistration());
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void BtnAddAgent_Click(object sender, RoutedEventArgs e)
        {
            LvAgents.SelectedItem = null;
            TempFile.SelectAgent = null;
            NavigationService.Navigate(new StaffRegistration());
        }
    }
}
