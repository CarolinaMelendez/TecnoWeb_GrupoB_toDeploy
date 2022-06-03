using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class PriceModel
    {
        /*
            id : 6143
            uid : "35306b61-bea4-49de-b833-ee3da91a5be5"
            number : 7685820593
            leading_zero_number : "0503284653"
            decimal : 95.96
            normal : 46.901260152185515
            hexadecimal : "08bcae8a5dbac6e4"
            positive : 3410.9061925445435
            negative : -2933.9916390320764
            non_zero_number : 3
            digit : 7
        */

        public int id { get; set; }
        public string uid { get; set; }
        public double number { get; set; }
        public string leading_zero_number { get; set; }
        public double decimal_ { get; set; }
        public double normal { get; set; }
        public string hexadecimal { get; set; }
        public double positive { get; set; }
        public double negative { get; set; }
        public int non_zero_number { get; set; }
        public int digit { get; set; } 
}
}
