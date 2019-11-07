using MisFinder.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Utility
{
    public static class Processing
    {
        public static async Task<PaymentStatus> MakePaymentAsync(CyberPay model)
        {
            PaymentStatus status = new PaymentStatus();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://cyberpay-payment-api.azurewebsites.net/api/v1/payments", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    status = JsonConvert.DeserializeObject<PaymentStatus>(apiResponse);
                }
            }
            return status;
        }
    }
}