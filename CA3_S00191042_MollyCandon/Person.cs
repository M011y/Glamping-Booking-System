using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA3_S00191042_MollyCandon
{
    class Person
    {
        //property
        public string Capacity { get; set; }

        //constructor
        public Person(string capacity)
        {
            Capacity = capacity;
        }

        //to string
        public override string ToString()
        {
            return string.Format($"{Capacity}");
        }
    }
}
