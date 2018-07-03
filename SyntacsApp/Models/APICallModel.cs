using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SyntacsApp.Models
{
    public class APICallModel
    {
        string URL { get; } = "https://brokenapi.azurewebsites.net";
        /// <summary>
        /// Action uses to make a request to the Broken API to find the top
        /// voted error
        /// </summary>
        /// <returns>JSON string</returns>
        public static async Task<string> APICallTopError()
        {
            APICallModel apm = new APICallModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apm.URL);
                var response = client.GetAsync("api/error/").Result;

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    string topErrorResult = await response.Content.ReadAsStringAsync();
                    return topErrorResult;
                }
                return "";
            }
        }
        /// <summary>
        /// Action used to make a get request to the API to grab all
        /// available errors
        /// </summary>
        /// <returns>JSON string</returns>
        public static async Task<string> APICallGetAll()
        {
            APICallModel apm = new APICallModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apm.URL);
                var response = client.GetAsync("api/error/all").Result;

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    string allErrors = await response.Content.ReadAsStringAsync();
                    return allErrors;
                }
                return "";
            }
        }
        /// <summary>
        /// Action used to make a request to the Broken API to find Errors
        /// based off of the search word
        /// </summary>
        /// <param name="search">Search word</param>
        /// <returns>Error Results</returns>
        public static async Task<string> APICallErrorResults(string error)
        {
            APICallModel apm = new APICallModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apm.URL);
                var response = client.GetAsync($"api/error/search/{error}").Result;

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    string ErrorResults = await response.Content.ReadAsStringAsync();
                    return ErrorResults;
                }
                return "";
            }
        }
    }
}
