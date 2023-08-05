using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowBooks.Data;
using KnowBooks.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KnowBooks.Pages.Books
{
	[Authorize(Roles = "Member")]
	public class LoanModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public LoanModel(KnowBooks.Data.KnowBooksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.ISBN == id);
            if (book == null)
            {
                return NotFound();
            }

            TimeZoneInfo singaporeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to Singapore time
            DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(utcNow, singaporeTimeZone);


            book.Borrower = User.Identity.Name;
            book.ReturnDate = currentDate.AddDays(7);
            book.AvailabilityStatus = "Loaned";
            _context.SaveChanges();

            Book = book;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("../YourBooks/Index");
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.ISBN == id)).GetValueOrDefault();
        }
    }
}
