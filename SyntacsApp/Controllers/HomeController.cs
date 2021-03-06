﻿using System;
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
        /// <summary>
        /// Action that makes a request to grab the Top Rated Comment
        /// </summary>
        /// <returns>ErrorResultViewModel</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var topErr = await APICallModel.APICallTopError();
                if (!String.IsNullOrEmpty(topErr))
                {
                    string tokens = JToken.Parse(topErr).ToString();
                    Error topError = JsonConvert.DeserializeObject<Error>(tokens);
                    return View(ErrorResultViewModel.ViewTopError(topError));
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NotFound();
        }
        /// <summary>
        /// Action that takes in a search string and sends that as part of the 
        /// get request to the API
        /// </summary>
        /// <param name="search">Search string</param>
        /// <returns>Redirects to a result or to the full list</returns>
        public async Task<IActionResult> Search(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                try
                {
                    string errorResults = await APICallModel.APICallErrorResults(search);
                    string tokens = JToken.Parse(errorResults).ToString();
                    Error results = JsonConvert.DeserializeObject<Error>(tokens);
                    return RedirectToAction("Index", "ErrorResult", results);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index", "ErrorList");
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
    }
}
