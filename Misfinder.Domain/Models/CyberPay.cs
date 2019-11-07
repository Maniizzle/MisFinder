using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public class CyberPay
    {
        public string IntegrationKey { get; set; } = "aa18e565da774d61b58d86076f17010a";
        public string ReturnUrl { get; set; }
        public string Currency { get; set; } = "NGN";
        public string MerchantRef { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
    }
}