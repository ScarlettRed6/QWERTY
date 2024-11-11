using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CIRCUIT.Utilities
{
    internal class PassBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PassBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged));

        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BoundPasswordProperty, value);
        }

        public static string GetBoundPassword(DependencyObject dObj)
        {
            return (string)dObj.GetValue(BoundPasswordProperty);
        }
        
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passBox = d as PasswordBox;
            if (passBox != null)
            {
                passBox.PasswordChanged -= PasswordBox_PasswordChanged;
                if (e.NewValue != null)
                {
                    passBox.Password = e.NewValue.ToString();
                }
                passBox.PasswordChanged += PasswordBox_PasswordChanged;

            }
            
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passBox = sender as PasswordBox;
            if(passBox != null)
            {
                SetBoundPassword(passBox, passBox.Password);
            }

        }
    }
}
