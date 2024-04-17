using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class CartItems
    {
        public int? OrderId { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
        public string? Images { get; set; }
        public string? UserId { get; set; }
        public string? ProductId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? Items { get; set; }
        public string? ShippingAddress { get; set; }
        public int Quantity { get; set; }
    }
}
