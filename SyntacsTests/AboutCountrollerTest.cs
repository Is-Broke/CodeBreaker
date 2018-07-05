using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SyntacsApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SyntacsTests
{
    public class AboutCountrollerTest
    {
        [Fact]
        public void TestAboutControllerReturnsView()
        {
            AboutController ac = new AboutController();
            ViewResult vr = (ViewResult)ac.Index();
            Assert.True(vr.ViewData.ModelState.IsValid);
        }
    }
}
