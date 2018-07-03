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
        public IEnumerable<Error> Errors { get; set; }
        public Error Error { get; set; }//PLACEHOLDER
        public Comment Comment { get; set; }

        /// <summary>
        /// Action that pings the Syntacs Database for comments based on the the Error
        /// Example ID from the API call
        /// </summary>
        /// <param name="id">Error ID</param>
        /// <param name="context">Context of the Syntacs Database</param>
        /// <returns>ErrorResultViewModel</returns>
        public static async Task<ErrorResultViewModel> ViewDetails(int id, SyntacsDbContext context)
        {
            ErrorResultViewModel ervm = new ErrorResultViewModel();

            ervm.Error = new Error
            {
                ID = id
            };

            ervm.Comments = await context.Comments.Where(c => c.ErrExampleID == id).ToListAsync();

            return ervm;
        }
    }
}
