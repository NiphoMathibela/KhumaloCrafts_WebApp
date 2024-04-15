﻿namespace KhumaloCrafts.WebApp.Models
{
    public class User
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? ShippingAddress { get; set; }

        public string? BillingAddress { get; set; }
    }
}
