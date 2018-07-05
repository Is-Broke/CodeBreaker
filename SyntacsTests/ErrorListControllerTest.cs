using System;
using System.Collections.Generic;
using System.Text;
using SyntacsApp.Controllers;
using Xunit;

namespace SyntacsTests
{
    public class ErrorListControllerTest
    {
        [Fact]
        public void ErrorListControllerGrabsAllFromAPI()
        {
            ErrorListController elc = new ErrorListController();
            Assert.True(elc.Index().IsCompletedSuccessfully);
        }
    }
}
