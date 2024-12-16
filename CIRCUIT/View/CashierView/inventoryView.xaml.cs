using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CIRCUIT.View.CashierView
{
    public partial class inventoryView : Window
    {
        public inventoryView()
        {
            InitializeComponent();
        }

        // Button click event handler for "Back"
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

    }
}
