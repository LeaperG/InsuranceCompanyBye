using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InsuranceCompany.HellperClass
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty HintProperty = DependencyProperty.RegisterAttached(
            "Hint",
            typeof(string),
            typeof(PasswordBoxHelper),
            new FrameworkPropertyMetadata(null)
        );

        public static void SetHint(DependencyObject element, string value)
        {
            element.SetValue(HintProperty, value);
        }

        public static string GetHint(DependencyObject element)
        {
            return (string)element.GetValue(HintProperty);
        }


    }
}
