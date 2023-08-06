using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KnowBooks.Data;
using KnowBooks.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace KnowBooks.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public IndexModel(KnowBooks.Data.KnowBooksContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int Rating { get; set; }

        public SelectList? Titles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookTitle { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Review != null)
            {
                Review = await _context.Review.ToListAsync();
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from b in _context.Review
                                            orderby b.BookTitle
                                            select b.BookTitle;
            var reviews = from b in _context.Review
                        select b;
            if (Rating != 0)
            {
                reviews = reviews.Where(s => s.Rating == Rating);
            }

            if (!string.IsNullOrEmpty(BookTitle))
            {
                reviews = reviews.Where(x => x.BookTitle == BookTitle);
            }
            Titles = new SelectList(await genreQuery.Distinct().ToListAsync());
            Review = await reviews.ToListAsync();
        }
    }
}
