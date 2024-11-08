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
using System.Security.Cryptography;

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Page
    {


        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По дате начала (по возрастанию)",
            "По дате начала (по убыванию)",
            "По дате завершения (по возрастанию)",
            "По дате завершения (по убыванию)",
            "По номеру полиса (по возрастанию)",
            "По номеру полиса (по убыванию)",
            "По цене (по возрастанию)",
            "По цене (по убыванию)",
        };




        public Administrator()
        {
            InitializeComponent();
            BtnClaims.Visibility = Visibility.Collapsed;
            Back.Visibility = Visibility.Collapsed;
            var user = TempFile.user;
            if(user.IdRole == 3)
            {
                TbHead.Text = "РАБота";
                BtnAgents.Visibility = Visibility.Collapsed;
            }

            if (TempFile.user.IdRole == 2)
            {
                EditPolicy.Content = "Узнать подробности";
                BtnClaims.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
                BtnSignOut.Visibility = Visibility.Collapsed;
            }

            CMBFilter.ItemsSource = ContextDB.Status.ToList();
            CMBFilter.SelectedIndex = 0;

            CMBSorting.ItemsSource = listSort;
            CMBSorting.SelectedIndex = 6;

                ListViewUpdate();
        }

        private void ListViewUpdate()
        {
            var policies = ContextDB.Policies.ToList();





            if (TempFile.user.IdRole == 2)
            {
                BtnAgents.Visibility = Visibility.Collapsed;
                BtnClients.Visibility = Visibility.Collapsed;
                TbHead.Text = "Ваши полисы";

                policies = TempFile.client.Policies.ToList();
            }
            
            //Поиск
            policies = policies.Where(i => i.PolicyNumber.ToLower().Contains(TbSearch.Text.ToLower()) || Convert.ToString(i.IdClient).Contains(TbSearch.Text) || Convert.ToString(i.IdOwner).Contains(TbSearch.Text)).ToList();


            //Фильтр
            policies = policies.Where(i => i.Status.IdStatus == CMBFilter.SelectedIndex + 1).ToList();

            //Сортировка

            var selectedIndexCmb = CMBSorting.SelectedIndex;

            switch (selectedIndexCmb)
            {
                case 0:
                    policies = ContextDB.Policies.ToList();
                    break;


                case 1:
                    policies = policies.OrderBy(i => i.StartDate.ToShortDateString()).ToList();
                    break;


                case 2:
                    policies = policies.OrderByDescending(i => i.StartDate.ToShortDateString()).ToList();
                    break;


                case 3:
                    policies = policies.OrderBy(i => i.EndDate).ToList();
                    break;


                case 4:
                    policies = policies.OrderByDescending(i => i.EndDate).ToList();
                    break;


                case 5:
                    policies = policies.OrderBy(i => i.PolicyNumber).ToList();
                    break;


                case 6:
                    policies = policies.OrderByDescending(i => i.PolicyNumber).ToList();
                    break;


                case 7:
                    //products = products.orderby(i =>convert.toint32(i.cost)).tolist();
                    policies = policies.OrderBy(i => i.CoverageAmount).ToList();
                    break;


                case 8:
                    policies = policies.OrderByDescending(i => i.CoverageAmount).ToList();
                    break;

                default:
                    break;
            }

            //Промежуток даты

            if (DatePickerPolicesFrom.Text != "" && DatePickerPolicesBefore.Text != "")
            {
                var DateFrom = Convert.ToDateTime(DatePickerPolicesFrom.Text);
                var DateBefore = Convert.ToDateTime(DatePickerPolicesBefore.Text);
                policies = policies.Where(i => i.StartDate >= DateFrom && i.StartDate <= DateBefore).ToList();
            }
            else if(DatePickerPolicesFrom.Text != "" && DatePickerPolicesBefore.Text == "")
            {
                var DateFrom = Convert.ToDateTime(DatePickerPolicesFrom.Text);
                policies = policies.Where(i => i.StartDate >= DateFrom).ToList();
            }
            else if(DatePickerPolicesBefore.Text != "" && DatePickerPolicesFrom.Text == "" )
            {
                var DateBefore = Convert.ToDateTime(DatePickerPolicesBefore.Text);
                policies = policies.Where(i => i.StartDate <= DateBefore).ToList();
            }

            LvPolicies.ItemsSource = policies;
        }


        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (TempFile.user.IdRole == 2)
            {
                NavigationService.Navigate(new PersonalAccount());
            }
            else
            {
                NavigationService.Navigate(new PageInsuranceOverview());
            }
        }

        private void BtnAgents_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AgentList());
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientList());
        }

        private void EditPolicy_Click(object sender, RoutedEventArgs e)
        {
            TempFile.SelectPolicy = LvPolicies.SelectedItem as Policies; // вроде не нужно

            if (LvPolicies.SelectedItem == null)
            {
                return;
            }

            NavigationService.Navigate(new Policy());
            //LvPolicies.ItemsSource = ContextDB.Clients.ToList();
        }

        private void CMBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void CMBSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }


        private void TbDatePicker_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "С: " || textBox.Text.Length < 8)
            {
                textBox.Text = "";
            }



            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);
                DatePickerPolicesFrom.Text = newText;
                textBox.Text = newText;
            }
        }

        private void TbDatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);

                textBox.Text = "С: " + newText;
            }
        }

        private void TbDatePicker_GotFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "По: " || textBox.Text.Length < 8)
            {
                textBox.Text = "";
            }



            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);
                DatePickerPolicesBefore.Text = newText;
                textBox.Text = newText;
            }
        }

        private void TbDatePicker_LostFocus_1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);

                textBox.Text = "По: " + newText;
            }
        }

        private void DatePickerPolicesFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void DatePickerPolicesBefore_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void BtnResetSort_Click(object sender, RoutedEventArgs e)
        {
            CMBFilter.SelectedIndex = 0;
            CMBSorting.SelectedIndex = 2;
            TbSearch.Text = "";
            DatePickerPolicesFrom.SelectedDate = DateTime.Parse("2024/01/01");
            DatePickerPolicesBefore.SelectedDate = DateTime.Now.AddYears(1);
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            TempFile.Reset();
            TempFile.client = null;
            TempFileVehicleData.Reset();
            TempFileCalc.Reset();
            DriverManager.Reset();
            TempFileInsurant.Reset();
            TempFileOwner.Reset();

            TempFile.Auth = false;
            TempFile.user = null;
            NavigationService.Navigate(new PageInsuranceOverview());
        }

        private void BtnClaims_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClaimsList());
        }

        private void BtnClaim_Click(object sender, RoutedEventArgs e)
        {
            TempFile.claimPoliciesContinue = true;
            MessageBoxResult result = MessageBox.Show("Подать заявку на страховой случай ?", "Вопрос",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                if (button == null)
                {
                    return;
                }

                var policyNum = button.DataContext as Policies;
                var policies = ContextDB.Policies.ToList();
                var selectPolicy = policies.FirstOrDefault(i => i.IdVehicles == policyNum.IdVehicles);

                TempFile.SelectPolicy = selectPolicy;
                TempFile.BoolAddClaim = true;
                NavigationService.Navigate(new EditClaim());
            }
        }
    }
}
