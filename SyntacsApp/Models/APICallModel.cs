using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SyntacsApp.Models
{
    public class APICallModel
    {



        /// <summary>
        /// Action uses to make a request to the Broken API to find the top
        /// voted error
        /// </summary>
        /// <returns>JSON string</returns>
        public static async Task<string> APICallTopError()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://brokenapi.azurewebsites.net");
                var response = client.GetAsync("api/error").Result;

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    string topErrorResult = await response.Content.ReadAsStringAsync();
                    return topErrorResult;
                }
                return "";
            }
        }
    }
}
