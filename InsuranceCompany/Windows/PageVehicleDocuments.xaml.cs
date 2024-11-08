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

namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PageVehicleDocuments.xaml
    /// </summary>
    public partial class PageVehicleDocuments : Page
    {
        List<string> listTypeDocument = new List<string>()
        {
            "Паспорт ТС",
            "ЭПТС"
        };



        public PageVehicleDocuments()
        {
            InitializeComponent();
            CMBTypeVehicle.ItemsSource = listTypeDocument;
            CMBTypeVehicle.SelectedIndex = 0;
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


            if (textBox.Text == "Дата выдачи: " || textBox.Text.Length < 8)
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

                textBox.Text = "Дата выдачи: " + newText;
            }
        }


        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageOwner());

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {


            //if (DatePickerDriverLicense.SelectedDate == null)
            //{
            //    MessageBox.Show("Введите Дату получения Паспорта ТС !");
            //    return;
            //}

            //if (TbPassportSeries.Text == "")
            //{
            //    MessageBox.Show("Введите серию Паспорта ТС !");
            //    return;
            //}

            //if (TbPassportNumber.Text == "")
            //{
            //    MessageBox.Show("Введите номер Паспорта ТС !");
            //    return;
            //}


            TempFileVehicleDocuments.TypeVehicle = CMBTypeVehicle.SelectedIndex;
            TempFileVehicleDocuments.TypeVehicleText = CMBTypeVehicle.Text;
            TempFileVehicleDocuments.DatePickerDriverLicense = DatePickerDriverLicense.SelectedDate.Value;
            TempFileVehicleDocuments.PassportSeries = TbPassportSeries.Text;
            TempFileVehicleDocuments.PassportNumber = TbPassportNumber.Text;
        }
    }   
}
