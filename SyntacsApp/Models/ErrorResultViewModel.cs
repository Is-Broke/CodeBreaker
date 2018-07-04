using Microsoft.EntityFrameworkCore;
using SyntacsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyntacsApp.Models
{
    public class ErrorResultViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Error> Errors { get; set; }
        public string[] CodeFormat { get; set; }
        public Error Error { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
        /// <summary>
        /// Action that pings the Syntacs Database for comments based on the the Error
        /// Example ID from the API call
        /// </summary>
        /// <param name="id">Error ID</param>
        /// <param name="context">Context of the Syntacs Database</param>
        /// <returns>ErrorResultViewModel</returns>
        public static async Task<ErrorResultViewModel> ViewDetails(int id, SyntacsDbContext context, List<Error> errors)
        {
            ErrorResultViewModel ervm = new ErrorResultViewModel
            {
                Errors = errors,
                Error = errors.FirstOrDefault(e => e.ID == id),
                Users = await context.Users.ToListAsync(),
                Comments = await context.Comments.Where(c => c.ErrExampleID == id)
                                                 .Join(context.Users,
                                                        c => c.UserID,
                                                        u => u.ID,
                                                        (comment, user) => new Comment
                                                        {
                                                            Alias = user.Alias,
                                                            UserID = user.ID,
                                                            ID = comment.ID,
                                                            CommentBody = comment.CommentBody,
                                                            ErrExampleID = comment.ErrExampleID,
                                                            UpVote = comment.UpVote
                                                        })
                                                  .ToListAsync()
            };

            ervm.CodeFormat = CodeFormatter(ervm.Error);
            return ervm;
        }
        /// <summary>
        /// Action that will grab a specific error from the API and also the comments and users associated with that
        /// error
        /// </summary>
        /// <param name="id">Error ID</param>
        /// <param name="context">Syntacs DbContext</param>
        /// <param name="error">Error</param>
        /// <returns>ErrorResultViewModel</returns>
        public static async Task<ErrorResultViewModel> ViewDetailsError(int id, SyntacsDbContext context, Error error)
        {
            ErrorResultViewModel ervm = new ErrorResultViewModel
            {
                Error = error,
                Users = await context.Users.ToListAsync(),
                Comments = await context.Comments.Where(c => c.ErrExampleID == id)
                                                 .Join(context.Users,
                                                        c => c.UserID,
                                                        u => u.ID,
                                                        (comment, user) => new Comment
                                                        {
                                                            Alias = user.Alias,
                                                            UserID = user.ID,
                                                            ID = comment.ID,
                                                            CommentBody = comment.CommentBody,
                                                            ErrExampleID = comment.ErrExampleID,
                                                            UpVote = comment.UpVote
                                                        })
                                                  .ToListAsync()
            };
            ervm.CodeFormat = CodeFormatter(ervm.Error);
            return ervm;
        }
        /// <summary>
        /// Action that takes in a list of all errors
        /// </summary>
        /// <param name="errors">List of Errors from the API</param>
        /// <returns>ErrorResultViewModel</returns>
        public static ErrorResultViewModel AllDetails(List<Error> errors)
        {
            ErrorResultViewModel ervm = new ErrorResultViewModel
            {
                Errors = errors
            };
            return ervm;
        }
        /// <summary>
        /// Method used to view the current Top Rated Error
        /// </summary>
        /// <param name="error">Error to pass in</param>
        /// <returns></returns>
        public static ErrorResultViewModel ViewTopError(Error error)
        {
            ErrorResultViewModel ervm = new ErrorResultViewModel
            {
                Error = error,
                CodeFormat = error.CodeExample.Split("\n")
            };
            return ervm;
        }
        /// <summary>
        /// Method that formats incoming error string
        /// </summary>
        /// <param name="error">Error to be formatted</param>
        /// <returns>srring array</returns>
        public static string[] CodeFormatter(Error error)
        {
            string[] codeSnippet = error.CodeExample.Split("\n");
            return codeSnippet;
        }
    }
}
