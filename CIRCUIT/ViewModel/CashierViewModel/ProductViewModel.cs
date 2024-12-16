
//GETIING PRODUCTS
using CIRCUIT.Utilities;
using System.ComponentModel;
using System.Security.Cryptography;

public class Product : PropertyChange
{
    private int _ProductId;
    private string _imageSource;
    private string _productName;
    private string _description;
    private decimal _price;


    public int ProductId
    {
        get => _ProductId;
        set
        {
            _ProductId = value;
            OnPropertyChange(nameof(ProductId));
        }
    }

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

    public string Description
    {
        get { return _description; }
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChange(nameof(Description));
            }
        }
    }


}
