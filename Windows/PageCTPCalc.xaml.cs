using InsuranceCompany.HellperClass;
using System;
using System.Collections.Generic;
using System.Linq;
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
using InsuranceCompany.HellperClass;


namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для PageCTPCalc.xaml
    /// </summary>
    public partial class PageCTPCalc : Page
    {

        List<string> listDuration = new List<string>()
        {
            "3 месяца",
            "4 месяца",
            "5 месяца",
            "6 месяца",
            "7 месяца",
            "8 месяца",
            "9 месяца",
            "10 месяца",
            "11 месяца",
            "12 месяца",
        };


        List<string> listPeriodOfUse = new List<string>()
        {
            "Владения авто 3 месяца",
            "Владения авто 4 месяца",
            "Владения авто 5 месяца",
            "Владения авто 6 месяца",
            "Владения авто 7 месяца",
            "Владения авто 8 месяца",
            "Владения авто 9 месяца",
            "Владения авто 10 месяца и более",
        };

        List<string> listTypeDocument = new List<string>()
        {
            "Паспорт ТС",
            "ЭПТС"
        };


        public PageCTPCalc()
        {
            InitializeComponent();

                CMBTypeVehicle.ItemsSource = listTypeDocument;
                //CMBTypeVehicle.SelectedIndex = 0;

                CMBDuration.ItemsSource = listDuration;
               // CMBDuration.SelectedIndex = 9;
                CMBPeriodOfUse.ItemsSource = listPeriodOfUse;
                //CMBPeriodOfUse.SelectedIndex = 0;


                DatePickerStart.SelectedDate = DateTime.Now;
                DatePickerDriverLicense.SelectedDate = DateTime.Now;



            if (TempFile.PageCTPCalcBack == 0)
            {
                CMBTypeVehicle.SelectedIndex = 0;
                CMBDuration.SelectedIndex = 9;
                CMBPeriodOfUse.SelectedIndex = 0;
                TempFile.PageCTPCalcBack = 1;

            }
            else
            {
                  DatePickerStart.SelectedDate = TempFileCalc.DatePickerStart;
                  CMBDuration.SelectedIndex = TempFileCalc.Duration;
                  //TbDateEnd.Text = TempFileCalc.DateEnd; // Обратите внимание, что это значение не устанавливается обратно, так как оно было закомментировано
                  CMBPeriodOfUse.SelectedIndex = TempFileCalc.PeriodOfUse;

                  CMBTypeVehicle.SelectedIndex = TempFileCalc.TypeVehicle;
                  CMBTypeVehicle.Text = TempFileCalc.TypeVehicleText;
                  DatePickerDriverLicense.SelectedDate = TempFileCalc.DatePickerDriverLicense;
                  TbPassportSeries.Text = TempFileCalc.PassportSeries;
                  TbPassportNumber.Text = TempFileCalc.PassportNumber;
            }


            if(TempFile.SelectCar != null)
            {
                var car = ContextDB.Vehicles.ToList();
                var selectedCar = car.FirstOrDefault(i => (i.IdVehicles == TempFile.SelectCar.IdVehicles));


                TbPassportSeries.Text = selectedCar.VehicleDocuments.DocumentSeries;
                TbPassportNumber.Text = selectedCar.VehicleDocuments.DocumentNumber;
                DatePickerDriverLicense.SelectedDate = selectedCar.VehicleDocuments.DocumentIssueDate;
            }


        }


        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("месяцев:" + CMBDuration.SelectedIndex + 3);

            if (DatePickerStart.Text == "")
            {
                MessageBox.Show("Введите или выберите начало Страховки !");
                return;
            }


            if (DatePickerDriverLicense.Text == "")
            {
                MessageBox.Show("Введите дату выдачи документа ТС!");
                return;
            }

            if (TbPassportSeries.Text == "")
            {
                MessageBox.Show("Введите серию документа ТС !");
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
                MessageBox.Show("Введите номер документа ТС !");
                return;
            }

            if (!Regex.IsMatch(TbPassportNumber.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Введите число номера ТС правильно!");
                TbPassportNumber.Text = "";
                return;
            }




            TempFileCalc.DatePickerStart = DatePickerStart.SelectedDate.Value;
            TempFileCalc.Duration = CMBDuration.SelectedIndex;
            //TempFileCalc.DateEnd = TbDateEnd.Text;
            TempFileCalc.PeriodOfUse = CMBPeriodOfUse.SelectedIndex;
            TempFileCalc.PeriodOfUseText = CMBPeriodOfUse.SelectedItem.ToString();

            TempFileCalc.TypeVehicle = CMBTypeVehicle.SelectedIndex;
            TempFileCalc.TypeVehicleText = CMBTypeVehicle.Text;
            TempFileCalc.DatePickerDriverLicense = DatePickerDriverLicense.SelectedDate.Value;
            TempFileCalc.PassportSeries = TbPassportSeries.Text;
            TempFileCalc.PassportNumber = TbPassportNumber.Text;


            NavigationService.Navigate(new PageDrivers());

        }


        private void BackPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCTPVehicleData());
        }

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerStart.SelectedDate != null)
            {
                DateTime selectedDate = DatePickerStart.SelectedDate.Value;
                DateTime newDate;

                int selectedIndex = CMBDuration.SelectedIndex + 3;

                if (selectedIndex == 12)
                {
                    // Добавляем год
                    newDate = selectedDate.AddYears(1);
                }
                else
                {
                    // Добавляем год
                    newDate = selectedDate.AddMonths(selectedIndex);
                }
                TempFileCalc.DateEnd = newDate.ToShortDateString();
                TbDateEnd.Text = "Страховой период до: " + newDate.ToShortDateString();
            }
        }


        private void TbDatePicker_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "Страховой период от: " || textBox.Text.Length < 8)
            {
                textBox.Text = "";
            }



            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);
                DatePickerStart.Text = newText;
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

                textBox.Text = "Страховой период от: " + newText;
            }
        }

        private void CMBDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePickerStart_SelectedDateChanged(sender, e);
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

        private void CheckReset_Checked(object sender, RoutedEventArgs e)
        {
            DatePickerStart.SelectedDate = DateTime.Now;
            CMBDuration.SelectedIndex = 0;
            //TbDateEnd.Text = TempFileCalc.DateEnd; // Обратите внимание, что это значение не устанавливается обратно, так как оно было закомментировано
            CMBPeriodOfUse.SelectedIndex = 0;

            CMBTypeVehicle.SelectedIndex = 0;
            DatePickerDriverLicense.SelectedDate = DateTime.Now;
            TbPassportSeries.Text = "";
            TbPassportNumber.Text = "";
        }
    }


}
