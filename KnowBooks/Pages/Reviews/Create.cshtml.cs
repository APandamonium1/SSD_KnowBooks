using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KnowBooks.Data;
using KnowBooks.Models;

namespace KnowBooks.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public CreateModel(KnowBooks.Data.KnowBooksContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id, string title)
        {
            if (id == null || title == null)
            {
                return NotFound();
            }

            // Set the title and disable the input field
            Review = new Review
            {
                BookTitle = title,
                User = User.Identity.Name
            };

            return Page();
        }

        [BindProperty]
        public Review Review { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Review == null || Review == null)
            {
                return Page();
            }

            _context.Review.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
