using System;

namespace CIRCUIT.Model
{

    //GET TIME AND DATE
    public class GetTimeandDate
    {
        public string CurrentDateTime => DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
    }
}
