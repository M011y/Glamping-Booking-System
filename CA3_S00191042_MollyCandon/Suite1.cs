using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_S00191042_MollyCandon
{
    public partial class Suite
    {
        //get details method
        public string GetDetails()
        {
            return string.Format($"Suite: {Name}" +
            $"\nDescription: {Description}" +
            $"\nCapacity: {Capacity}" +
            $"\nBed Type: {Bed}" +
            $"\nPrice per night: {Rate}");
        }

        //tostring method
        public override string ToString()
        {
            return string.Format($"Suite: {Name}" +
            $"\nDescription: {Description}" +
            $"\nPrice per night: {Rate}");
        }
    }
}
