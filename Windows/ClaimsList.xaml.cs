using InsuranceCompany.DB;
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
    /// Логика взаимодействия для Claims.xaml
    /// </summary>
    public partial class ClaimsList : Page
    {

        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По дате (по возрастанию)",
            "По дате (по убыванию)",
            "По номеру заявки (по возрастанию)",
            "По номеру заявки (по убыванию)",
        };



        public ClaimsList()
        {
            InitializeComponent();

            TempFile.BoolAddClaim = false;

            var user = TempFile.user;

            if (user != null)
            {
                if (user.IdRole == 3)
                {
                    
                }

                if (TempFile.user.IdRole == 2)
                {
                    EditClaim.Content = "Узнать подробности";
                }

            }

            CMBFilter.ItemsSource = ContextDB.Status.ToList();
            CMBFilter.SelectedIndex = 0;

            CMBSorting.ItemsSource = listSort;
            CMBSorting.SelectedIndex = 2;

            ListViewUpdate();
        }




        private void ListViewUpdate()
        {
            var claims = ContextDB.Claims.ToList();




            if (TempFile.user != null)
            {

                if (TempFile.user.IdRole == 2)
                {
                    TbHead.Text = "Ваши заявки";

                    claims = claims.Where(i => i.Policies.Clients.IdUser == TempFile.user.IdUser).ToList();
                }
            }
            //Поиск
            claims = claims.Where(i => i.ClaimNumber.ToLower().Contains(TbSearch.Text.ToLower()) || Convert.ToString(i.IdPolicy).Contains(TbSearch.Text)).ToList();


            //Фильтр
            claims = claims.Where(i => i.Status.IdStatus == CMBFilter.SelectedIndex + 1).ToList();

            //Сортировка

            var selectedIndexCmb = CMBSorting.SelectedIndex;

            switch (selectedIndexCmb)
            {
                case 0:
                    if (TempFile.client == null)
                    {
                        claims = ContextDB.Claims.ToList();
                    }
                    break;


                case 1:
                    claims = claims.OrderBy(i => i.DateFiled.ToShortDateString()).ToList();
                    break;


                case 2:
                    claims = claims.OrderByDescending(i => i.DateFiled.ToShortDateString()).ToList();
                    break;

                case 3:
                    claims = claims.OrderBy(i => i.ClaimNumber).ToList();
                    break;


                case 4:
                    claims = claims.OrderByDescending(i => i.ClaimNumber).ToList();
                    break;

                default:
                    break;
            }

            //Промежуток даты

            if (DatePickerClaim.Text != "" && DatePickerClaimBefore.Text != "")
            {
                var DateFrom = Convert.ToDateTime(DatePickerClaim.Text);
                var DateBefore = Convert.ToDateTime(DatePickerClaimBefore.Text);
                claims = claims.Where(i => i.DateFiled >= DateFrom && i.DateFiled <= DateBefore).ToList();
            }
            else if (DatePickerClaim.Text != "" && DatePickerClaimBefore.Text == "")
            {
                var DateFrom = Convert.ToDateTime(DatePickerClaim.Text);
                claims = claims.Where(i => i.DateFiled >= DateFrom).ToList();
            }
            else if (DatePickerClaimBefore.Text != "" && DatePickerClaim.Text == "")
            {
                var DateBefore = Convert.ToDateTime(DatePickerClaimBefore.Text);
                claims = claims.Where(i => i.DateFiled <= DateBefore).ToList();
            }

            LvClaims.ItemsSource = claims;
        }






        private void EditClaim_Click(object sender, RoutedEventArgs e)
        {
            TempFile.SelectClaim = LvClaims.SelectedItem as Claims; // вроде не нужно

            if (LvClaims.SelectedItem == null)
            {
                return;
            }

            NavigationService.Navigate(new EditClaim());
        }

        private void DatePickerClaim_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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
                DatePickerClaim.Text = newText;
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

        private void DatePickerClaimBefore_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
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
                DatePickerClaimBefore.Text = newText;
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

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (TempFile.user != null)
            {

                if (TempFile.user.IdRole == 2)
                {
                    NavigationService.Navigate(new Administrator());
                }
                else if(TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3)
                {
                    NavigationService.Navigate(new ClientList());
                }
                else
                {
                    NavigationService.Navigate(new PageInsuranceOverview());
                }
            }
            else
            {
                NavigationService.Navigate(new PageInsuranceOverview());
            }
        }

        private void BtnResetSort_Click(object sender, RoutedEventArgs e)
        {
            CMBFilter.SelectedIndex = 0;
            CMBSorting.SelectedIndex = 2;
            TbSearch.Text = "";
            DatePickerClaim.SelectedDate = DateTime.Parse("2024/01/01");
            DatePickerClaimBefore.SelectedDate = DateTime.Now;
            ListViewUpdate();
        }

        private void CMBSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }

        private void CMBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewUpdate();
        }
    }
}
