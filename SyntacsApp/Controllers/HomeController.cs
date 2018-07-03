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
            string topErr = await APICallModel.APICallTopError();
            string tokens = JToken.Parse(topErr).ToString();
            Error topError = JsonConvert.DeserializeObject<Error>(tokens);

            return View(ErrorResultViewModel.ViewTopError(topError));
        }

        //public async Task<IActionResult> Search(string search)
        //{

        //}
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
    }
}
