using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class DOrder
    {
        public int? Id { get; set; }

        public string? UserId { get; set; }

        public string? OrderDate { get; set; }

        public string? OrderStatus { get; set; }

        public string? Items { get; set; }

        public string? TotalPrice { get; set; }

        public string? ShippingAddress { get; set; }
    }
}
