namespace KhumaloCrafts.WebApp.Models
{
    public class Order
    {
        public string? UserId { get; set; }

        public string? OrderDate { get; set; }

        public string? OrderStatus { get; set; }

        public string? Items { get; set; }

        public string? TotalPrice { get; set; }

        public string? ShippingAddress { get; set; }
    }
}
