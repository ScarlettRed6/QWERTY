using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIRCUIT.View.AdminDashboardViews
{
    public partial class AddProductView : UserControl
    {
        public AddProductView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text.Trim());
        }

    }
}
