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

namespace InsuranceCompany.HellperClass
{
    /// <summary>
    /// Логика взаимодействия для HintedPasswordBox.xaml
    /// </summary>
    public partial class HintedPasswordBox : UserControl
    {
        public HintedPasswordBox()
        {
            InitializeComponent();
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            passwordBox.GotFocus += PasswordBox_GotFocus;
            passwordBox.LostFocus += PasswordBox_LostFocus;
            hintTextBox.GotFocus += HintTextBox_GotFocus;
            hintTextBox.LostFocus += HintTextBox_LostFocus;
            hintTextBox.PreviewMouseLeftButtonDown += HintTextBox_PreviewMouseLeftButtonDown;
            MouseEnter += UserControl_MouseEnter;
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(HintedPasswordBox));

        private void UpdateHintVisibility()
        {
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                hintTextBox.Visibility = Visibility.Visible;
                hintTextBox.IsHitTestVisible = true;
            }
            else
            {
                hintTextBox.Visibility = Visibility.Collapsed;
                hintTextBox.IsHitTestVisible = false;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(UpdateHintVisibility));
        }

        private void HintTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintTextBox.Visibility = Visibility.Collapsed;
            hintTextBox.IsHitTestVisible = false;
            passwordBox.Focus();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateHintVisibility();
        }

        private void HintTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateHintVisibility();
        }

        private void HintTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hintTextBox.Visibility = Visibility.Collapsed;
            hintTextBox.IsHitTestVisible = false;
            passwordBox.Focus();
            e.Handled = true;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintTextBox.Visibility = Visibility.Collapsed;
            hintTextBox.IsHitTestVisible = false;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            hintTextBox.IsHitTestVisible = true;
        }
    }
}
