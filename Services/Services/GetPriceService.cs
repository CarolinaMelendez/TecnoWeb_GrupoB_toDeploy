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

        public async Task<double> GetPriceServiceAsync()
        {
            Console.WriteLine("Pidiendo precio del Producto");
            using (HttpClient client = new HttpClient())
            {
                string idPriceURL = "https://random-data-api.com/api/number/random_number";

                HttpResponseMessage response = await client.GetAsync(idPriceURL);
                if (response.IsSuccessStatusCode)
                {
                    string idPriceBody = await response.Content.ReadAsStringAsync();
                    // idPriceBody = {"id":4863,"uid":"c55e9ff1-c8be-4b3c-8a2a-dfee7a0a117e","number":7126300082,"leading_zero_number":"0597698994","decimal":85.57,"normal":54.5147749120799,"hexadecimal":"c1e65698274de505","positive":4127.9640346637125,"negative":-1152.2889008380175,"non_zero_number":9,"digit":5}
                    // http://jsonviewer.stack.hu
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
