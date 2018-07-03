using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SyntacsApp.Models;

namespace SyntacsApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string topError = await APICallTopError();
            string tokens = JToken.Parse(topError).ToString();
            var top = JsonConvert.DeserializeObject<Error>(tokens);

            ViewData["Error Name"] = top.DetailedName;
            ViewData["Code Example"] = top.CodeExample;

            return View();
        }
        /// <summary>
        /// Action for using a custom error page that is shared
        /// throughout the site
        /// </summary>
        /// <param name="id">id of the error</param>
        /// <returns>An error view</returns>
        public IActionResult Error(string id)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;
            var isError = id;
            return View();
        }
        /// <summary>
        /// Action uses to make a request to the Broken API to find the top
        /// voted error
        /// </summary>
        /// <returns>JSON string</returns>
        // TODO: Make APICall Interface to avoid duped code
        public async Task<string> APICallTopError()
        {
            using(var client = new HttpClient())
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
