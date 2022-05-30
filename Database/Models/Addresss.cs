using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Models
{
    public class Addresss : Entity
    {
        public string StreetName { get; set; }

        public int Number { get; set; }

        public Guid AddressId { get; set; }

        public Product Product { get; set; }
    }
}