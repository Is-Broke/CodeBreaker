using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SyntacsApp.Models;

namespace SyntacsApp.Controllers
{
    public class ErrorListController : Controller
    {
        /// <summary>
        /// Action that makes a GET request to the API to get all available
        /// errors in reference
        /// </summary>
        /// <returns>ErrorViewModel</returns>
        public async Task<IActionResult> Index()
        {
            string errorResults = await APICallModel.APICallGetAll();
            string tokens = JToken.Parse(errorResults).ToString();
            var errorList = JsonConvert.DeserializeObject<List<Error>>(tokens);

            return View(ErrorResultViewModel.AllDetails(errorList));
        }
    }
}
