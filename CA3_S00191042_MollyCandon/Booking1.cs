using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_S00191042_MollyCandon
{
    public partial class Booking
    {
        //to string method
        public override string ToString()
        {
            return string.Format($"\nArrival Date: {ArrivalDate}" +
                $"\nDeparture Date: {DepartureDate}");
        }
    }
}
