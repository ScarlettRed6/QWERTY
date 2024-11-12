using CIRCUIT.Utilities;
using CIRCUIT.ViewModel;
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
        public MainWindow()
        {
            //Initialize the windowcontrolservice and set datacontext to mainviewmodel
            InitializeComponent();
            var windowService = new WindowControlService(this);
            var mainViewModel = new MainViewModel(windowService);
            DataContext = mainViewModel;
            
        }

        
    }
}