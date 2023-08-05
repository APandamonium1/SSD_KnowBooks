using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KnowBooks.Data;
using RazorPagesMovie.Models;

namespace KnowBooks.Pages.Audit
{
    public class CreateModel : PageModel
    {
        private readonly KnowBooks.Data.KnowBooksContext _context;

        public CreateModel(KnowBooks.Data.KnowBooksContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuditRecord AuditRecord { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AuditRecords == null || AuditRecord == null)
            {
                return Page();
            }

            _context.AuditRecords.Add(AuditRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
