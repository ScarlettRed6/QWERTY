using CIRCUIT.Utilities;
using System.ComponentModel;

public class Product : PropertyChange
{
    private string _imageSource;
    private string _productName;
    private decimal _price;

    public string ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChange(nameof(ImageSource));
        }
    }

    public string ProductName
    {
        get => _productName;
        set
        {
            _productName = value;
            OnPropertyChange(nameof(ProductName));
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChange(nameof(Price));
        }
    }
}
