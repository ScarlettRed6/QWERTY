using System.Globalization;
using System.Windows.Data;

namespace CIRCUIT.Utilities
{
    public class StatusToButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = value as string;
            if (status == "Completed")
            {
                return "Completed";  // 
            }
            return "Receive";  // Default content
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusToButtonEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = value as string;
            if (status == "Completed" || status == "Received" || status == "Claimed")
            {
                return false; // Disable button if status is Completed, Received, or Claimed
            }
            return true;  // Enable button
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
