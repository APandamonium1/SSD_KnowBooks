using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KnowBooks.Data;
using KnowBooks.Models;
using RazorPagesMovie.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KnowBooks.Pages.Books
{
	[Authorize(Roles = "Staff, Owner")]
	public class CreateModel : PageModel
    {
        //[Authorize(Roles = "Admin")]
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
        public Book Book { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Book == null || Book == null)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await ImageFile.CopyToAsync(memoryStream);
                    Book.Image = memoryStream.ToArray();
                }
                return Page();
            }

            _context.Book.Add(Book);
            //await _context.SaveChangesAsync();

            // Once a record is added, create an audit record
            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Add Movie Record";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.KeyBookFieldID = Book.ISBN;
                // Get current logged-in user
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
