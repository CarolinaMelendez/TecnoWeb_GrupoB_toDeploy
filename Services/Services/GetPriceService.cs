using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Services.Models;
using System.Collections.Generic;
using Services.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class GetPriceService
    {
        private IConfiguration _configuration;

        public GetPriceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<double> GetPriceServiceAsync()
        {
            Console.WriteLine("Pidiendo precio del Producto");
            using (HttpClient client = new HttpClient())
            {
                string idPriceURL = _configuration.GetSection("BackingService").GetSection("PriceService").Value;
                HttpResponseMessage response = await client.GetAsync(idPriceURL);
                if (response.IsSuccessStatusCode)
                {
                    string idPriceBody = await response.Content.ReadAsStringAsync();
                    PriceModel price = JsonConvert.DeserializeObject<PriceModel>(idPriceBody);
                    Console.WriteLine($"Price given : ------------ { price.normal} " );
                    return price.normal;
                }
                else
                {
                    throw new NumberServiceException("HUBO FALLAS al pedir precio del Producto");
                }
            }
            
        }
    }
}
