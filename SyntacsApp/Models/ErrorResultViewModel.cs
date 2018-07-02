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
        public Error Error { get; set; }//PLACEHOLDER
        public Comment Comment { get; set; }


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
