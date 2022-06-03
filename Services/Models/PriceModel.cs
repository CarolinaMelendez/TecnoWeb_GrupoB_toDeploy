using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class PriceModel
    {
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
