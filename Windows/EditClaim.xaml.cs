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
using System.Data.Entity.Infrastructure;


namespace InsuranceCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditClaim.xaml
    /// </summary>
    public partial class EditClaim : Page
    {
        public EditClaim()
        {
            InitializeComponent();

            CMBStatus.ItemsSource = ContextDB.Status.ToList();
            CMBStatus.SelectedIndex = 0;
            DatePickerFiled.SelectedDate = DateTime.Now;

            if (TempFile.user.IdRole == 1 || TempFile.user.IdRole == 3 && TempFile.BoolAddClaim == false)
            {
                StackPanelOne.IsEnabled = true;
                StackPanelTwo.IsEnabled = true;
                CMBStatus.IsEnabled = true;
            }
            else if(TempFile.BoolAddClaim == false)
            {
                StackPanelOne.IsEnabled = false;
                StackPanelTwo.IsEnabled = false;
                SaveEdit.Visibility = Visibility.Collapsed;
                CMBStatus.IsEnabled = false;
            }
            else
            {
                StackPanelOne.IsEnabled = true;
                StackPanelTwo.IsEnabled= true;
                CMBStatus.IsEnabled = false;
            }

            if (TempFile.SelectClaim != null)
            {
                PolicyNumber.IsEnabled = false;
                PolicyNumber.Text = TempFile.SelectClaim.Policies.PolicyNumber;
                Description.Text = TempFile.SelectClaim.Description;
                DatePickerFiled.SelectedDate = TempFile.SelectClaim.DateFiled;
            }
            else
            {
                PolicyNumber.Text = "Номер полиса: " + TempFile.SelectPolicy.PolicyNumber;
            }
        }

        private void TbDatePicker_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Text == "Дата происшествия: " || textBox.Text.Length < 8)
            {
                textBox.Text = "";
            }



            int firstDigitIndex = textBox.Text.IndexOfAny("0123456789".ToCharArray());

            if (firstDigitIndex != -1)
            {
                // Если найдена цифра, обрезаем текст, оставляя только текст после первой цифры
                string newText = textBox.Text.Substring(firstDigitIndex);
                DatePickerFiled.Text = newText;
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

                textBox.Text = "Дата происшествия: " + newText;
            }
        }

        private void SaveEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {



                Claims selectClaim;
                if (TempFile.SelectClaim != null)
                {
                    selectClaim = TempFile.SelectClaim;

                }
                else
                {
                    selectClaim = new Claims();


                    if (TempFile.SelectPolicy != null)
                    {
                        selectClaim.IdPolicy = TempFile.SelectPolicy.IdPolicy;
                    }
                }



                var claims = ContextDB.Claims.ToList();

                var policies = ContextDB.Policies.ToList();
                var selectPolicy = policies.FirstOrDefault(i => i.IdPolicy == selectClaim.IdPolicy);
                TempFile.SelectPolicy = selectPolicy;


                selectClaim.ClaimNumber = TempFile.SelectPolicy.PolicyNumber + "CL" + claims.Count;
                selectClaim.DateFiled = DatePickerFiled.SelectedDate.Value;
                selectClaim.IdStatus = CMBStatus.SelectedIndex + 1;
                selectClaim.Description = Description.Text.Trim();

                if (TempFile.SelectClaim != null)
                {
                    selectClaim.IdPolicy = TempFile.SelectClaim.IdPolicy;
                }
                else
                {
                    try
                    {
                        if (TempFile.client != null)
                        {
                            //var polcies = ContextDB.Policies.ToList();
                            //polcies = polcies.Where(i => i.Clients.IdClient == TempFile.client.IdClient).ToList();

                            //var selectPolicies = polcies.FirstOrDefault(i => i.PolicyNumber == PolicyNumber.Text);
                            //if (selectPolicies != null)
                            //{
                            //    selectClaim.IdPolicy = selectPolicies.IdPolicy;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Нет такого полиса, попробуйте заново", "Ошибка...");
                            //    PolicyNumber.Text = "";
                            //    return;
                            //}

                            selectClaim.IdPolicy = TempFile.SelectPolicy.IdPolicy;
                        }
                        else
                        {
                            //var polcies = ContextDB.Policies.ToList();
                            //var selectPolicies = polcies.FirstOrDefault(i => i.PolicyNumber == PolicyNumber.Text);
                            selectClaim.IdPolicy = TempFile.SelectPolicy.IdPolicy;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Нет такого полиса, попробуйте заново", "Ошибка...");
                        PolicyNumber.Text = "";
                        return;
                        throw;
                    }

                }
         

                //var client = TempFile.client;


                if (TempFile.BoolAddClaim)
                {
                    //selectClaim.Clients.Add(client);
                    //client.Vehicles.Add(selectCar);
                    //ContextDB.VehicleDocuments.Add(vd);
                    ContextDB.Claims.Add(selectClaim);
                }



                ContextDB.SaveChanges();
                NavigationService.Navigate(new ClaimsList());

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
            NavigationService.Navigate(new ClaimsList());
        }
    }
}
