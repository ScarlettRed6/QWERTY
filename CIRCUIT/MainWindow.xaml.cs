using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIRCUIT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int num = 0;
        public MainWindow()
        {
            InitializeComponent();
            Something();
        }

        private void Something()
        {
            num = 1203;
            string develop = "something";
            develop = "DevelopNew";
        }
    }
}