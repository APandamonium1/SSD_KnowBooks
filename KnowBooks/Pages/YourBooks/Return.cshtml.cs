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

namespace KnowBooks.Pages.YourBooks
{
    public class ReturnModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public ReturnModel(KnowBooks.Data.KnowBooksContext context)
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

            book.Borrower = "";
            book.ReturnDate = null;
            book.AvailabilityStatus = "Available";
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
