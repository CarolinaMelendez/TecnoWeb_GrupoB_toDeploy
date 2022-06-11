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
    public class PriceService
    {
        private IConfiguration _configuration;

        public PriceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<double> GetPriceServiceAsync()
        {
            try
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
                        Console.WriteLine($"Price given : ------------ { price.normal} ");
                        return price.normal;
                    }
                    else
                    {
                        throw new PriceServiceException("HUBO FALLAS al pedir precio del Producto");
                    }
                }
            }
            catch (PriceServiceException ex)
            {
                string err = "HUBO FALLAS al pedir info del Precio";
                Console.WriteLine(err);
                Console.WriteLine(ex.Message + ex.StackTrace);
                throw new PriceServiceException($"{err} : {ex.Message}");
            }
        }
    }
}
