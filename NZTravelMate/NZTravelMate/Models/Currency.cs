using System;
using System.Collections.Generic;
using System.Text;

namespace NZTravelMate.Models
{
    public class Currency
    {
        public Currency(string code, string name, double rate)
        {
            Code = code;
            Name = name;
            Rate = rate;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
    }
}
