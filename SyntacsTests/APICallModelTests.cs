using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SyntacsApp.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SyntacsTests
{
    public class APICallModelTests
    {
        [Fact]
        public void MakeAPICallToGetTopError()
        {
            var response = APICallModel.APICallTopError();
            Assert.NotNull(response);
        }
        [Fact]
        public async void MakeAPICallToGetAllErrorsr()
        {
            var response = await APICallModel.APICallGetAll();
            var tokens = JToken.Parse(response).ToString();
            List<Error> errors = JsonConvert.DeserializeObject<List<Error>>(tokens);
            Assert.NotEmpty(errors);
        }
        [Fact]
        public async void MakeAPICallToGetSpecificError()
        {
            var response = await APICallModel.APICallErrorResults("dividebyzero exception");
            var tokens = JToken.Parse(response).ToString();
            Error result = JsonConvert.DeserializeObject<Error>(tokens);

            Assert.Equal("DivideByZero Exception", result.DetailedName);
        }

    }
}
