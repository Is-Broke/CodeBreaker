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
                string errorResult = await APICallModel.APICallErrorResults("Invalid Assignment");
                string tokens = JToken.Parse(errorResult).ToString();
                Error results = JsonConvert.DeserializeObject<Error>(tokens);

                var ervm = await ErrorResultViewModel.ViewDetailsError(results.ID, context, results);

                Assert.Equal("Invalid Assignment", ervm.Error.DetailedName);
            }
        }
        [Fact]
        public async void CreateACommentForAnErrorReDirectsToSearchInHome()
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
        [Fact]
        public async void CreateACommentForAndErrorCommentIsCreatedOnIsSentWithRedirect()
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
                Assert.True(routeResult.RouteValues.Values.Contains(error.DetailedName));
            }
        }
        [Fact]
        public async void CanUpVoteAComment()
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

                await erc.Create(23, newComment, error, user);
                await erc.UpVote(newComment, error, 1);
                Comment voted = context.Comments.Find(newComment.ID);
                Assert.Equal(1, voted.UpVote);
            }
        }
        [Fact]
        public async void CanDeleteAComment()
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

                await erc.Create(23, newComment, error, user);
                var notEmpty = await context.Comments.ToListAsync();
                Assert.NotEmpty(notEmpty);

                await erc.Delete(newComment, error);
                var empty = await context.Comments.ToListAsync();
                Assert.Empty(empty);
            }
        }
    }
}
