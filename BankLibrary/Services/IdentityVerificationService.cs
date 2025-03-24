using BankLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace BankLibrary.Services
{
    public class IdentityVerificationService : IIdentityVerificationService
    {
        private readonly HttpClient _httpClient;
        public IdentityVerificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> VerifyCprAsync(string cprNumber)
        {
            // In a real application,call an external API or do real logic here.
            // make an HTTP call or some other network request to check the CPR
            var response = await _httpClient.GetAsync($"https://api.cpr-service.com/verify?cpr={cprNumber}");

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(result);
        }
    }
}
