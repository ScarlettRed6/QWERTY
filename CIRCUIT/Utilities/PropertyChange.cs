using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CIRCUIT.Utilities
{
    public class PropertyChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

    }
}
