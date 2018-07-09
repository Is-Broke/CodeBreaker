using System;
using System.Collections.Generic;
using System.Text;
using SyntacsApp.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            var result  = hc.Search("Invalid Assignment").Result;
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
        [Fact]
        public async void HomeControllerExceptionGetsCaught()
        {
            HomeController hc = new HomeController();
            var result = await hc.Search("asgrgararh");
            var notFound = result as NotFoundResult;
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}
