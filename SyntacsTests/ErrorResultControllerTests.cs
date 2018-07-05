using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SyntacsApp.Controllers;
using SyntacsApp.Data;
using SyntacsApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SyntacsTests
{
    public class ErrorResultControllerTests
    {
        [Fact]
        public async void GetAnErrorFromAPI()
        {
            DbContextOptions<SyntacsDbContext> options =
                new DbContextOptionsBuilder<SyntacsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (SyntacsDbContext context = new SyntacsDbContext(options))
            {
                string errorResult = await APICallModel.APICallErrorResults("Invalid Conversion");
                string tokens = JToken.Parse(errorResult).ToString();
                Error results = JsonConvert.DeserializeObject<Error>(tokens);

                var ervm = await ErrorResultViewModel.ViewDetailsError(results.ID, context, results);

                Assert.Equal("Invalid Conversion", ervm.Error.DetailedName);
            }
        }
        [Fact]
        public async void CreateACommentForAnError()
        {
            DbContextOptions<SyntacsDbContext> options =
                new DbContextOptionsBuilder<SyntacsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (SyntacsDbContext context = new SyntacsDbContext(options))
            {
                Comment newComment = new Comment
                {
                    ID = 1,
                    CommentBody = "Some comment text",
                    UpVote = 0
                };
                User user = new User
                {
                    ID = 23,
                    Alias = "bob"
                };
                Error error = new Error
                {
                    DetailedName = "Invalid Conversion"
                };
                ErrorResultController erc = new ErrorResultController(context);

                var result = await erc.Create(23, newComment, error, user);
                RedirectToActionResult routeResult = result as RedirectToActionResult;
                Assert.Equal("Home", routeResult.ControllerName);
            }
        }
    }
}
