using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SyntacsApp.Data;
using SyntacsApp.Models;

namespace SyntacsApp.Controllers
{
    public class ErrorResultController : Controller
    {
        private readonly SyntacsDbContext _context;
        /// <summary>
        /// Constructor to set up our dependency injection
        /// </summary>
        /// <param name="context">Our DbContext</param>
        public ErrorResultController(SyntacsDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Action that returns the result of the error search
        /// </summary>
        /// <param name="id">id of error</param>
        /// <returns>A ViewModel or a NotFound if no value</returns>
        public async Task<IActionResult> Index(Error error)
        {
            if (error != null)
            {
                return View(await ErrorResultViewModel.ViewDetailsError(error.ID, _context, error));
            }
            return NotFound();
        }
        /// <summary>
        /// Action that is used to create a comment entered by the user and is stored on the
        /// Syntacs Database
        /// </summary>
        /// <param name="id">ID of the current error</param>
        /// <param name="comment">Data that is bound to a Comment object</param>
        /// <returns>
        /// A redirect to the error result page either with updated comments or
        /// modelstate fai
        /// l</returns>
        [HttpPost]
        public async Task<IActionResult> Create(int id, [Bind("ID,Alias,CommentBody")]Comment comment, Error error)
        {
            if (ModelState.IsValid)
            {
                comment.ErrExampleID = id;
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Search", "Home", new { search = error.DetailedName });
            }
            return RedirectToAction("Search", "Home", new { search = error.DetailedName });
        }
  
    }
}
