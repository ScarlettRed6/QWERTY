using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CIRCUIT.Utilities
{
    class TextBoxPHHelper
    {
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached("PlaceholderText", typeof(string), typeof(TextBoxPHHelper), new PropertyMetadata(string.Empty, OnPlaceholderTextChange));

        public TextBoxPHHelper()
        {
            
        }

        public static string GetPlaceholdertText(TextBox txtBox)
        {
            return (string)txtBox.GetValue(PlaceholderTextProperty);
        }

        public static void SetPlaceholderText(TextBox textBox, string value)
        {
            textBox.SetValue(PlaceholderTextProperty, value);
        }


        private static void OnPlaceholderTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.GotFocus += RemovePlaceholder;
                textBox.LostFocus += ShowPlaceholder;

                textBox.Loaded += (sender, args) =>
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        ShowPlaceholder(textBox, null);
                    }
                };

            }
        }

        private static void ShowPlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetPlaceholdertText(textBox);
                textBox.Foreground = Brushes.Gray;
            }

        }

        private static void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox != null && textBox.Text == GetPlaceholdertText(textBox))
            {
                textBox.Text = string.Empty;
            }

        }
    }
}
