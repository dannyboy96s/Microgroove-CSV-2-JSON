using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microgroove.Models
{
    public class MicroGrooveModel
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public List<Order> Orders { get; set; }
        public Ender Ender { get; set; }
    }

    public class Order
    {
        public string OrderDate { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public Buyer Buyer { get; set; }
        public List<Item> Items { get; set; }
        public Timing Timing { get; set; }
    }

    public class Buyer
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
    }

    public class Item
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }

    public class Timing
    {
        public int Start { get; set; }
        public int Stop { get; set; }
        public int Gap { get; set; }
        public int Offset { get; set; }
        public int Pause { get; set; }
    }

    public class Ender
    {
        public int Process { get; set; }
        public int Paid { get; set; }
        public int Created { get; set; }
    }
}
