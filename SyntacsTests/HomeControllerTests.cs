using System;
using System.Collections.Generic;
using System.Text;
using SyntacsApp.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyntacsApp.Data;

namespace SyntacsTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void HomeControllerCanConnectToAPI()
        {
            HomeController hc = new HomeController();
            Assert.True(hc.Index().IsCompletedSuccessfully);
        }
        [Fact]
        public void HomeControllerRedirectsToErrorResult()
        {
            HomeController hc = new HomeController();
            var result  = hc.Search("Invalid Conversion").Result;
            RedirectToActionResult routeResult = result as RedirectToActionResult;
            Assert.Equal("ErrorResult", routeResult.ControllerName);
        }
        [Fact]
        public void HomeControllerRedirectsToErrorList()
        {
            HomeController hc = new HomeController();
            var result = hc.Search("").Result;
            RedirectToActionResult routeResult = result as RedirectToActionResult;
            Assert.Equal("ErrorList", routeResult.ControllerName);
        }
    }
}
