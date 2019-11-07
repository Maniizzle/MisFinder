using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public class PaymentStatus
    {
        public bool Succeeded { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string TransactionReference { get; set; }
        public decimal Charge { get; set; }
        public string RedirectUrl { get; set; }
    }
}