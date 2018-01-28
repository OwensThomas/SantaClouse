using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SantaClouse.Models
{
    public class Orders
    {
        public List<Order> EntityList { get; set; }
        public List<ToyKid> ToyList { get; set; }
        public decimal TotalCost { get; set; }
    }
}