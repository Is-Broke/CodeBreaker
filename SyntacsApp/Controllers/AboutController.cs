using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SyntacsApp.Controllers
{
    public class AboutController : Controller
    {
        /// <summary>
        /// Action that is used to find the view for the About Page
        /// </summary>
        /// <returns>A View</returns>
        public IActionResult Index() => View();
    }
}
