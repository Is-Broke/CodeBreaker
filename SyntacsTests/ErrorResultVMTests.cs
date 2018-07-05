using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SyntacsApp.Data;
using SyntacsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SyntacsTests
{
    public class ErrorResultVMTests
    {
        [Fact]
        public async void ViewListOfSimilarErrors()
        {
            DbContextOptions<SyntacsDbContext> options =
                new DbContextOptionsBuilder<SyntacsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (SyntacsDbContext context = new SyntacsDbContext(options))
            {
                List<Error> errors = new List<Error>
                {
                    new Error
                    {
                        ID = 1,
                        ErrorCategoryID = 1,
                        DetailedName = "Invalid Conversion",
                        CodeExample = "woo\ncode\twoo"
                    },
                    new Error
                    {
                        ID = 20,
                        ErrorCategoryID = 1,
                        DetailedName = "Similar",
                        CodeExample = "woo\ncode\twoo"
                    },
                    new Error
                    {
                        ID = 15,
                        ErrorCategoryID = 1,
                        DetailedName = "Error 3",
                        CodeExample = "woo\ncode\twoo"
                    },
                };
                ErrorResultViewModel ervm = await ErrorResultViewModel.ViewDetails(1, context, errors);
                Assert.NotEmpty(ervm.Errors);
            }
        }
        [Fact]
        public async void ViewSpecificDetailsOfAnError()
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
                    ID = 1,
                    DetailedName = "Invalid Conversion",
                    CodeExample = "woo\ncode\twoo"
                };

                await context.Comments.AddAsync(newComment);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                ErrorResultViewModel ervm = await ErrorResultViewModel.ViewDetailsError(1, context, error);
                Assert.Equal(error.DetailedName, ervm.Error.DetailedName);
            }
        }
        [Fact]
        public void ViewAllAvailableErrors()
        {
            List<Error> errors = new List<Error>
            {
                new Error
                {
                    ID = 1,
                    DetailedName = "Invalid Conversion",
                    CodeExample = "woo\ncode\twoo"
                },
                new Error
                {
                    ID = 4,
                    DetailedName = "NullReference",
                    CodeExample = "woo\ncode\twoo"
                },
                new Error
                {
                    ID = 8,
                    DetailedName = "Exception",
                    CodeExample = "woo\ncode\twoo"
                }
            };
            ErrorResultViewModel ervm = ErrorResultViewModel.AllDetails(errors);
            Assert.NotEmpty(ervm.Errors);
        }
        [Fact]
        public void ViewTheTopError()
        {
            Error error = new Error
            {
                ID = 1,
                DetailedName = "Invalid Conversion",
                CodeExample = "woo\ncode\twoo",
                Votes = 10,
            };
            ErrorResultViewModel ervm = ErrorResultViewModel.ViewTopError(error);
            Assert.Equal(10, ervm.Error.Votes);
        }
        [Fact]
        public void CodeGetsSplitIntoArrayToBeFormatted()
        {
            Error error = new Error
            {
                ID = 1,
                DetailedName = "Invalid Conversion",
                CodeExample = "int a = 1, b = 2;\nint c = a + b\nint d = c - b"
            };
            string[] split = ErrorResultViewModel.CodeFormatter(error);
            Assert.Equal(3, split.Length);
        }
    }
}
